using UnityEngine;
using System.Collections;

public class LoadLevel : MonoBehaviour {
		
		
	public void LoadLevelWithName(string levelName)
	{
		Application.LoadLevel (levelName);
	}
}
