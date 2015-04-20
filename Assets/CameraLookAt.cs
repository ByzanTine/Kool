using UnityEngine;
using System.Collections;

public class CameraLookAt : MonoBehaviour {


	
	// Update is called once per frame
	void Update () {
		transform.RotateAround (new Vector3(0,0,0), Vector3.up, 0.5f);
	}
}
