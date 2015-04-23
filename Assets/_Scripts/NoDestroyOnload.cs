using UnityEngine;
using System.Collections;

public class NoDestroyOnload : MonoBehaviour {

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(this);
	}
}
