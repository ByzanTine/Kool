using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChooseTeamStartCount : MonoBehaviour {

	private const float MaxPrepareTime = 3f;
	public static float PrepareTime;
	private Text txt;
	// Use this for initialization
	void Start () {
		txt = GetComponent<Text> ();
		txt.enabled = true;
		StartCoroutine( WaitToStart ());
	}
	
	public static void ConfirmedAndCount()
	{
		PrepareTime = MaxPrepareTime;
	}

	IEnumerator WaitToStart()
	{
		PrepareTime = 99f;
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
