using UnityEngine;
using System.Collections;

public class SpecialSpellIndicator : MonoBehaviour {
	private PlayerData PD;
	private GameObject ring;
	// Use this for initialization
	void Start () {
		PD = GetComponent<PlayerData> ();
		GameObject indicatorPrefab = Resources.Load ("MagicSpells/Indicator/ring") as GameObject;
		ring = Instantiate (indicatorPrefab, transform.position, Quaternion.identity) as GameObject;
		ring.transform.parent = transform;

	}
	
	// sync with SpecialSpellID
	void Update () {
		// if have special skill
		if (PD.SpecialSpellID == SpellDB.AttackID.None) {
			ring.SetActive(false);
		} else {
			ring.SetActive(true);
		}
	}
}
