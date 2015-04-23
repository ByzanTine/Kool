using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RestartGameTimeCount : MonoBehaviour {

	Text txt;
	public int time = 3;
	// Use this for initialization
	void Start () {
		txt = GetComponent<Text> ();
		StartCoroutine (RestartCound ());
	}

	IEnumerator RestartCound()
	{
		txt.enabled = false;
		while(time > 0)
		{
			if(time < 10) txt.enabled = true;
			txt.text = "GAME WILL RESTART IN: " + time + " SECOND";
			time--;
			yield return new WaitForSeconds(1.0f);
		}
		Application.LoadLevel ("MainMap");
	}
	// Update is called once per frame
	void Update () {
	
	}
}
