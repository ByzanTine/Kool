using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonBlink : MonoBehaviour {

	private Button button;
	public delegate void SimulatePressButton();
	private PointerEventData pointer;
	// Use this for initialization
	void Start () {
		button = GetComponent<Button> ();
		pointer = new PointerEventData (EventSystem.current);
		Blink (90, 0.8f);
	}


	public void Press() {
		ExecuteEvents.Execute(button.gameObject, pointer, ExecuteEvents.pointerEnterHandler);
		ExecuteEvents.Execute(button.gameObject, pointer, ExecuteEvents.submitHandler);
	}

	public void Blink(int times = 5, float interval = 0.5f) {
		StartCoroutine (
			BlinkCo(new SimulatePressButton(Press), times, interval));
	}
	// helper func
	private IEnumerator BlinkCo(SimulatePressButton func, int times, float interval = 1.0f) {
		for (int i = 0; i < times; i++) {	
			func();
			yield return new WaitForSeconds(interval);
		}
	}
}


