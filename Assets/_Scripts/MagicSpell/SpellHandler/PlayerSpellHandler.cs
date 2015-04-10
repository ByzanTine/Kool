using UnityEngine;
using System.Collections;

public class PlayerSpellHandler : MonoBehaviour {
	private PlayerData playerData;
	public GameObject FrozenEffectObj;

	// Use this for initialization
	void Start () {
		playerData = GetComponent<PlayerData> ();
		if (!playerData) {
			Debug.LogError("[Player] player data not there!");
		}
		GameObject frozenPrefab = Resources.Load ("PlayerEffect/FrozenEffect") as GameObject;
		FrozenEffectObj = Instantiate (frozenPrefab, transform.position, Quaternion.identity) as GameObject;
		FrozenEffectObj.transform.parent = transform;
		FrozenEffectObj.SetActive (false);
	}
	
	// Update is called once per frame
	public void onSpellTrigger (Vector3 spellPos, SpellDB.AttackID spellId) {
		// if fireball, deduct health
		Debug.Log ("[Spell] " + SpellDB.attackIDnames[(int)spellId] + "   on spell trigger");
		if (spellId == SpellDB.AttackID.iceball || spellId == SpellDB.AttackID.bigiceball
		    || spellId == SpellDB.AttackID.iceBurst) {
			playerData.DamageHP(SpellDB.GetSpellDamage(spellId));
			StartCoroutine(GetFrozen());
		}

		if (spellId == SpellDB.AttackID.meteor) {
			playerData.DamageHP(SpellDB.GetSpellDamage(spellId));
		}

		if (spellId == SpellDB.AttackID.fireball || spellId == SpellDB.AttackID.bigfireball) {
			Debug.Log ("[Spell] fireball is hit on " + this.name);
			Vector3 direction = transform.position - spellPos;
			Vector3 appliedForce = direction.normalized * Globals.FORCE_MULTIPLIER;
//			StartCoroutine(addTimeDecayForce(GetComponent<Rigidbody>(), appliedForce, 0.5f));
			StartCoroutine(addDistanceDecayForce(GetComponent<Rigidbody>(), appliedForce, 16f, spellPos));

			playerData.DamageHP(SpellDB.GetSpellDamage(spellId));
			Debug.Log ("[Spell] hit " + GetComponent<Collider>().name);
		}

//		if (spellID == 1){ // iceball id is 1
//			playerData.DamageHP(SpellDB.SPELL_DAMAGE[spellID]);
//			StartCoroutine(GetFrozen());
//		}


//		if (spellID == 2){ // iceburst id is 2
//			playerData.DamageHP(SpellDB.SPELL_DAMAGE[spellID]);
//			StartCoroutine(GetFrozen());
//		}

//		if (spellID == 3) { // meteor
//			playerData.DamageHP(SpellDB.SPELL_DAMAGE[spellID]);
//		}
	}
	
	
	IEnumerator GetFrozen() {
		playerData.frozen = true;
		FrozenEffectObj.SetActive (true);
		yield return new WaitForSeconds(3f); // waits 3 seconds
		playerData.frozen = false; // will make the update method pick up 
		FrozenEffectObj.SetActive (false);
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
