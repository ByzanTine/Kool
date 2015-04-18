using UnityEngine;
using System.Collections;

public class TeamScore : MonoBehaviour {
	public int teamNum = 0;
	private int PrevScore;
	public GameObject[] lives;
	// Use this for initialization
	void Start () {
		PrevScore = GameStatus.Instance.GameTargetRounds;
	}
	
	// Update is called once per frame
	void Update () {
		if (PrevScore > GetScore()){
			PrevScore -- ;
			lives[PrevScore].GetComponentInChildren<AddCross>().AddingCross();
		}
	}

	private int GetScore(){
		return GameStatus.Instance.GameTargetRounds - GameStatus.Instance.GetTeamScore (teamNum);
	}
}
