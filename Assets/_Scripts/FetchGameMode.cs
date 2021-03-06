﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FetchGameMode : MonoBehaviour {

	private Text txt;
	// Use this for initialization
	void Start () {
		txt = GetComponent<Text> ();

		txt.text = "Current Game Mode:" + "\t\n";

		if(TeamSelectionControl.Instance.TeamSize  == -1)
		{
			// free mode
			txt.text += "1v1 or 2v2";
		}
		else
		{
			txt.text += TeamSelectionControl.Instance.TeamSize + " vs "
			+ TeamSelectionControl.Instance.TeamSize;
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
