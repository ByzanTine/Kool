using UnityEngine;
using System.Collections;

public class PlayerSpellHandler : MonoBehaviour {
	private PlayerData playerData;

	// Use this for initialization
	void Start () {
		playerData = GetComponent<PlayerData> ();
		if (!playerData) {
			Debug.LogError("[Player] player data not there!");
		}
	}
	
	// Update is called once per frame
	public void onSpellTrigger (Vector3 spellPos, int spellID) {
		// if fireball, deduct health
		Debug.Log ("[Spell] " + SpellDB.attackIDnames[spellID] + "   on spell trigger");

		if (spellID == 0){ // fireball id is 0
			Debug.Log ("[Spell] fireball is hit on " + this.name);
			Vector3 direction = transform.position - spellPos;
			Vector3 appliedForce = direction.normalized * Globals.FORCE_MULTIPLIER;
//			StartCoroutine(addTimeDecayForce(GetComponent<Rigidbody>(), appliedForce, 0.5f));
			StartCoroutine(addDistanceDecayForce(GetComponent<Rigidbody>(), appliedForce, 4f, spellPos));

			playerData.DamageHP(Constants.SPELL_DAMAGE[spellID]);
			Debug.Log ("[Spell] hit " + GetComponent<Collider>().name);
		}

		if (spellID == 1){ // iceball id is 1
			playerData.DamageHP(Constants.SPELL_DAMAGE[spellID]);
			StartCoroutine(GetFrozen());
		}


		if (spellID == 2){ // iceburst id is 2
			playerData.DamageHP(Constants.SPELL_DAMAGE[spellID]);
			StartCoroutine(GetFrozen());
		}
	}
	
	
	IEnumerator GetFrozen() {
		playerData.frozen = true;
		yield return new WaitForSeconds(3f); // waits 3 seconds
		playerData.frozen = false; // will make the update method pick up 
	}


	

	IEnumerator addDistanceDecayForce(Rigidbody rigidbody, Vector3 Force, float range, Vector3 explosionPos) {
		float distance = (explosionPos - rigidbody.transform.position).magnitude;
		while (distance < range) {
			// Debug.Log ("explosion force: " + Force * (range - distance)/range);
			distance = (explosionPos - rigidbody.transform.position).magnitude;
			rigidbody.AddForce(Force * (range - distance)/range);
			yield return new WaitForFixedUpdate();
		}
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
