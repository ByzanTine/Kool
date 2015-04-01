using UnityEngine;
using System.Collections;

public class Reflector : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnTriggerEnter (Collider collider) {

		ReflectHandler reflectHandler = collider.GetComponent<ReflectHandler> ();
		if (reflectHandler) {
			// not exactly accurate 
			Vector3 normal = collider.transform.position - transform.position;
			reflectHandler.onReflectHandler(normal);
		}
	}
}
