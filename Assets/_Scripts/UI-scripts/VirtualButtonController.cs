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

	public delegate void SimulatePressButton();

	private PointerEventData pointer;
	// Use this for initialization
	void Start () {
		pointer = new PointerEventData (EventSystem.current);

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


	/// <summary>
	/// Blinks the L trigger.
	/// </summary>
	/// <param name="times">Number of times to blink.</param>
	/// <param name="interval">Time Interval for each blink.</param>
	public void BlinkLTrigger(int times = 5, float interval = 0.5f) {
		StartCoroutine (
			BlinkCo(new SimulatePressButton(PressLTrigger), times, interval));
	}
	
	/// <summary>
	/// Blinks the R trigger.
	/// </summary>
	/// <param name="times">Number of times to blink.</param>
	/// <param name="interval">Time Interval for each blink.</param>
	public void BlinkRTrigger(int times = 5, float interval = 0.5f) {
		StartCoroutine (
			BlinkCo(new SimulatePressButton(PressRTrigger), times, interval));
	}
	/// <summary>
	/// Blinks the L bumper.
	/// </summary>
	/// <param name="times">Number of times to blink.</param>
	/// <param name="interval">Time Interval for each blink.</param>
	public void BlinkLBumper(int times = 5, float interval = 0.5f) {
		StartCoroutine (
			BlinkCo(new SimulatePressButton(PressLBumper), times, interval));
	}
	/// <summary>
	/// Blinks the R bumper.
	/// </summary>
	/// <param name="times">Number of times to blink.</param>
	/// <param name="interval">Time Interval for each blink.</param>
	public void BlinkRBumper(int times = 5, float interval = 0.5f) {
		StartCoroutine (
			BlinkCo(new SimulatePressButton(PressRBumper), times, interval));
	}
	/// <summary>
	/// Blinks the L joystick.
	/// </summary>
	/// <param name="times">Number of times to blink.</param>
	/// <param name="interval">Time Interval for each blink.</param>
	public void BlinkLJoystick(int times = 5, float interval = 0.5f) {
		StartCoroutine (
			BlinkCo(new SimulatePressButton(MoveLeftJoystick), times, interval));
	}
	/// <summary>
	/// Blinks the R joystick.
	/// </summary>
	/// <param name="times">Number of times to blink.</param>
	/// <param name="interval">Time Interval for each blink.</param>
	public void BlinkRJoystick(int times = 5, float interval = 0.5f) {
		StartCoroutine (
			BlinkCo(new SimulatePressButton(MoveRightJoystick), times, interval));
	}
	// helper func
	private IEnumerator BlinkCo(SimulatePressButton func, int times, float interval = 1.0f) {
		for (int i = 0; i < times; i++) {	
			func();
			yield return new WaitForSeconds(interval);
		}
	}
}
