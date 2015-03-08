using UnityEngine;
using System.Collections;
using InControl;


public class PlayerControl : MonoBehaviour {
	// for testing

	// is casting (fireball or special spell) or not, sync with animator in child model
	public bool isCasting = false;
	// CurSpeed, as the absolute value, currently only 1 or 0 is used
	public int Speed = 0;
	// local status
	public int magicID = 1;

	private UserInputManager inputManager;
	private Animator animator;
	private WizardAttackMeans attackMeans;

	void Start()
	{
		// mapping input events
		inputManager = GetComponent<UserInputManager> ();
		inputManager.OnPressLBumper += HandleLBumper;
		inputManager.OnPressRBumper += HandleRBumper;
		inputManager.OnPressButton += HandleButton;
		animator = GetComponentInChildren<Animator> ();
		attackMeans = GetComponent<WizardAttackMeans> ();
	}

	void HandleLBumper()
	{
		// Debug.Log ("try casting fireball");
		if(!animator.GetBool("isCasting") && 
		   !animator.GetCurrentAnimatorStateInfo(0).IsName("isCasting")) {
			// Debug.Log ("casting spell" + magicID);

			Debug.Log ("[SPELL]: casting fireball");
			// StartCoroutine(castCoolDown());
			castFireball();
		}
	}

	void castFireball()
	{
		// cast one fireball
		Vector3 direction = transform.forward;
		attackMeans.AttackByDiretion (SpellDB.AttackID.fireball, direction);
	}

	void HandleRBumper()
	{
		// Debug.Log ("try casting spell");
		if(!animator.GetBool("isCasting") && 
		   !animator.GetCurrentAnimatorStateInfo(0).IsName("isCasting"))
		{
			Debug.Log ("[SPELL]: casting spell " + magicID);
			StartCoroutine(castCoolDown());

			castMagic(magicID);

		}
	}

	void castMagic(int magicID)
	{
		// cast a special spell by magic ID
		Vector3 direction = transform.forward;
		// NOT SAFE
		attackMeans.AttackByDiretion ((SpellDB.AttackID)magicID, direction);
	}

	IEnumerator castCoolDown()
	{
		animator.SetBool("isCasting", true);
		yield return new WaitForSeconds (Constants.MinCastCoolDown);
		animator.SetBool("isCasting", false);
	}

	void HandleButton()
	{
		magicID = inputManager.magicID_in;
	}

	void FixedUpdate()
	{
		move (inputManager.leftInput);

		// will overwrite the direction updated by move,
		// so the fireball/spell will cast in correct direction
		rotate (inputManager.rightInput);

		// show casting lines
		debug ();



	}

	void debug()
	{
		Color lineColor = Color.red;

		if(isCasting)
			lineColor = Color.green;
		Debug.DrawLine(transform.localPosition, 
		               transform.localPosition + 10.0f * transform.forward,
		               lineColor);
	}


	void move(Vector2 input)
	{
		Speed = Mathf.Abs (input.x + input.y) > 0 ? 1 : 0;
		if(!animator.GetBool("isCasting") && 
		   !animator.GetCurrentAnimatorStateInfo(0).IsName("isCasting") &&
			input.magnitude > 0 )
		{
			// move target object with left stick.
			float ratio = 7.0f;
			Vector3 newForward = new Vector3 (input.x, 0.0f, input.y).normalized;
			if(inputManager.rightInput.magnitude == 0)
				smoothRotate (newForward);
			transform.Translate( Vector3.right * ratio * Time.deltaTime * input.x, Space.World );
			//		transform.Rotate( Vector3.right, 500.0f * Time.deltaTime * inputDevice.Direction.Y, Space.World );
			transform.Translate( Vector3.forward *  ratio * Time.deltaTime * input.y, Space.World );
			//		transform.Rotate( Vector3.right, 500.0f * Time.deltaTime * inputDevice.RightStickY, Space.World );
		}
	}

	void rotate(Vector2 input)
	{
		if(!animator.GetBool("isCasting") && 
		   !animator.GetCurrentAnimatorStateInfo(0).IsName("isCasting") &&
		   input.magnitude > 0 )
		{
			Vector3 newForward = new Vector3 (input.x, 0.0f, input.y).normalized;
			smoothRotate (newForward);
		}
		// rotate target with right stick.
	}

	void smoothRotate(Vector3 vec_to)
	{
		//	transform.LookAt (transform.position + newForward);
		Vector3 vec_from = transform.forward;
		float minDeltaAngle = Constants.BasicPlayerAngularSpeed;

		// calculate new direction by
		Vector3 newDir = Vector3.RotateTowards(vec_from, vec_to, minDeltaAngle, 0.0F);

//		Debug.DrawRay(transform.position, newDir, Color.red);

		transform.rotation = Quaternion.LookRotation(newDir);

		//		rigidbody.AddTorque (torqueFactor * Vector3.Cross (vec_from, vec_to));

	}

}
