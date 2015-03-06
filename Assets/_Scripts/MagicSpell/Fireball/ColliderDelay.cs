using UnityEngine;
using System.Collections;

public class ColliderDelay : MonoBehaviour {

	// Use this for initialization
	private Collider[] colliders;
	void Start () {

		colliders = gameObject.GetComponents<Collider> ();
		foreach (Collider col in colliders)
		{
			col.enabled = false;
		}
		Invoke ("EnableCollider", Constants.COLLIDER_DELAY_SECONDS);//enable afte a second
	}
	
	// Update is called once per frame
	void EnableCollider () {
		foreach (Collider col in colliders)
		{
			col.enabled = true;
		}
	}
}
