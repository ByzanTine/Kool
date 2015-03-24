using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScenePrompt : MonoBehaviour {
	
	Text txt;
	// Use this for initialization
	void Start () {
		txt = GetComponent<Text> ();
	}
	
	
	// Update is called once per frame
	void Update () {
		
		txt.text = "Any team who gets " + GameStatus.Instance.GameTargetRounds
			+ " scores will win.";
	}
}
