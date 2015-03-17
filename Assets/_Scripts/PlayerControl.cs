﻿using UnityEngine;
using System.Collections;
using InControl;


public class PlayerControl : MonoBehaviour {

	// is casting (fireball or special spell) or not, sync with animator in child model
	public bool isCasting = false;

	// Current speed ratio.
	private float speedScale = 3.5f;

	// Is player running
	public bool isRunning = false;

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

	// Local variables & local status
	private float maxVelocity = 10.0f;
	private bool isPosAiming = false;

	void Start()
	{

		// mapping input events
		inputManager = GetComponent<UserInputManager> ();
		inputManager.OnPressMainSkill += TryCastingMainSkil;
		inputManager.OnPressSubSkill += TryCastingSubSkill;
		inputManager.OnReleaseSubSkill += StopCastingSubSkill;
		inputManager.OnPressButton += HandleButton;
		inputManager.OnReleaseButton += ReleaseButton;
		inputManager.OnPressRunning += StartRunning;
		inputManager.OnReleaseRunning += EndRunning;

		// other local components
		animator = GetComponentInChildren<Animator> ();
		attackMeans = GetComponent<WizardAttackMeans> ();
		castingAid = GetComponent<CastingAid> ();

	}

	// -------------------------------------------------------------------------
	// ------- Start ---------- Input Events Callback Functions ----------------
	// -------------------------------------------------------------------------

	void TryCastingMainSkil()
	{
		// Debug.Log ("try casting fireball");
		if(!animator.GetBool("isCasting") && 
		   !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1")) {
			// Debug.Log ("casting spell" + magicID);

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
	}

	void EndRunning()
	{
		Debug.Log("Finish Running");
		isRunning = false;
	}

	// ---------------------------------------------------------------------
	// ------ START ------------- Casting Functions ------------------------
	// ---------------------------------------------------------------------

	void CastFireball()
	{
		// cast fireball
		Vector3 direction = transform.forward;
		attackMeans.AttackByDiretion (SpellDB.AttackID.fireball, direction);
	}


	void CastMagic(int magicID)
	{
		// cast a special spell by magic ID
		Vector3 direction = transform.forward;
		
		// For metero/Aerolite skill
		// TODO swiss army is fucking idiot 
		// Avoid it
		// DEPRECATED 
		//		if(magicID == 2)
		//		{
		//			if(isPosAiming == false)
		//			{
		//				// start Aiming
		//				isPosAiming = true;
		//				StartCoroutine(AimingDecending());
		//				castingAid.StartAiming();
		//			}
		//			else
		//			{
		//				isPosAiming = false;
		//				StartCoroutine(CastCoolDown());
		//				Vector3 targetPos = castingAid.EndAiming();
		//				attackMeans.AttackToPosition ((SpellDB.AttackID)magicID, targetPos);
		//			}
		//		}
		//		else
		//		{
		// StartCoroutine(CastCoolDown());
		attackMeans.AttackByDiretion ((SpellDB.AttackID)magicID, direction);
		//		}
		
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
				
			float runningForce = 50.0f;

			if(isRunning)
				RB.AddForce(transform.forward * runningForce);
			else
			{
				RB.velocity = Vector3.right * speedScale * input.x + Vector3.forward * speedScale * input.y;

//				transform.Translate( Vector3.right * speedScale * Time.fixedDeltaTime * input.x, Space.World);
//				transform.Translate( Vector3.forward * speedScale * Time.fixedDeltaTime * input.y, Space.World);
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
		if(!animator.GetBool("isCasting") && 
		   !animator.GetCurrentAnimatorStateInfo(0).IsName("isCasting") &&
		   input.magnitude > 0 )
		{
			Vector3 newForward = new Vector3 (input.x, 0.0f, input.y).normalized;
			SmoothRotate (newForward);
		}
		// rotate target with right stick.
	}

	void SmoothRotate(Vector3 vec_to)
	{
		//	transform.LookAt (transform.position + newForward);
		Vector3 vec_from = transform.forward;
		float minDeltaAngle = Constants.PLAYER_ANGULAR_SPEED;
		if(isRunning) minDeltaAngle /= 2.5f;
		// calculate new direction by
		Vector3 newDir = Vector3.RotateTowards(vec_from, vec_to, minDeltaAngle, 0.0F);

//		Debug.DrawRay(transform.position, newDir, Color.red);

		transform.rotation = Quaternion.LookRotation(newDir);

		//		rigidbody.AddTorque (torqueFactor * Vector3.Cross (vec_from, vec_to));

	}
	// -------------------------------------------------------------------------
	// -------- Start ----------------- Public Interfaces ----------------------
	// -------------------------------------------------------------------------

	public void Die() {
		int playerId = GetComponent<UserInputManager> ().playerNum;
		Debug.Log ("[Player] Player died, " + playerId);
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
	
	IEnumerator CastCoolDown()
	{
		animator.SetBool("isCasting", true);
		yield return new WaitForSeconds (Constants.MIN_CAST_COOL_DOWN);
		animator.SetBool("isCasting", false);
	}

	private IEnumerator DieAnim() {
		animator.SetBool ("isAlive", false);
		yield return new WaitForSeconds(0.2f);
		animator.SetBool ("isAlive", true); // reset to lock animation

		GameStatus.Instance.DecrementPlayerLife (inputManager.playerNum);

	}

	void DrawDebug()
	{
		Color lineColor = Color.red;
		
		if(isCasting)
			lineColor = Color.green;
		Debug.DrawLine(transform.localPosition, 
		               transform.localPosition + 10.0f * transform.forward,
		               lineColor);
	}
}
