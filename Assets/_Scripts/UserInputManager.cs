using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using InControl;

public class UserInputManager : MonoBehaviour {


	// variable set in Inspector
	public int playerNum = 0;

	// variables for player controller and GUI
	[HideInInspector]
	public Vector2 leftInput;
	[HideInInspector]
	public Vector2 rightInput;
	[HideInInspector]
	public int button_id = -1;

	[SerializeField]
	private EventSystem eventSystem = null;

	// controller input events:
	public delegate void OnInput();

	// Game control: Trigger and bumpers
	public event OnInput OnPressMainSkill;
	public event OnInput OnPressSubSkill;
	public event OnInput OnReleaseSubSkill;
	public event OnInput OnPressHit;
	public event OnInput OnPressSwapIceFire;

	public event OnInput OnPressRunning;
	public event OnInput OnReleaseRunning;

	// UI control:
	public event OnInput OnPressConfirm;
	public event OnInput OnPressBack;
	public event OnInput OnPressNavUp;
	public event OnInput OnPressNavDown;

	// Buttons: X, Y, A, B
	public event OnInput OnPressButton;
	public event OnInput OnReleaseButton;
	private InputDevice inputDevice;

	// input lock of each: 0left, 1right, 2buttons, 3Ltrigger, 4RTrigger, 5Lbumper, 6Rbumper;
	private bool[] ctrlLocks = new bool[7]{false, false, false, false, false, false, false};
	void Start()
	{}
	
	void Update()
	{
		inputDevice = (InputManager.Devices.Count > playerNum) ? InputManager.Devices[playerNum] : null;
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
				if(OnPressConfirm != null) OnPressConfirm();

				if(eventSystem != null && eventSystem.currentSelectedGameObject != null)
				{
					Debug.Log(eventSystem.currentSelectedGameObject.ToString());
					ExecuteEvents.Execute (eventSystem.currentSelectedGameObject, null, ExecuteEvents.submitHandler);
				}

				button_id = 1;
			}
			else
				if (inputDevice.Action2.WasPressed)
			{
				Debug.Log ("Button pressed: B");
				if(OnPressBack != null) OnPressBack();

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
			
			if(OnPressButton != null) OnPressButton();
		}

