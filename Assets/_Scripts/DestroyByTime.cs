using UnityEngine;
using System.Collections;

public class DestroyByTime : MonoBehaviour {
	public float livingTime;
	// Use this for initialization
	private ExplodeLink explodeLink;
	void Start () {
		explodeLink = GetComponent<ExplodeLink> ();
		if (explodeLink) {
			StartCoroutine(DelayedDestoryDelegate());
		}
		else {
			Destroy (gameObject, livingTime);
		}
	}

	IEnumerator DelayedDestoryDelegate() {
		yield return new WaitForSeconds(livingTime);
		explodeLink.CasterDelegateDestroy(transform.position);
	}

}
