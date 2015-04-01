using UnityEngine;
using System.Collections;

public class ReflectHandler : MonoBehaviour {
	private float reflectCoolDown = 0.2f; 
	private float timer;
	void Start () {
		timer = Time.time;
	}
	public void onReflectHandler(Vector3 normal) {
		if (Time.time - timer > reflectCoolDown) { 
			timer = Time.time;
			GetComponent<Rigidbody>().velocity = Vector3.Reflect (GetComponent<Rigidbody>().velocity, normal);
		}
	}
}
