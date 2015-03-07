using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FetchPlayerSpellInfo : MonoBehaviour {

	public int PlayerID;
	private Text text;
	// private string label;
	private GameObject[] Player;

	void Start () {
		text = GetComponent<Text> ();
		Player = GameObject.FindGameObjectsWithTag (TagList.Player);
		// label = text.text;
	}
	
	// Update is called once per frame
	void Update () {
		text.text = "PlayerID: ";
		text.text += PlayerID;
		text.text += "\nCurrent Spell: ";
		if (Player[PlayerID]) {
			text.text += SpellDB.attackIDnames[Player[PlayerID].GetComponent<PlayerControl> ().magicID];
		}
	}
}
