using UnityEngine;
using System.Collections;

public class RotateAround : MonoBehaviour {
	private Vector3 point;
	public Vector3 axis;
	public Material fire;
	public Material ice;
	private PlayerData PD;

	void Start (){
		PD = transform.parent.transform.parent.gameObject.GetComponent<PlayerData> ();
	}


	void Update() {
		if (PD.ice_fire == Constants.SpellMode.Fire)
			GetComponent<Renderer>().material = fire;
		else
			GetComponent<Renderer>().material = ice;
		point = transform.parent.transform.position;
		transform.RotateAround(point, axis, 200 * Time.deltaTime);
	}

}
