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

		txt.text = GetScore(1) + " : " + GetScore(0);
	}

	private int GetScore(int teamNum){
		return GameStatus.Instance.GameTargetRounds - GameStatus.Instance.GetTeamScore (teamNum);
	}
}
