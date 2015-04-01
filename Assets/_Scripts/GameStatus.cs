﻿using UnityEngine;
using System.Collections;

public class GameStatus : MonoBehaviour {
	

	// LoseLife: You will die when you lose enough lives;
	// GainScore: You will win when you perform enough killing. Only 2v2 now;
	public enum GameMode{LoseLife, GainScore};
	public GameMode gameMode;

	// Target lives/scores in each mode for winning
	public int GameTargetRounds = 1;
	public GameObject playerPrefab;


	// Make this avaliable in inspector to intialize manually
	public string[] Usernames = new string[4];
	public Color[] UserColors = new Color[4]; 
	public Material[] UserMaterials = new Material[4];
//	private static Hashtable playerTable;

	// Game status control:
	private bool isGameOver = false;
	private int[] teamScores;

	// total player number in the game. READ ONLY
	private static int totalPlayerNum = 0;
	public static int TotalPlayerNum
	{
		get
		{
			return totalPlayerNum;
		}
	}

	private static UserData[] userDataCollection = new UserData[4];

	public static UserData[] UserDataCollection
	{
		get
		{
			return userDataCollection;
		}
	}

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
		// For later usage
		if(_instance == null)
		{
			//If I am the first instance, make me the Singleton
			_instance = this;
			DontDestroyOnLoad(this);
			BindAllUserData ();
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
//		playerTable = new Hashtable ();
		teamScores = new int[2] {0, 0};
		isGameOver = false;

		GameObject[] playerCollection = GameObject.FindGameObjectsWithTag (TagList.Player);
		if(playerCollection[0].GetComponent<PlayerControl>() != null)
		{
			BindAllWizardToUser();
		}

	}


	void OnLevelWasLoaded(int level) {

		teamScores = new int[2] {0, 0};
		isGameOver = false;
		
		GameObject[] playerCollection = GameObject.FindGameObjectsWithTag (TagList.Player);
		if(playerCollection[0].GetComponent<PlayerControl>() != null)
		{
			BindAllWizardToUser();
		}

	}

	void BindAllWizardToUser()
	{
		Debug.Log ("binding wizard to user");
		int id = 0;
		totalPlayerNum = 0;
		GameObject[] playerCollection = GameObject.FindGameObjectsWithTag (TagList.Player);
		foreach(GameObject player in playerCollection)
		{
			// bind input manager
			UserInputManager userCtrl = player.GetComponent<UserInputManager>();
			// assign player ID
			userCtrl.playerNum = id;
			player.name = userDataCollection[id].Username;
			userDataCollection[id].initPosition = player.transform.position;
			userDataCollection[id].wizardInstance = player;
			totalPlayerNum++;
			// seperate team for default/unassigned player:
			// default: 0 & 1 in team 0, 2 & 3 in team 1;
			// unassigned: destroy the corresponding player object;
			if(userDataCollection[id].teamID >= 2)
			{
				userDataCollection[id].teamID = id / 2;
			}
			else if(userDataCollection[id].teamID == -1)
			{
				totalPlayerNum--;
				Destroy(player);
			}
			userDataCollection[id].wizardMaterial = UserMaterials[userDataCollection[id].teamID + 1];
			// TODO this a HACK, I think there should be a player factory
			Renderer[] renders = player.GetComponentsInChildren<Renderer>();
			foreach (Renderer r in renders) {
				r.material = userDataCollection[id].wizardMaterial;
			}

			id++;
		}

	}

	// store into hashtable as well
	void BindAllUserData()
	{
		int playerID = 0;
		GameObject[] playerCollection = GameObject.FindGameObjectsWithTag (TagList.Player);
		foreach(GameObject player in playerCollection)
		{
			// bind input manager
			UserInputManager userCtrl = player.GetComponent<UserInputManager>();
			// assign player ID
			userCtrl.playerNum = playerID;
			userDataCollection[playerID] = new UserData();
			userDataCollection[playerID].userID = playerID;

			// bind the provided player info into User data.
			userDataCollection[playerID].Username = Usernames[playerID];
			userDataCollection[playerID].Usercolor = UserColors[playerID];
			userDataCollection[playerID].wizardMaterial = UserMaterials[playerID];
			playerID++;
		}
	}

