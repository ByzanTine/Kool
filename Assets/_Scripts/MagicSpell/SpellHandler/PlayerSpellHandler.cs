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
		if (spellID == 0){ // fireball id is 0
			Debug.Log ("[Spell] fireball is hit on " + this.name);
			Vector3 direction = transform.position - spellPos;
			Vector3 appliedForce = direction.normalized * Globals.FORCE_MULTIPLIER;
			StartCoroutine(addTimeDecayForce(GetComponent<Rigidbody>(), appliedForce, 0.5f));


			playerData.DamageHP(Constants.SPELL_DAMAGE[spellID]);
			Debug.Log ("[Spell] hit " + GetComponent<Collider>().name);
		}

		Debug.Log("[Spell] Enter spell trigger");
		// if ice ball 
		// TODO
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
