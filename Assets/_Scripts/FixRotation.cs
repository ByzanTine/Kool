using UnityEngine;
using System.Collections;

public class FixRotation : MonoBehaviour {

	Quaternion rotation;
	
	void Awake()
	{
		rotation = transform.rotation;
//		rotation.y = 0;
	}
	
	void LateUpdate()
	{
		transform.rotation = rotation;
	}
}
