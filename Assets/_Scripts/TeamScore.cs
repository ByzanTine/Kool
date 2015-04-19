using UnityEngine;
using System.Collections;

public class TeamScore : MonoBehaviour {
	public int teamNum = 0;
	private int PrevScore;
	private GameObject[] lives;

	private int UIPositionOffset = 50;
	public GameObject LiveUIPrefab;

	// Use this for initialization
	void Start () {
		// Left or right:
		UIPositionOffset = teamNum == 1 ? UIPositionOffset : -UIPositionOffset;

		PrevScore = GameStatus.Instance.GameTargetRounds;

		lives = new GameObject[GameStatus.Instance.GameTargetRounds];
		for(int i = 0; i < GameStatus.Instance.GameTargetRounds; ++i)
		{
			GameObject LiveUIObj = Instantiate (LiveUIPrefab) as GameObject;
			LiveUIObj.transform.parent = this.gameObject.transform;
			LiveUIObj.transform.localPosition 
				= new Vector3(UIPositionOffset * i, 0, 0); 
			lives[i] = LiveUIObj;
		}
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
