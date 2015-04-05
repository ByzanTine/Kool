using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VirtualButtonController : MonoBehaviour {
	public Button LeftTrigger;
	public Button RightTrigger;
	public Button LeftBumper;
	public Button RightBumper;
	public Button LeftJoystick;
	public Button RightJoystick;

	private PointerEventData pointer;
	// Use this for initialization
	void Start () {
		pointer = new PointerEventData (EventSystem.current);
		// ExecuteEvents.Execute (button.gameObject, pointer, ExecuteEvents.pointerDownHandler);

	}
	// public accessors 
	/// <summary>
	/// Simulate the event Presses the L trigger.
	/// </summary>
	public void PressLTrigger() {
		ExecuteEvents.Execute(LeftTrigger.gameObject, pointer, ExecuteEvents.pointerEnterHandler);
		ExecuteEvents.Execute(LeftTrigger.gameObject, pointer, ExecuteEvents.submitHandler);
	}
	/// <summary>
	///  Simulate the event Presses the R trigger.
	/// </summary>
	public void PressRTrigger() {
		ExecuteEvents.Execute(RightTrigger.gameObject, pointer, ExecuteEvents.pointerEnterHandler);
		ExecuteEvents.Execute(RightTrigger.gameObject, pointer, ExecuteEvents.submitHandler);
	}
	/// <summary>
	///  Simulate the event Presses the R bumper.
	/// </summary>
	public void PressRBumper() {
		ExecuteEvents.Execute(LeftBumper.gameObject, pointer, ExecuteEvents.pointerEnterHandler);
		ExecuteEvents.Execute(LeftBumper.gameObject, pointer, ExecuteEvents.submitHandler);
	}
	/// <summary>
	///  Simulate the event Presses the L bumper.
	/// </summary>
	public void PressLBumper() {
		ExecuteEvents.Execute(RightBumper.gameObject, pointer, ExecuteEvents.pointerEnterHandler);
		ExecuteEvents.Execute(RightBumper.gameObject, pointer, ExecuteEvents.submitHandler);
	}
	// TODO better animation
	/// <summary>
	/// Simulate the event Move Left Joystick
	/// </summary>
	public void MoveLeftJoystick() {
		ExecuteEvents.Execute(LeftJoystick.gameObject, pointer, ExecuteEvents.pointerEnterHandler);
		ExecuteEvents.Execute(LeftJoystick.gameObject, pointer, ExecuteEvents.submitHandler);
	}
	/// <summary>
	/// Simulate the event Move Right Joystick
	/// </summary>
	public void MoveRightJoystick() {
		ExecuteEvents.Execute(RightJoystick.gameObject, pointer, ExecuteEvents.pointerEnterHandler);
		ExecuteEvents.Execute(RightJoystick.gameObject, pointer, ExecuteEvents.submitHandler);
	}

}
