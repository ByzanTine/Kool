using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class RebornTime : MonoBehaviour {

	// Text UI
	private Text textUI;
	
	// Use this for initialization
	void Start () {
		textUI = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		RebornTimeCount ();
	}

	void RebornTimeCount()
	{
		textUI.text = "";
		for(int i = 0; i < GameStatus.TotalPlayerNum; ++i)
		{
			if(GameStatus.UserDataCollection[i].rebornTime >= 0)
			{
				textUI.enabled = true;
				textUI.text += GameStatus.UserDataCollection[i].Username;
				textUI.text += ": " + GameStatus.UserDataCollection[i].rebornTime.ToString("D1");
				textUI.text += "\t\n";
			}
		}

	}
}
