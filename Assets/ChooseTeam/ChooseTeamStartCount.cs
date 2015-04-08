using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChooseTeamStartCount : MonoBehaviour {


	// the final game player number of each team
	// -1 means 1v1, 2v2 are both OK;
	public static int TeamSize = -1;

	private const float InitPrepareTime = 9999f;

	private const float MaxPrepareTime = 3f;
	private static float prepareTime;
	private static int enteredNum = 0;
	
	private static int[] teamNumCount = new int[2] {0, 0};
	private Text txt;
	// Use this for initialization
	void Start () {
		txt = GetComponent<Text> ();
		txt.enabled = true;
		StartCoroutine( WaitToStart ());
	}

	public static void PlayerEntered()
	{
		prepareTime = InitPrepareTime;
		enteredNum++;
	}

	//
	public static bool ConfirmedAndCount(int teamId)
	{
		// check if some team have too much people
		if(TeamSize != -1 && teamNumCount[teamId] > TeamSize)
		{
			return false;
		}

		teamNumCount [teamId]++;

		// the last player, one more check to make sure team are balanced
		if(enteredNum == 1)
		{
			if(teamNumCount[0] != teamNumCount[1])
			{
				teamNumCount [teamId]--;
				return false;
			}
		}

		enteredNum--;
		if(enteredNum == 0)
		{
			prepareTime = MaxPrepareTime;
		}
		return true;
	}

	IEnumerator WaitToStart()
	{
		prepareTime = InitPrepareTime;
		while(prepareTime > 0)
		{
			yield return new WaitForSeconds(1.0f);
			prepareTime--;
			txt.text = (prepareTime % 100).ToString();
		}
		txt.text = "Start!";
		Application.LoadLevel ("GameMap");
	}
}
