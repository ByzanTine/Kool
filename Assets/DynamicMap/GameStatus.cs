using UnityEngine;
using System.Collections;

public class GameStatus : MonoBehaviour {

	private int playerNum;
	private bool isGameOver = false;
	// Use this for initialization
	void Start () {
		playerNum = GameObject.FindGameObjectsWithTag (TagList.Player).Length;
	}
	
	// Update is called once per frame
	void Update () {
		GameObject[] playerCollection = GameObject.FindGameObjectsWithTag (TagList.Player);
		if(playerCollection.Length <= 1)
		{
			if(playerCollection.Length == 1 && !isGameOver)
			{
				isGameOver = true;
				GameObject winEffPrefab = Resources.Load ("WinParEff") as GameObject;
				GameObject winEff = GameObject.Instantiate (winEffPrefab, 
						 	playerCollection[0].transform.position, Quaternion.identity)
							as GameObject;
				UserInputManager userInput = playerCollection[0].GetComponent<UserInputManager>();
				userInput.LockLeftInput(3.0f);
				StartCoroutine(ReadyToRestart());
			}
		}

	}

	IEnumerator ReadyToRestart()
	{
		yield return new WaitForSeconds (3.0f);
		Application.LoadLevel (Application.loadedLevel);
	}
}
