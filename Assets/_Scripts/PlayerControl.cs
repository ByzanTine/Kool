using UnityEngine;
using System.Collections;
using InControl;


public class PlayerControl : MonoBehaviour {

//	// is casting (fireball or special spell) or not, sync with animator in child model
//	public bool isCasting = false;
//
//	// is stabing (fireball or special spell) or not, sync with animator in child model
//	public bool isStabing = false;

	// Current speed ratio.
	private float speedScale = Constants.PLAYER_MOVE_SPEED;

	// Is player running
	public bool isRunning = false;

	// for stab use
	public GameObject explodePrefab;

	// Current magic ID that player chosed
	public int magicID = 1;

	// This is a public speed indicator for debugging usage
	public int Speed;
	public float KineticSpeed;

	// Local components
	private UserInputManager inputManager;
	private Animator animator;
	private WizardAttackMeans attackMeans;
	private CastingAid castingAid;
	private PlayerData PD;

	// Local variables & local status
	private bool isPosAiming = false;

	// for aim assistant system;
	public const float current_dir_lock_zone = 0.05f;
	public const float target_dir_lock_zone = 0.4f;
	public const float magnitude_decay = 20f;
	public const float joystick_min_magnitute = 0.5f;

	void Start()
	{

		// mapping input events
		inputManager = GetComponent<UserInputManager> ();
//		inputManager.OnPressHit += TryCombatAttack;

		inputManager.OnPressMainSkill += TryCastingMainSkill;
		inputManager.OnPressSubSkill += TryCastingSubSkill;
		inputManager.OnReleaseSubSkill += StopCastingSubSkill;
		inputManager.OnPressButton += HandleButton;
		inputManager.OnReleaseButton += ReleaseButton;
		inputManager.OnPressRunning += StartRunning;
		inputManager.OnReleaseRunning += EndRunning;
		inputManager.OnPressSwapIceFire += SwapIceFire;
		// other local components
		animator = GetComponentInChildren<Animator> ();
		attackMeans = GetComponent<WizardAttackMeans> ();
		castingAid = GetComponent<CastingAid> ();
		PD = GetComponent<PlayerData> ();
	}

	// -------------------------------------------------------------------------
	// ------- Start ---------- Input Events Callback Functions ----------------
	// -------------------------------------------------------------------------

	void SwapIceFire()
	{
		Debug.Log ("change Ice fire");
		PD.ChangeIceFire ();
	}

	void TryCastingMainSkill()
	{

		// If running, main skill will be a combat attack
		if(isRunning && animator.GetCurrentAnimatorStateInfo(0).IsName("Run"))
		{
			// try stabbing if running
			if(!animator.GetBool("isStabbing") && 
			   !animator.GetCurrentAnimatorStateInfo(0).IsName("Stab")) {
				Debug.Log ("[SPELL]: combat stab");
				Stab();
			}
		}
		// else will cast fireball/ iceball
		else if(!animator.GetBool("isCasting") && 
		   !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1")) {

			// try casting if not running
			Debug.Log ("[SPELL]: casting fireball");
			// StartCoroutine(castCoolDown());
			CastFireball();
		}
	}


