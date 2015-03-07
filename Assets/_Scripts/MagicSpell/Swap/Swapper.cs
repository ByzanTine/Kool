using UnityEngine;
using System.Collections;

public class Swapper : MonoBehaviour {
	public float distance;
	public GameObject caster;
	public LayerMask collideLayer;
	// Use this for initialization
	void Start () {
		// dynamic allocate size

		ParticleSystem ps = GetComponentInChildren<ParticleSystem> ();
		if (ps) {
			ps.startSize = distance/4;
		}

		// generate line cast 
		Debug.DrawLine (caster.transform.position, 
		               caster.transform.position + transform.forward * distance, Color.green, 2.0f);
		RaycastHit hitInfo;

		if (Physics.Raycast(caster.transform.position, transform.forward, out hitInfo, distance, collideLayer)) {
			Debug.Log("[Spell] Raycast hit");
			GameObject gb = hitInfo.transform.gameObject;
			SwapHandler swapHandler = gb.GetComponent<SwapHandler>();
			if (swapHandler) {
				swapHandler.onSwapHandler(caster);
			}
		}
	}

}
