using UnityEngine;
using System.Collections;

public class ColliderExplode : MonoBehaviour {
	public LayerMask overlapLayer;
	public GameObject ExplodeEffect;
	public int spellID;
	// Use this for initialization
	void Start () {
		
		
		Instantiate (ExplodeEffect, transform.position, Quaternion.identity);;
		Collider[] co = Physics.OverlapSphere(transform.position, 1.0f, overlapLayer);

		// TODO Identify if the collider is pushable(namely only players)
		int MeetPlayer = 0;
		print ("start explositon");
		foreach (Collider collider in co){
			print (collider.gameObject.tag + "  " + collider.name);
			if (collider.gameObject.tag == TagList.Player){
				MeetPlayer++;
				Vector3 direction = collider.transform.position - transform.position;
				Vector3 appliedForce = direction.normalized * Globals.FORCE_MULTIPLIER;
				StartCoroutine(addTimeDecayForce(collider.attachedRigidbody, appliedForce, 0.5f));
				// collider.attachedRigidbody.AddExplosionForce(100.0f, transform.position, 0);

				playerData pD = collider.GetComponent<playerData>();
				pD.damageHP(Constants.SPELL_DAMAGE[spellID]);
				print ("hit " + collider.name);
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