	void TryCastingSubSkill()
	{
		// Debug.Log ("try casting spell");
		if(!animator.GetBool("isCasting") && 
		   !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
		{
			Debug.Log ("[SPELL]: casting spell " + magicID);
			if (PD.SpecialSpellID != SpellDB.AttackID.None)
				CastMagic(magicID);
		}
	}

	void StopCastingSubSkill()
	{

	}

	void HandleButton()
	{
		magicID = inputManager.button_id;
	}

	void ReleaseButton()
	{

	}

	void StartRunning()
	{
		Debug.Log("Start Running");
		isRunning = true;
//		inputManager.LockRightInput ();
	}

	void EndRunning()
	{
		if(isRunning)
		{
			Debug.Log("Finish Running");
			isRunning = false;
//			inputManager.UnlockAllControl ();
		}
	}

	// ---------------------------------------------------------------------
	// ------ START ------------- Casting Functions ------------------------
	// ---------------------------------------------------------------------

	void Stab()
	{
		GameStatistic.Instance.Attacks [UserInfoManager.UserDataCollection [inputManager.playerNum].teamID]++;
		StartCoroutine (CombatAttack ());
	}

	IEnumerator CombatAttack()
	{
		animator.SetBool("isStabbing", true);

		yield return new WaitForSeconds (0.3f);
		inputManager.LockControl (UserInputManager.InputSource.AllControl, Constants.MIN_STAB_COOL_DOWN);

		explodePrefab.GetComponent<ColliderExplode> ().caster = this.gameObject;

		float explodeNum = 5;
		for (int i = 0; i < explodeNum; ++i)
		{
			yield return new WaitForSeconds (0.2f / explodeNum);
			Instantiate(explodePrefab, transform.position + i * transform.forward, Quaternion.identity);
		}

		yield return new WaitForSeconds (Constants.MIN_STAB_COOL_DOWN - 0.5f);

		animator.SetBool("isStabbing", false);
	}
		
		
	void CastFireball()
	{
		// cast one fireball
		Vector3 direction = transform.forward;
		attackMeans.AttackByDiretion (PD.spellID, direction);

		GameStatistic.Instance.Attacks [UserInfoManager.UserDataCollection [inputManager.playerNum].teamID]++;

	}

	// Magic ID is controlled by button
	void CastMagic(int magicID)
	{
		inputManager.LockControl (UserInputManager.InputSource.LStick, 2.0f);
		// cast a special spell by magic ID
		Vector3 direction = transform.forward;
		attackMeans.AttackByDiretion (PD.SpecialSpellID, direction);
		// HACK Cost the magic point
		PD.SpecialSpellID = SpellDB.AttackID.None;

		GameStatistic.Instance.Ults [UserInfoManager.UserDataCollection [inputManager.playerNum].teamID]++;

	}

	// -------------------------------------------------------------------------
	// ------ START ------------- Frame Update Funcions ------------------------
	// -------------------------------------------------------------------------

	void FixedUpdate()
	{
		Move (inputManager.leftInput);

		// will overwrite the direction updated by move,
		// so the fireball/spell will cast in correct direction
		Rotate (inputManager.rightInput);

		// show casting lines
		DrawDebug();
	}
	

	void Move(Vector2 input)
	{	
		Rigidbody RB = GetComponent<Rigidbody>();

		if(!animator.GetBool("isCasting") && 
		   !animator.GetCurrentAnimatorStateInfo(0).IsName("isCasting") &&
		   input.magnitude > 0 )
		{
			// move target object with left stick.

			if(inputManager.rightInput.magnitude == 0)
			{
				Vector3 newForward = new Vector3 (input.x, 0.0f, input.y).normalized;
				SmoothRotate (newForward);
			}

			speedScale = Constants.PLAYER_MOVE_SPEED;

			float LocalForzenScale = 1;
			if (PD.frozen)
				LocalForzenScale = 0.25f;

			speedScale *= LocalForzenScale;


			if(isRunning)
			{
				float runningForce = speedScale * 15.0f;
				RB.AddForce(transform.forward * runningForce);
			}
			else
			{
				RB.velocity = (Vector3.right * speedScale * input.x + Vector3.forward * speedScale * input.y);
			}

		}
		KineticSpeed = RB.velocity.magnitude;
		Speed = 1;
		// Speed Animator > 1 Run <= 1 Walk, 0 idle
		if(input.magnitude == 0)
		{
			Speed = 0;
		}
		else if(isRunning)
		{
			Speed = 2;
		}
		animator.SetInteger ("Speed", Mathf.RoundToInt(Speed));
	}

	void Rotate(Vector2 input)
	{
		// Ban rotate when run
		if(isRunning) input *= 0;

		if(!animator.GetBool("isCasting") && 
		   !animator.GetCurrentAnimatorStateInfo(0).IsName("isCasting") &&
		   input.magnitude >  joystick_min_magnitute)
		{
			Vector3 newForward = new Vector3 (input.x, 0.0f, input.y).normalized;
			SmoothRotate (newForward);
		}
		// rotate target with right stick.
	}

	bool TargetNearDirectoion(Vector3 direction){
		GameObject[] objs = GameObject.FindGameObjectsWithTag("Player");
		foreach (GameObject obj in objs) {
			Vector3 delta = obj.transform.position - transform.position;
			if((delta.normalized - direction).sqrMagnitude < current_dir_lock_zone){
				return true;
			}
		}
		return false;
	}

	void SmoothRotate(Vector3 vec_to)
	{
		//	transform.LookAt (transform.position + newForward);
		Vector3 vec_from = transform.forward;
		float minDeltaAngle = Constants.PLAYER_ANGULAR_SPEED;

		if (TargetNearDirectoion (vec_from) && (vec_to - vec_from).sqrMagnitude < target_dir_lock_zone) {
			minDeltaAngle /= magnitude_decay;
		}
		if(isRunning) minDeltaAngle /= 2.0f;
		// calculate new direction by
		Vector3 newDir = Vector3.RotateTowards(vec_from, vec_to, minDeltaAngle, 1F);
//		Debug.DrawRay(transform.position, newDir, Color.red);

		transform.rotation = Quaternion.LookRotation(newDir);

		//		rigidbody.AddTorque (torqueFactor * Vector3.Cross (vec_from, vec_to));

	}
	// -------------------------------------------------------------------------
	// -------- Start ----------------- Public Interfaces ----------------------
	// -------------------------------------------------------------------------

	public void Die() {
		int playerId = GetComponent<UserInputManager> ().playerNum;
		Debug.Log ("[Player] Player died, id: " + playerId);
		StartCoroutine (DieAnim ());

	}
	// -------------------------------------------------------------------------
	// -------- Start ---------- Helper and IEnumerator Functions --------------
	// -------------------------------------------------------------------------

	IEnumerator AimingDecending()
	{
		yield return new WaitForSeconds (3.0f);
		isPosAiming = false;
	}

//	IEnumerator CastCoolDown()
//	{
//		animator.SetBool("isCasting", true);
//		yield return new WaitForSeconds (Constants.MIN_CAST_COOL_DOWN);
//		animator.SetBool("isCasting", false);
//	}
	


	private IEnumerator DieAnim() {
		animator.SetBool ("isAlive", false);
		inputManager.LockControl(UserInputManager.InputSource.AllControl, 2.0f);
		Debug.Log("reset player death animation");
		yield return new WaitForSeconds(0.2f);
		animator.SetBool ("isAlive", true); // reset to lock animation

		yield return new WaitForSeconds(1.0f);
		// For compatiblity for other scenes
		if (GameStatus.Instance)
			GameStatus.Instance.DecrementPlayerLife (inputManager.playerNum);
		else 
			Debug.Log("[Player] player died, but no Gamestatus to update");

	}

	void DrawDebug()
	{
		Color lineColor = Color.red;
		
		if(animator.GetBool("isCasting"))
			lineColor = Color.green;
		Debug.DrawLine(transform.localPosition, 
		               transform.localPosition + 10.0f * transform.forward,
		               lineColor);
	}
}
