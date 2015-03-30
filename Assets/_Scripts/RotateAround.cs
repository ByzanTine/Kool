using UnityEngine;
using System.Collections;

public class RotateAround : MonoBehaviour {
	private Vector3 point;
	public Vector3 axis;
	public Material fire;
	public Material ice;
	private PlayerData PD;
	public bool moreBall;
	private PlayerBuffStatus PBS;
	private MeshRenderer m;
	private Vector3 originalScale;


	void Start (){
		PD = transform.parent.transform.parent.gameObject.GetComponent<PlayerData> ();
		PBS = transform.parent.transform.parent.gameObject.GetComponent<PlayerBuffStatus> ();
		m = GetComponent<MeshRenderer>();
		originalScale = transform.localScale;
	}


	void Update() {
		if (moreBall) {
			m.enabled = PBS.IncreaseNumber;
		}

		if (PBS.Bigger) {
			transform.localScale = originalScale * 1.5f;
		} else {
			transform.localScale = originalScale;
		}
		if (PD.ice_fire == Constants.SpellMode.Fire)
			GetComponent<Renderer>().material = fire;
		else
			GetComponent<Renderer>().material = ice;
		point = transform.parent.transform.position;
		transform.RotateAround(point, axis, 200 * Time.deltaTime);
	}

}
