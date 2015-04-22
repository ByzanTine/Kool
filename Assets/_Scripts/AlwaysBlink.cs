using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AlwaysBlink : MonoBehaviour {
	private Image img;
	public delegate void SimulatePressButton();
	TutorialView TV;
	public int playerId;
	public Sprite spt;
	// Use this for initialization
	void Start () {
		img = GetComponent<Image> ();
		Blink (100, 1f);
		TV = UnityEngine.Object.FindObjectOfType<TutorialView>();
	}

	void Update(){
		if (TV.addSecondButton) {
			img.sprite = spt;
			img.enabled = true;
		} else {
			img.enabled = false;
		}
	}

	public void StayRed() {
		img.color = Color.red;
	}
	
	public void StayWhite() {
		img.color = Color.white;
	}
	
	public void Blink(int times = 5, float interval = 0.5f) {
		StartCoroutine (
			BlinkCo(times, interval));
	}
	// helper func
	private IEnumerator BlinkCo(int times, float interval = 1.0f) {
		for (int i = 0; i < times; i++) {	
			if (!TutorialView.StopBlinking[playerId]){
				StayRed ();
				yield return new WaitForSeconds (0.2f);
				StayWhite ();
			}
			else {
				StayWhite();
			}
			yield return new WaitForSeconds(interval);

		}
	}

}
