using UnityEngine;
using System.Collections;
// this script is intended to control if the player is onfire or on block
// deduct the health if on fire
public class DangerControl : MonoBehaviour {

//	public int health;

	private PlayerData playerData;

	public enum STAND_STATE
	{
		OnDanger,
		OnSafe
	};
	public STAND_STATE standState;

	// health deduction guard
	private bool damaging = false;
	
	private GameObject DamagingEffect;
	private float lastTriggerStayTimer = 0.0f;

	void Start () {
		// DamagingEffect = GameObject.FindGameObjectWithTag (TagList.DamagingEffect);
		DamagingEffect = transform.FindChild ("DamagingFlame").gameObject;
		standState = STAND_STATE.OnSafe;

		playerData = GetComponent<PlayerData> ();

	}
	// Trigger Control
	void OnTriggerStay(Collider coll) {

		if (coll.tag == TagList.GroundBlock) {
			Debug.Log("Trigger Stay");
			standState = STAND_STATE.OnSafe;
			lastTriggerStayTimer = Time.time;
		}

	}


	//Continously damaged if the player is stand on Fire
	void FixedUpdate (){
		if (Time.time - lastTriggerStayTimer > 0.2f) {
			standState = STAND_STATE.OnDanger;
		}
		if (standState == STAND_STATE.OnDanger && !damaging){
			damaging = true;
			
			StartCoroutine(DangerDamage());
			
		}
		DamagingEffect.SetActive (standState == STAND_STATE.OnDanger);

	}
	//Damage player with the Damage Interval and damage health point
	//This two parameter should be controled by outer script
	IEnumerator DangerDamage(){
		yield return new WaitForSeconds(1.0f);
		if (standState == STAND_STATE.OnDanger) {
			playerData.DamageHP(0.05f);
		}
		damaging = false;
	}
}
