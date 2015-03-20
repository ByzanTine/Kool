using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ColliderExplode : MonoBehaviour {
	public LayerMask overlapLayer;
	public GameObject ExplodeEffect;
	public int spellID;
	public float ExplodeRadius = 1.0f;
	public GameObject caster;
	// Use this for initialization
	void Start () {

		if (ExplodeEffect) {
			Instantiate (ExplodeEffect, transform.position, Quaternion.identity);
			GenerateSphereCast (transform.position);
		}

	}

	/// <summary>
	/// Only works on players
	/// </summary>
	/// <param name="position">Position.</param>
	public void GenerateSphereCast(Vector3 position) {
		Collider[] co = Physics.OverlapSphere(position, ExplodeRadius, overlapLayer);
		
		// TODO Identify if the collider is pushable(namely only players)
		Debug.Log ("[Spell] start explositon");
		HashSet<int> hashtable = new HashSet<int>();// TODO Each player should have a different name
		if (caster)
			hashtable.Add (caster.GetInstanceID());// caster won't get hurt 
		foreach (Collider collider in co){
			Debug.Log ("[Spell] " + collider.gameObject.tag + "  " + collider.name);
			if (!hashtable.Contains(collider.gameObject.GetInstanceID())){
				hashtable.Add(collider.gameObject.GetInstanceID());
				
				
				PlayerSpellHandler PSH = collider.GetComponent<PlayerSpellHandler>();
				if (PSH) {
					PSH.onSpellTrigger(gameObject.transform.position, spellID);
				}
			}
		}
	}
//	IEnumerator addTimeDecayForce(Rigidbody rigidbody, Vector3 Force, float time) {
//		float timer = 0;
//		while (timer < time) {
//			timer += Time.fixedDeltaTime;
//			rigidbody.AddForce(Force * (time - timer)/time);
//			yield return new WaitForFixedUpdate();
//		}
//	}
}
