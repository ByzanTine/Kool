using UnityEngine;
using System.Collections;

public class CreditScrollUp : MonoBehaviour {
	private float speed = 50f;
	private float maxY = 600;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y < maxY) {
			transform.Translate (Vector3.up * Time.deltaTime * speed);

		} else {
			CreditEnd();
		}
	}	
	void CreditEnd(){
		print ("End");
	}
}
