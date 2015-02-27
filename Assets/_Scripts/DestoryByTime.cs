using UnityEngine;
using System.Collections;

public class DestoryByTime : MonoBehaviour {
	public float livingTime;
	// Use this for initialization
	void Start () {
		Destroy (gameObject, livingTime);
	}
	

}
