using UnityEngine;
using System.Collections;

public class DamageNumber : MonoBehaviour {
	Vector3 target;
	private Vector3 startMarker;
	private Vector3 endMarker;
	private float speed = 4.0F;
	private float startTime;
	private float journeyLength;
	bool moving = false;
	public int number = 0;
	Color delta;
	// Use this for initialization
	void Start () {
		Vector3 dest = transform.position;
		dest.y += 3;
		moveTo (dest);
		colorFade (GetComponent<TextMesh> ().color, Color.clear);
		GetComponent<TextMesh> ().text = "" + number;
	}

	void Update () {
		if (moving) {
			float distCovered = (Time.time - startTime) * speed;
			float fracJourney = distCovered / journeyLength;
			Vector3 d = transform.position - Vector3.Lerp (startMarker, endMarker, fracJourney);
			transform.position = Vector3.Lerp (startMarker, endMarker, fracJourney);
			Color next = GetComponent<TextMesh> ().color;
			next.a += delta.a;
			GetComponent<TextMesh> ().color = next;
			transform.localScale = transform.localScale * 1.05f;
		}
	}
	void moveTo(Vector3 destination){
		startMarker = transform.position;
		endMarker = destination;
		startTime = Time.time;
		journeyLength = Vector3.Distance(startMarker, endMarker);
		moving = true;
	}
	void colorFade(Color from, Color to, float time = 0.5f){
		delta = (to - from);
		delta.a /= time/Time.deltaTime;
	}
}
