using UnityEngine;
using System.Collections;

public class LoadLevelAfterTime : MonoBehaviour {
	public string levelName;
	public float waitTime;
	// Use this for initialization
	void Start () {
		StartCoroutine (DelayedLoadLevel (waitTime));
	}
	IEnumerator DelayedLoadLevel(float waitTime_) {
		yield return new WaitForSeconds(waitTime_);
		Application.LoadLevel (levelName);
	}

}
