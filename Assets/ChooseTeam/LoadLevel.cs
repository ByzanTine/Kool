using UnityEngine;
using System.Collections;

public class LoadLevel : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LoadLevelWithName(string levelName = "ChooseTeam")
	{
		Application.LoadLevel (levelName);
	}
}
