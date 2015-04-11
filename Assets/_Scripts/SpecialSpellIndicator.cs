using UnityEngine;
using System.Collections;

public class SpecialSpellIndicator : MonoBehaviour {
	PlayerData playerdata;
	GameObject indicator;
	// Use this for initialization
	void Start () {
		playerdata = GetComponent<PlayerData> ();
		GameObject indicatorPrefab = Resources.Load ("Misc/LightEffect") as GameObject;
		indicator = Instantiate (indicatorPrefab, transform.position, transform.rotation) as GameObject;
		indicator.transform.SetParent (transform);

	}

	void Update() {
		if (playerdata) {
			switch (playerdata.SpecialSpellID)
			{
			case SpellDB.AttackID.iceBurst:
				indicator.SetActive(true);
				break;
			case SpellDB.AttackID.meteor:
				indicator.SetActive(true);
				break;
			default:
				// Debug.Log ("Not a special Spell");
				indicator.SetActive(false);
				break;
			}
		}
	}
	

}
