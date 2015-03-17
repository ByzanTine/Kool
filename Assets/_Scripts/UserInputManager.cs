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

	// Trigger and bumpers
	public event OnInput OnPressMainSkill;
	public event OnInput OnPressSubSkill;
	public event OnInput OnReleaseSubSkill;
	public event OnInput OnPressHit;
	public event OnInput OnPressRunning;
	public event OnInput OnReleaseRunning;
	
	// Buttons: X, Y, A, B
	public event OnInput OnPressButton;
	public event OnInput OnReleaseButton;

	
	// input lock of each: left, right, buttons;
	private bool[] ctrlLocks = new bool[3]{false, false, false};
	void Start()
	{}
	
	void Update()
	{
		var inputDevice = (InputManager.Devices.Count > playerNum) ? InputManager.Devices[playerNum] : null;
		if (inputDevice == null)
		{
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
		GetTriggerBumperInput (inputDevice);
		GetDirectionInput (inputDevice);
	}
	
	void GetButtonInput(InputDevice inputDevice)
	{
		if(ctrlLocks[2]) return;

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

		if(inputDevice.AnyButton.WasPressed)
		{
			Debug.Log ("Button pressed");
			
			OnPressButton();
		}

		if(inputDevice.Action1.WasReleased
		   || inputDevice.Action2.WasReleased
		   || inputDevice.Action3.WasReleased
		   || inputDevice.Action4.WasReleased)
		{
			OnReleaseButton();
			Debug.Log ("Button released" + inputDevice.AnyButton.Value);
		}


	}

	void GetTriggerBumperInput(InputDevice inputDevice)
	{
		if(inputDevice.LeftBumper.WasPressed)
		{
			OnPressHit();
		}
		
		if(inputDevice.RightBumper.WasPressed)
		{
			OnPressRunning();
		}

		if(inputDevice.RightBumper.WasReleased)
		{
			OnReleaseRunning();
		}

		if(inputDevice.LeftTrigger.WasPressed)
		{
			OnPressMainSkill();
		}
		
		if(inputDevice.RightTrigger.WasPressed)
		{
			OnPressSubSkill();
		}

		if(inputDevice.RightTrigger.WasPressed)
		{
			OnReleaseSubSkill();
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
			Vector3 mousePos3D = GetWorldPositionOnPlane(mousePos2D, 0);
			rightInput = new Vector2(mousePos3D.x - transform.position.x,
			                         mousePos3D.z - transform.position.z);
		}

		if(ctrlLocks[0]) 
		{
			leftInput *= 0.0f;
			Debug.Log ("Moving control locked");
		}
		if(ctrlLocks[1])
		{
			rightInput *= 0.0f;
			Debug.Log ("Direction control locked");
		}

	}

	Vector3 GetWorldPositionOnPlane(Vector3 screenPosition, float y) {
		Ray ray = Camera.main.ScreenPointToRay(screenPosition);
		Plane xy = new Plane(Vector3.up, new Vector3(0, y, 0));
		float distance;
		xy.Raycast(ray, out distance);
		return ray.GetPoint(distance);
	}

	IEnumerator LockParameter(int lockIndex, float period)
	{
		ctrlLocks[lockIndex] = true;

		yield return new WaitForSeconds (period);

		ctrlLocks[lockIndex] = false;
	}

	public void LockLeftInput(float period)
	{
		if(!ctrlLocks[0])
		{
			StartCoroutine (LockParameter (0, period));
		}
	}

	public void LockRightInput(float period)
	{
		if(!ctrlLocks[1])
		{
			StartCoroutine (LockParameter (1, period));
		}
	}

	public void LockButton(float period)
	{
		if(!ctrlLocks[2])
		{
			StartCoroutine (LockParameter (2, period));
		}
	}
}
