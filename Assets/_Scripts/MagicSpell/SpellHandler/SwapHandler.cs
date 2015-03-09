using UnityEngine;
using System.Collections;

public class SwapHandler : MonoBehaviour {
	private float swapTriggerCD = 0.5f;
	public delegate void onSpell();
	public onSpell onSpellSwap;
	float timer;
	void Start () {
		timer = Time.time;
	}
	public void onSwapHandler(GameObject caster) {
		if (Time.time - timer > swapTriggerCD) {
			timer = Time.time;
			Debug.Log("[SPELL]: object swapped");
			// create some effect
			Vector3 tmpPos = caster.transform.position;
			caster.transform.position = transform.position;
			transform.position = tmpPos;
			// swap is occured
			onSpellSwap();
		}
	}
}
