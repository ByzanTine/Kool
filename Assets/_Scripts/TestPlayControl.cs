using UnityEngine;
using System.Collections;
using InControl;


public class TestPlayControl : MonoBehaviour {
	// for testing

	// is casting (fireball or special spell) or not, sync with animator in child model
	public bool isCasting = false;

	// local status
	private int magicID = 1;

	private UserInputManager inputManager;
	void Start()
	{
		// mapping input events
		inputManager = GetComponent<UserInputManager> ();
		inputManager.OnPressLBumper += HandleLBumper;
		inputManager.OnPressRBumper += HandleRBumper;
		inputManager.OnPressButton += HandleButton;
	}

	void HandleLBumper()
	{
		Debug.Log ("try casting fireball");
		if(!isCasting)
		{
			Debug.Log ("casting fireball");
			StartCoroutine(castCoolDown());
			castFireball();
		}
	}

	void castFireball()
	{
		// cast one fireball
	}

	void HandleRBumper()
	{
		Debug.Log ("try casting spell");
		if(!isCasting)
		{
			Debug.Log ("casting spell" + magicID);
			StartCoroutine(castCoolDown());
			castMagic(magicID);
		}
	}

	void castMagic(int magicID)
	{
		// cast a special spell by magic ID
	}

	IEnumerator castCoolDown()
	{
		isCasting = true;
		yield return new WaitForSeconds (Constants.minCastCoolDown);
		isCasting = false;
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
		if(Mathf.Abs(input.x + input.y) > 0)
		{
			// move target object with left stick.
			float ratio = 7.0f;
			transform.LookAt (transform.position + new Vector3(input.x,0.0f, input.y));
			transform.Translate( Vector3.right * ratio * Time.deltaTime * input.x, Space.World );
			//		transform.Rotate( Vector3.right, 500.0f * Time.deltaTime * inputDevice.Direction.Y, Space.World );
			transform.Translate( Vector3.forward *  ratio * Time.deltaTime * input.y, Space.World );
			//		transform.Rotate( Vector3.right, 500.0f * Time.deltaTime * inputDevice.RightStickY, Space.World );
		}
	}
	void rotate(Vector2 input)
	{
		// rotate target with right stick.
		transform.LookAt (transform.position + new Vector3(input.x,0.0f, input.y));
	}


}
