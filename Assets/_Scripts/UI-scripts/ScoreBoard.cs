using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreBoard : MonoBehaviour {

	Text txt;
	// Use this for initialization
	void Start () {
		txt = GetComponent<Text> ();
	}

		
	// Update is called once per frame
	void Update () {

		txt.text = " " + GameStatus.Instance.GetTeamScore(0) + " vs " + GameStatus.Instance.GetTeamScore(1) + " ";
	}
}
