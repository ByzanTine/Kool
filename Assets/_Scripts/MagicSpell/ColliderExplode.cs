using UnityEngine;
using System.Collections;

public class ColliderExplode : MonoBehaviour {
	public LayerMask overlapLayer;
	public GameObject ExplodeEffect;
	// Use this for initialization
	void Start () {
		
		
		Instantiate (ExplodeEffect, transform.position, Quaternion.identity);;
		Collider[] co = Physics.OverlapSphere(transform.position, 1.0f, overlapLayer);

		// TODO Identify if the collider is pushable(namely only players)
		int MeetPlayer = 0;
		foreach (Collider collider in co){
			if (collider.gameObject.tag == TagList.Player){
				MeetPlayer++;
				Vector3 direction = collider.transform.position - transform.position;
				Vector3 appliedForce = direction.normalized * Globals.FORCE_MULTIPLIER;
				StartCoroutine(addTimeDecayForce(collider.attachedRigidbody, appliedForce, 0.5f));
				// collider.attachedRigidbody.AddExplosionForce(100.0f, transform.position, 0);
			}
		}
		Debug.Log ("[SPELL]: meet objects: " + MeetPlayer);
	}

	IEnumerator addTimeDecayForce(Rigidbody rigidbody, Vector3 Force, float time) {
		float timer = 0;
		while (timer < time) {

			timer += Time.fixedDeltaTime;
			rigidbody.AddForce(Force * (time - timer)/time);
			yield return new WaitForFixedUpdate();
		}
	}
}
