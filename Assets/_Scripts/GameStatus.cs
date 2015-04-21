using UnityEngine;
using System.Collections;

public class GameStatus : MonoBehaviour {
	

	// LoseLife: You will die when you lose enough lives;
	// GainScore: You will win when you perform enough killing. Only 2v2 now;
	public enum GameMode{LoseLife, GainScore};
	public GameMode gameMode;

	// Target lives/scores in each mode for winning
	public int GameTargetRounds = 1;
	public int GameMaxTime = 200;


	// Game status control:
	private bool isGameOver = false;
	private int[] teamScores;

	private static GameStatus _instance;
	
	//This is the public reference that other classes will use
	public static GameStatus Instance
	{
		get
		{
			//If _instance hasn't been set yet, we grab it from the scene!
			//This will only happen the first time this reference is used.
			if(_instance == null)
				_instance = GameObject.FindObjectOfType<GameStatus>();
			return _instance;
		}
	}

	void Awake() 
	{
		// Scene transition protection for singleton
		if(_instance == null)
		{
			//If I am the first instance, make me the Singleton
			_instance = this;
			DontDestroyOnLoad(this);
		}
		else
		{
			//If a Singleton already exists and you find
			//another reference in scene, destroy it!
			if(this != _instance)
				Destroy(this.gameObject);
		}

	}

	// Use this for initialization
	void Start () {
		teamScores = new int[2] {0, 0};
		isGameOver = false;
	}

	void OnLevelWasLoaded(int level) {

		// Avoid duplicated call by other instance
		if(this != _instance) return;

		teamScores = new int[2] {0, 0};
		isGameOver = false;
	}

	public int GetTeamScore(int teamId)
	{
		return teamScores[teamId];
	}

	void WinEndGame()
	{
		isGameOver = true;
		StartCoroutine(WinEndGameEffect());
	}

	IEnumerator WinEndGameEffect()
	{
		yield return new WaitForSeconds (0.1f);
		GameObject winSEPrefab = Resources.Load (Constants.AudioFileDir + "WinningSE") as GameObject;
		GameObject winSE = GameObject.Instantiate (winSEPrefab)	as GameObject;

		
		GameObject[] playerCollection = GameObject.FindGameObjectsWithTag (TagList.Player);

		foreach(GameObject player in playerCollection)
		{
			GameObject winEffPrefab = Resources.Load ("ArenaEffects/WinParEff") as GameObject;
			GameObject winEff = GameObject.Instantiate (winEffPrefab, 
			                                            player.transform.position, Quaternion.identity)	as GameObject;
			UserInputManager userInput = player.GetComponent<UserInputManager>();
			userInput.LockControl(UserInputManager.InputSource.AllControl, 9.0f);
		}

		yield return new WaitForSeconds (8.0f);
		Application.LoadLevel (Application.loadedLevel);
	}

	// update the game status when a player died
	void UpdateScoreStatusWithDeath(int playerID)
	{
		switch(gameMode)
		{
		case GameMode.LoseLife:
			if(UserInfoManager.UserDataCollection [playerID].deathCount >= GameTargetRounds)
			{
				
				GameObject[] playerCollection = GameObject.FindGameObjectsWithTag (TagList.Player);
				
				if(playerCollection.Length <= 2 && !isGameOver)
				{
					WinEndGame();
				}
			}
			else
			{
				StartCoroutine(UserInfoManager.Instance.RebornPlayerWithId(playerID));
			}
			break;
			
		case GameMode.GainScore:

			int otherTeamID = (1 + UserInfoManager.UserDataCollection[playerID].teamID) % 2;

			// add score to the other team:
			teamScores[otherTeamID]++;

			if(teamScores[otherTeamID] >= GameTargetRounds)
			{
				if(!isGameOver)
				{
					for(int i = 0; i < UserInfoManager.TotalPlayerNum; ++i)
					{
						if(UserInfoManager.UserDataCollection[i].teamID != otherTeamID)
						{
							UserInfoManager.Instance.DestroyPlayerWithId(i);
						}
					}
					WinEndGame();
				}
			}
			else
			{
				StartCoroutine(UserInfoManager.Instance.RebornPlayerWithId(playerID));
			}
			break;
		default:
			break;
		}
	}

	// public method used when player dead
	public void DecrementPlayerLife(int playerID)
	{
		UserInfoManager.UserDataCollection [playerID].deathCount ++;
		UserInfoManager.Instance.DestroyPlayerWithId(playerID);
		
		UpdateScoreStatusWithDeath (playerID);
	}

	/// <summary>
	/// Ends the game by time limit.
	/// </summary>
	public int EndGameByTimeLimit()
	{
		isGameOver = true;

		int winTeam = -1;

		if(teamScores[0] == teamScores[1])
		{
			winTeam = 2;
		}
		else
		{
			if(teamScores[0] > teamScores[1])
			{
				winTeam = 0;
			}
			else
			{
				winTeam = 1;
			}

			for(int i = 0; i < UserInfoManager.TotalPlayerNum; ++i)
			{
				if(UserInfoManager.UserDataCollection[i].teamID != winTeam)
				{
					UserInfoManager.Instance.DestroyPlayerWithId(i);
				}
			}

		}



		WinEndGame();

		return winTeam;
	}

}
