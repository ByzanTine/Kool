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
	public int magicID_in = 1;

	// controller input events:
	public delegate void OnInput();
	public event OnInput OnPressRBumper;
	public event OnInput OnPressLBumper;
	public event OnInput OnPressButton;

	void Start()
	{}
	
	void Update()
	{
		var inputDevice = (InputManager.Devices.Count > playerNum) ? InputManager.Devices[playerNum] : null;
		if (inputDevice == null)
		{
			// If no controller exists for this cube, just make it translucent.
			// renderer.material.color = new Color( 1.0f, 1.0f, 1.0f, 0.2f );
			Debug.Log ("No devide detected for player:" + playerNum);
		}
		else
		{
			UpdateWithInputDevice( inputDevice );
		}

	}
	
	
	void UpdateWithInputDevice( InputDevice inputDevice )
	{
		buttonInput (inputDevice);
		directionInput (inputDevice);
	}
	
	void buttonInput(InputDevice inputDevice)
	{
		// if any buttons in X,Y,A,B is pressed down (one shot)
		if (inputDevice.AnyButton) 
		{
			Debug.Log ("Button pressed");
			if (inputDevice.Action1.WasPressed)
			{
				Debug.Log ("Button pressed: A");
				magicID_in = 1;
			}
			else
				if (inputDevice.Action2.WasPressed)
			{
				Debug.Log ("Button pressed: B");
				magicID_in = 2;
			}
			else
				if (inputDevice.Action3.WasPressed)
			{
				Debug.Log ("Button pressed: X");
				magicID_in = 3;
			}
			else
				if (inputDevice.Action4.WasPressed)
			{
				Debug.Log ("Button pressed: Y");
				magicID_in = 4;
			}
			else
			{
				// should occur when keep pressing
				Debug.Log ("Button pressed: pressing");
			}

			OnPressButton();
		}

		// if left or right bumper was pressed down
		if(inputDevice.LeftBumper.WasPressed)
		{
			OnPressLBumper();
		}

		if(inputDevice.RightBumper.WasPressed)
		{
			OnPressRBumper();
		}
	}
	
	void directionInput(InputDevice inputDevice )
	{
		leftInput = new Vector2 (inputDevice.LeftStickX, inputDevice.LeftStickY);

		rightInput = new Vector2 (inputDevice.RightStickX, inputDevice.RightStickY);

		if(inputDevice.Name == "Keyboard/Mouse")
		{
			Vector3 mousePos2D = Input.mousePosition;
			// Convert the mouse position to 3D world coordinates
			mousePos2D.z = -Camera.main.transform.position.z;
			Vector3 mousePos3D = Camera.main.ScreenToWorldPoint( mousePos2D );

//			Debug.Log ("input posision" + mousePos2D.ToString());
			rightInput = new Vector2(mousePos3D.x - transform.position.x,
			                         mousePos3D.z - transform.position.z);
		}

	}
}
