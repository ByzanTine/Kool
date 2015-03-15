using UnityEngine;
using System.Collections;

public class GameStatus : MonoBehaviour {

	static public GameStatus Instance;


	// LoseLife: You will die when you lose enough lives;
	// GainScore: You will win when you perform enough killing. Only 2v2 now;
	public enum GameMode{LoseLife, GainScore};
	public GameMode gameMode;
	// Target lives/scores in each mode for winning
	public int GameTargetRounds = 1;
	public GameObject playerPrefab;

	// private int playerNum;
	private bool isGameOver = false;
	private UserData[] userDataCollection = new UserData[4];

	void Awake()
	{
		Instance = this;
	}
	// Use this for initialization
	void Start () {
		// playerNum = GameObject.FindGameObjectsWithTag (TagList.Player).Length;
		BindAllUserData ();
	}

	void BindAllUserData()
	{
		GameObject[] playerCollection = GameObject.FindGameObjectsWithTag (TagList.Player);
		foreach(GameObject player in playerCollection)
		{
			UserInputManager userCtrl = player.GetComponent<UserInputManager>();
			int playerID = userCtrl.playerNum;
			userDataCollection[playerID] = new UserData();
			userDataCollection[playerID].userID = playerID;
			userDataCollection[playerID].initPosition = player.transform.position;
		}
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
			int team = playerID >= 2 ? 2 : 0;
			if(userDataCollection [playerID].deathCount 
			   + userDataCollection [playerID].deathCount >= GameTargetRounds)
			{
				if(!isGameOver)
				{
					DestroyPlayerWithID(team);
					DestroyPlayerWithID(team+1);
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
		yield return new WaitForSeconds (3.0f);
		GameObject wizard = 
			Instantiate(playerPrefab,userDataCollection [id].initPosition,Quaternion.identity) 
				as GameObject;
		UserInputManager userCtrl = wizard.GetComponent<UserInputManager>();
		userCtrl.playerNum = id;
	}

	void DestroyPlayerWithID(int id)
	{
		GameObject[] playerCollection = GameObject.FindGameObjectsWithTag (TagList.Player);
		foreach(GameObject player in playerCollection)
		{
			UserInputManager userCtrl = player.GetComponent<UserInputManager>();
			if(userCtrl.playerNum == id)
			{
				Destroy(player);
				return;
			}
		}
	}

}
