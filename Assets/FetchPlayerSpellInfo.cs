using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FetchPlayerSpellInfo : MonoBehaviour {

	private Text text;
	private string label;
	private GameObject[] Player;
	void Start () {
		text = GetComponent<Text> ();
		Player = GameObject.FindGameObjectsWithTag (TagList.Player);
		label = text.text;
	}
	
	// Update is called once per frame
	void Update () {
		text.text = label;
		text.text += Player[0].GetComponent<PlayerControl> ().magicID;
	}
}
