using UnityEngine;
using System.Collections;

public class DestroyByTime : MonoBehaviour {
	public float livingTime;
	// Use this for initialization
	void Start () {
		Destroy (gameObject, livingTime);
	}
	

}
