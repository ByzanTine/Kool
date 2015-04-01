using UnityEngine;
using System.Collections;

public class PlayerTag : MonoBehaviour {
	Vector3 target;
	private Vector3 startMarker;
	private Vector3 endMarker;
	private float speed = 8.0F;
	private float startTime;
	private float journeyLength;
	bool moving = false;
	public string pos = "CENTER";
	public int posId;
	public Vector3 initPos;
	// Use this for initialization
	void Start () {
		initPos = transform.position;
		pos = "CENTER";
	}

	void ready(){
		GameObject go = transform.Find("Ready").gameObject;
		go.GetComponent<TextMesh>().text = "Ready";
	}
	void notReady(){
		GameObject go = transform.Find("Ready").gameObject;
		go.GetComponent<TextMesh>().text = "Not Ready";
	}
	// Update is called once per frame
	void Update () {
		if (moving) {
			float distCovered = (Time.time - startTime) * speed;
			float fracJourney = distCovered / journeyLength;
			transform.position = Vector3.Lerp(startMarker, endMarker, fracJourney);
		}
	}
	public void moveTo(Vector3 destination){
		startMarker = transform.position;
		endMarker = destination;
		startTime = Time.time;
		journeyLength = Vector3.Distance(startMarker, endMarker);
		moving = true;
	}
}
