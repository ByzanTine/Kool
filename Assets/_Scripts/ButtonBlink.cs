using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonBlink : MonoBehaviour {

	private Button button;
	public delegate void SimulatePressButton();
	private PointerEventData pointer;
	public int playerId;
	// Use this for initialization
	void Start () {
		button = GetComponent<Button> ();
		pointer = new PointerEventData (EventSystem.current);
		Blink (90, 1f);
	}

	public void StayRed() {
		ExecuteEvents.Execute(button.gameObject, pointer, ExecuteEvents.pointerEnterHandler);
	}

	public void ReleaseRed() {
		ExecuteEvents.Execute(button.gameObject, pointer, ExecuteEvents.submitHandler);
	}

	public void Blink(int times = 5, float interval = 0.5f) {
		StartCoroutine (
			BlinkCo(new SimulatePressButton(StayRed), new SimulatePressButton(ReleaseRed), times, interval));
	}
	// helper func
	private IEnumerator BlinkCo(SimulatePressButton stay, SimulatePressButton release, int times, float interval = 1.0f) {
		bool StayRedBefore = false;
		for (int i = 0; i < times; i++) {	
			if (!TutorialView.StopBlinking[playerId]){
				if (StayRedBefore){
					release();
					stay();
					release();
					StayRedBefore = false;
				}
				else {
					stay();
					release();
				}
			}
			else {
				StayRedBefore = true;
				stay();
			}
			yield return new WaitForSeconds(interval);
		}
	}
}


