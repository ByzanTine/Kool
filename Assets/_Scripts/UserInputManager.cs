using UnityEngine;
using System.Collections;
using InControl;

public class UserInputManager : MonoBehaviour {


	// variable set in Inspector
	public int playerNum;

	// variables for player controller and GUI
	[HideInInspector]
	public Vector2 leftInput;
	[HideInInspector]
	public Vector2 rightInput;
	[HideInInspector]
	public int button_id = -1;

	// controller input events:
	public delegate void OnInput();
	public event OnInput OnPressSubSkill;
	public event OnInput OnPressMainSkill;
	public event OnInput OnReleaseButton;
	public event OnInput OnPressButton;


	// input lock of each: left, right, buttons;
	private bool lockLeft = false;
	private bool lockRight = false;
	private bool lockButton = false;
	void Start()
	{}
	
	void Update()
	{
		var inputDevice = (InputManager.Devices.Count > playerNum) ? InputManager.Devices[playerNum] : null;
		if (inputDevice == null)
		{
			// If no controller exists for this cube, just make it translucent.
			// renderer.material.color = new Color( 1.0f, 1.0f, 1.0f, 0.2f );
//			Debug.Log ("No devide detected for player:" + playerNum);
		}
		else
		{
			UpdateWithInputDevice( inputDevice );
		}

	}
	
	
	void UpdateWithInputDevice( InputDevice inputDevice )
	{
		GetButtonInput (inputDevice);
		GetDirectionInput (inputDevice);
	}
	
	void GetButtonInput(InputDevice inputDevice)
	{
		if(lockButton) return;

		if(inputDevice.AnyButton.WasPressed)
		{
			Debug.Log ("Button pressed");

			OnPressButton();
		}
		if(inputDevice.Action3.WasReleased)
		{
			OnReleaseButton();
			Debug.Log ("Button released" + inputDevice.AnyButton.Value);
		}

		// if any buttons in X,Y,A,B is pressed down (one shot)
		if (inputDevice.AnyButton) 
		{

			if (inputDevice.Action1.WasPressed)
			{
				Debug.Log ("Button pressed: A");
				button_id = 1;
			}
			else
				if (inputDevice.Action2.WasPressed)
			{
				Debug.Log ("Button pressed: B");
				button_id = 2;
			}
			else
				if (inputDevice.Action3.WasPressed)
			{
				Debug.Log ("Button pressed: X");
				button_id = 3;
			}
			else
				if (inputDevice.Action4.WasPressed)
			{
				Debug.Log ("Button pressed: Y");
				button_id = 4;
			}
			else
			{
				// should occur when keep pressing
				Debug.Log ("Button pressed: pressing");
			}
		}

		if(inputDevice.LeftBumper.WasPressed)
		{
			OnPressMainSkill();
		}

		if(inputDevice.RightBumper.WasPressed)
		{
			OnPressSubSkill();
		}
	}
	
	void GetDirectionInput(InputDevice inputDevice )
	{
		leftInput = new Vector2 (inputDevice.LeftStickX, inputDevice.LeftStickY);

		rightInput = new Vector2 (inputDevice.RightStickX, inputDevice.RightStickY);

		if(inputDevice.Name == "Keyboard/Mouse")
		{
			Vector3 mousePos2D = Input.mousePosition;
			// Convert the mouse position to 3D world coordinates
//			mousePos2D.z = mousePos2D.y;
//			mousePos2D.y = -Camera.main.transform.position.y;
//			Vector3 mousePos3D = Camera.main.ScreenToWorldPoint( mousePos2D );
			Vector3 mousePos3D = GetWorldPositionOnPlane(mousePos2D, 0);

			// Debug.Log ("input posision" + mousePos2D.ToString() + "\t\n" + mousePos3D.ToString());
			rightInput = new Vector2(mousePos3D.x - transform.position.x,
			                         mousePos3D.z - transform.position.z);
		}

		if(lockLeft) 
		{
			leftInput *= 0.0f;
			Debug.Log ("Moving control locked");
		}
		if(lockRight)
		{
			rightInput *= 0.0f;
		}

	}

	Vector3 GetWorldPositionOnPlane(Vector3 screenPosition, float y) {
		Ray ray = Camera.main.ScreenPointToRay(screenPosition);
		Plane xy = new Plane(Vector3.up, new Vector3(0, y, 0));
		float distance;
		xy.Raycast(ray, out distance);
		return ray.GetPoint(distance);
	}

	IEnumerator LockParameter(int isLeft, float period)
	{
		if(isLeft == 1)
		{
			lockLeft = true;
		}
		else
			lockRight = true;

		yield return new WaitForSeconds (period);

		if(isLeft == 1)
		{
			lockLeft = false;
		}
		else
			lockRight = false;
	}

	public void LockLeftInput(float period)
	{
		if(!lockLeft)
		{
			StartCoroutine (LockParameter (1, period));
		}
	}

	public void LockRightInput(float period)
	{
		if(!lockRight)
		{
			StartCoroutine (LockParameter (0, period));
		}
	}

	public void LockButton(float period)
	{
		if(!lockButton)
		{
			StartCoroutine (LockParameter (2, period));
		}
	}
}
