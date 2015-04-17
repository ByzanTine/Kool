using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScenePrompt : MonoBehaviour {
	
	Text txt;
	// Use this for initialization
	void Start () {
		txt = GetComponent<Text> ();
	}
	
	
	// Update is called once per frame
	void Update () {
		txt.text = "";
		foreach(UserData user in UserInfoManager.UserDataCollection)
		{
			if(user.teamID == 0)
				txt.text += user.Username + " ";
		}

		txt.text += " VS ";

		foreach(UserData user in UserInfoManager.UserDataCollection)
		{
			if(user.teamID == 1)
				txt.text += user.Username + " ";
		}

		txt.text += "\t\nAny team who gets " + GameStatus.Instance.GameTargetRounds
			+ " scores will win.";
	}
}
