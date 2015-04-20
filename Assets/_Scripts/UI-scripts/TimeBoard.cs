using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeBoard : MonoBehaviour {

	Text txt;
	int time;

	// Use this for initialization
	void Start () {
		txt = GetComponent<Text> ();
		time = GameStatus.GameMaxTime;
		StartCoroutine (TimeCountDown ());
	}

	IEnumerator TimeCountDown()
	{
		while(time > 0)
		{
			time--;
			txt.text = time.ToString("d3");
			yield return new WaitForSeconds(1.0f);
		}
	}
		
//	// Update is called once per frame
//	void Update () {
//
//		txt.text = GetScore(1) + " : " + GetScore(0);
//	}
//
//	private int GetScore(int teamNum){
//		return GameStatus.Instance.GameTargetRounds - GameStatus.Instance.GetTeamScore (teamNum);
//	}
}
