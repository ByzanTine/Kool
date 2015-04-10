using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChooseTeamStartCount : MonoBehaviour {

	private const float InitPrepareTime = 99f;

	private const float MaxPrepareTime = 3f;
	public static float PrepareTime;
	private static int enteredNum = 0;
	private Text txt;
	// Use this for initialization
	void Start () {
		txt = GetComponent<Text> ();
		txt.enabled = true;
		StartCoroutine( WaitToStart ());
	}

	public static void PlayerEntered()
	{
		PrepareTime = InitPrepareTime;
		enteredNum++;
	}
	
	public static void ConfirmedAndCount()
	{
		enteredNum--;
		if(enteredNum == 0)
		{
			PrepareTime = MaxPrepareTime;
		}
	}

	IEnumerator WaitToStart()
	{
		PrepareTime = InitPrepareTime;
		while(PrepareTime > 0)
		{
			yield return new WaitForSeconds(1.0f);
			PrepareTime--;
			txt.text = PrepareTime.ToString();
		}
		txt.text = "Start!";
		Application.LoadLevel ("GameMap");
	}
}
