using UnityEngine;
using System.Collections;

public class TrajectoryBlink : MonoBehaviour {

	// Use this for initialization
	void Start () {
//		StartCoroutine (extending ());
	}
	IEnumerator extending()
	{
		for(int i=0; i<10; ++i)
		{
			yield return new WaitForSeconds(0.05f);
			this.gameObject.transform.localScale += new Vector3(0.01f, 2f, 0.01f);
			this.gameObject.transform.localPosition += new Vector3(0f, 0f, 2f);
		}
	}
	// Update is called once per frame
	void Update () {
		this.gameObject.transform.localScale += new Vector3(0.0f, 0.5f, 0.0f);
		this.gameObject.transform.localPosition += new Vector3(0f, 0f, 0.5f);
	}
}