	public int GetTeamScore(int teamId)
	{
		return teamScores[teamId];
	}
	
	// Update is called once per frame
	void Update () {

	}

	void WinEndGame()
	{

		isGameOver = true;

		StartCoroutine(WinEndGameEffect());
	}

	IEnumerator WinEndGameEffect()
	{
		yield return new WaitForSeconds (0.1f);

		GameObject[] playerCollection = GameObject.FindGameObjectsWithTag (TagList.Player);

		foreach(GameObject player in playerCollection)
		{
			GameObject winEffPrefab = Resources.Load ("ArenaEffects/WinParEff") as GameObject;
			GameObject winEff = GameObject.Instantiate (winEffPrefab, 
			                                            player.transform.position, Quaternion.identity)	as GameObject;
			UserInputManager userInput = player.GetComponent<UserInputManager>();
			userInput.LockLeftInput(3.0f);
		}

		yield return new WaitForSeconds (3.0f);
		Application.LoadLevel (Application.loadedLevel);
	}

	public void DecrementPlayerLife(int playerID)
	{
		userDataCollection [playerID].deathCount ++;
		DestroyPlayerWithID(playerID);

		UpdateScoreStatusWithDeath (playerID);

	}

	// update the game status when a player died
	void UpdateScoreStatusWithDeath(int playerID)
	{
		switch(gameMode)
		{
		case GameMode.LoseLife:
			if(userDataCollection [playerID].deathCount >= GameTargetRounds)
			{
				
				GameObject[] playerCollection = GameObject.FindGameObjectsWithTag (TagList.Player);
				
				if(playerCollection.Length <= 2 && !isGameOver)
				{
					WinEndGame();
				}
			}
			else
			{
				StartCoroutine(RebornPlayerWithID(playerID));
			}
			break;
			
		case GameMode.GainScore:

			int otherTeamID = (1 + userDataCollection[playerID].teamID) % 2;

			// add score to the other team:
			teamScores[otherTeamID]++;

			if(teamScores[otherTeamID] >= GameTargetRounds)
			{
				if(!isGameOver)
				{
					for(int i=0; i<totalPlayerNum; ++i)
					{
						if(userDataCollection[i].teamID != otherTeamID)
						{
							DestroyPlayerWithID(i);
						}
					}
					WinEndGame();
				}
			}
			else
			{
				StartCoroutine(RebornPlayerWithID(playerID));
			}
			break;
		default:
			break;
		}
	}

	
	IEnumerator RebornPlayerWithID(int id)
	{
		for(int i = 4; i >= 0 ; --i)
		{
			userDataCollection[id].rebornTime = i;
			yield return new WaitForSeconds (1.0f);
		}
		userDataCollection [id].rebornTime = -1;
		GameObject wizard = 
			Instantiate(playerPrefab,userDataCollection [id].initPosition,Quaternion.identity) 
				as GameObject;
		UserInputManager userCtrl = wizard.GetComponent<UserInputManager>();
		userCtrl.playerNum = id;
		// add to table
		userDataCollection[id].wizardInstance = wizard;
		wizard.name = userDataCollection [id].Username;

		// 
		Renderer[] renders = wizard.GetComponentsInChildren<Renderer>();
		foreach (Renderer r in renders) {
			r.material = userDataCollection[id].wizardMaterial;
		}
	}

	void DestroyPlayerWithID(int playerid)
	{

		if (userDataCollection[playerid].wizardInstance != null) {
			Destroy(userDataCollection[playerid].wizardInstance);
			userDataCollection[playerid].wizardInstance = null;
		}
		else {
			Debug.LogError("[Status] Try deleting non exist player");
		}

	}

	public static GameObject GetPlayerObjById(int playerId_in)
	{
		if (userDataCollection[playerId_in].wizardInstance != null)
			return userDataCollection[playerId_in].wizardInstance as GameObject;
		else 
			return null;
	}

}
