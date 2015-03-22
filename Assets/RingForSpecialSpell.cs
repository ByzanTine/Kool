using UnityEngine;
using System.Collections;

public class RingForSpecialSpell : MonoBehaviour {
	private PlayerData PD;
	public GameObject ring;
	// Use this for initialization
	void Start () {
		PD = GetComponent<PlayerData> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (PD.SpecialSpellID == SpellDB.AttackID.None) {
			ring.SetActive(false);
		} else {
			ring.SetActive(true);
		}
	}
}