		if(inputDevice.Action1.WasReleased
		   || inputDevice.Action2.WasReleased
		   || inputDevice.Action3.WasReleased
		   || inputDevice.Action4.WasReleased)
		{
			if(OnReleaseButton != null) OnReleaseButton();
			Debug.Log ("Button released" + inputDevice.AnyButton.Value);
		}


	}

	void GetTriggerBumperInput(InputDevice inputDevice)
	{

		if(inputDevice.LeftBumper.WasPressed && !ctrlLocks[5])
		{
			if(OnPressSwapIceFire != null) OnPressSwapIceFire();
		}
		
		if(inputDevice.RightBumper.WasPressed && !ctrlLocks[6])
		{
			if(OnPressSubSkill != null) OnPressSubSkill();
		}

		if(inputDevice.RightTrigger.WasPressed && !ctrlLocks[4])
		{
			if(OnPressMainSkill != null) OnPressMainSkill();
		}

		if(inputDevice.LeftTrigger.WasPressed && !ctrlLocks[3])
		{
			if(OnPressRunning != null) OnPressRunning();
		}

		if(inputDevice.LeftTrigger.WasReleased)
		{
			if(OnReleaseRunning != null) OnReleaseRunning();
		}


	}

	void GetDirectionInput(InputDevice inputDevice)
	{
		leftInput = new Vector2 (inputDevice.LeftStickX, inputDevice.LeftStickY);

		rightInput = new Vector2 (inputDevice.RightStickX, inputDevice.RightStickY);

		if(inputDevice.DPadLeft.WasPressed || inputDevice.LeftStickX < -0.8)
		{
			if(OnPressNavUp != null) OnPressNavUp();
		}

		if(inputDevice.DPadRight.WasPressed || inputDevice.LeftStickX > 0.8)
		{
			if(OnPressNavDown != null) OnPressNavDown();
		}


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

	private void LockLeftInput(float period = float.MaxValue)
	{
		if(!ctrlLocks[0])
		{
			StartCoroutine (LockParameter (0, period));
		}
	}

	private void LockRightInput(float period = float.MaxValue)
	{
		if(!ctrlLocks[1])
		{
			StartCoroutine (LockParameter (1, period));
		}
	}

	private void LockButton(float period = float.MaxValue)
	{
		if(!ctrlLocks[2])
		{
			StartCoroutine (LockParameter (2, period));
		}
	}


	private void LockTriggerAndBumper(float period = float.MaxValue)
	{
		if(!ctrlLocks[3])
		{
			StartCoroutine (LockParameter (3, period));
		}

		if(!ctrlLocks[4])
		{
			StartCoroutine (LockParameter (4, period));
		}

		if(!ctrlLocks[5])
		{
			StartCoroutine (LockParameter (5, period));
		}

		if(!ctrlLocks[6])
		{
			StartCoroutine (LockParameter (6, period));
		}
	}

	private void LockAllControl(float period = float.MaxValue)
	{
		LockLeftInput (period);
		LockRightInput (period);
		LockButton (period);
		LockTriggerAndBumper (period);
	}

	public enum InputSource {AllControl, LStick, RStick, Button,
		LTrigger, RTrigger, LBumper, RBumper, LtriggerAndRTrigger, None};
	public void LockControl(InputSource inputSource,float period = float.MaxValue)
	{
		switch(inputSource)
		{
		case InputSource.AllControl:
			LockAllControl(period);
			break;

		case InputSource.Button:
			LockButton(period);
			break;

		case InputSource.LStick:
			LockLeftInput(period);
			break;

		case InputSource.RStick:
			LockRightInput(period);
			break;

		case InputSource.LTrigger:
			if(!ctrlLocks[3])
			{
				StartCoroutine (LockParameter (3, period));
			}
			break;

		case InputSource.RTrigger:
			if(!ctrlLocks[4])
			{
				StartCoroutine (LockParameter (4, period));
			}
			break;

		case InputSource.LBumper:
			if(!ctrlLocks[5])
			{
				StartCoroutine (LockParameter (5, period));
			}
			break;
		
		case InputSource.RBumper:
			if(!ctrlLocks[6])
			{
				StartCoroutine (LockParameter (6, period));
			}
			break;
		
		default:
			break;
		}
	}

	public bool CheckInputControl (UserInputManager.InputSource input){
		bool returnV = false;
		if (inputDevice == null)
			return false;
		switch(input)
		{
			case InputSource.LStick:
				if (inputDevice.LeftStickX != 0 && inputDevice.LeftStickY != 0)
				returnV = true;
				break;

			case InputSource.RStick:
				if (inputDevice.RightStickX != 0 && inputDevice.RightStickY != 0)
				returnV = true;
				break;

			case InputSource.LTrigger:
				returnV = inputDevice.LeftTrigger.WasPressed;
			break;

			case InputSource.RTrigger:
				returnV = inputDevice.RightTrigger.WasPressed;
			break;

			case InputSource.LBumper:
				returnV = inputDevice.LeftBumper.WasPressed;
			break;

			case InputSource.RBumper:
				returnV = inputDevice.RightBumper.WasPressed;
			break;

			case InputSource.LtriggerAndRTrigger:
				returnV = inputDevice.RightTrigger.WasPressed & inputDevice.LeftTrigger.IsPressed;
			break;

			default:
				returnV = false;
			break;
		}
		return returnV;
	}

	// input lock of each: 0left, 1right, 2buttons, 3Ltrigger, 4RTrigger, 5Lbumper, 6Rbumper;
	public void UnlockControl(InputSource inputSource)
	{
		switch(inputSource)
		{
		case InputSource.AllControl:
			UnlockAllControl();
			break;

		case InputSource.LStick:
			ctrlLocks[0] = false;
			break;
			
		case InputSource.RStick:
			ctrlLocks[1] = false;
			break;

		case InputSource.Button:
			ctrlLocks[2] = false;
			break;
			
		case InputSource.LTrigger:
			ctrlLocks[3] = false;
			break;
			
		case InputSource.RTrigger:
			ctrlLocks[4] = false;
			break;
			
		case InputSource.LBumper:
			ctrlLocks[5] = false;
			break;
			
		case InputSource.RBumper:
			ctrlLocks[6] = false;
			break;
			
		default:
			break;
		}
	}

	private void UnlockAllControl()
	{
		for(int i = 0; i < ctrlLocks.Length; ++i)
		{
			ctrlLocks[i] = false;
		}
	} 
}
