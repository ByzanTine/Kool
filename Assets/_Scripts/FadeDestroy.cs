using UnityEngine;
using System.Collections;

public class FadeDestroy : MonoBehaviour {
	public float livingTime;
	// Use this for initialization
	void Start () {


		StartCoroutine (DestroyAndFade ());

	}

	private IEnumerator DestroyAndFade() {


		yield return new WaitForSeconds(livingTime);
		// fade all children

		Destroy (gameObject);
	}
	

}
