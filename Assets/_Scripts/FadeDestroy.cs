using UnityEngine;
using System.Collections;

public class FadeDestroy : MonoBehaviour {
	private float livingTime = 7f;
	private float hintTime = 1f;
	private Color startColor;
	private Color endColor;

	// Use this for initialization
	void Start () {
		if (GetComponent<Renderer>().material.HasProperty("_Color")) {
			startColor = GetComponent<Renderer> ().material.color;
		}
		
		endColor = startColor;
		endColor.a = 0.0f;
		StartCoroutine (DestroyAndFade ());

	}

	private IEnumerator DestroyAndFade() {


		yield return new WaitForSeconds(livingTime - hintTime);
		// fade all children

		Debug.Log("Start Fade");
		for ( int childIndex = 0; childIndex < transform.GetChildCount(); childIndex++)
		{
			Transform child = gameObject.transform.GetChild(childIndex);           
			
			child.gameObject.AddComponent<FadeDestroy>();
		}
		
		yield return StartCoroutine (LerpColor(startColor, endColor, hintTime));

		Destroy (gameObject);
	}

	public IEnumerator LerpColor(Color from, Color to, float time) {
		
		float i = 0.0f;
		float rate = 1.0f / time;

		while (i < 1.0f) {
			i += Time.fixedDeltaTime * rate;
			GetComponent<Renderer> ().material.color = Color.Lerp(from, to, i);
			// print(GetComponent<Renderer> ().material.color);
			// Debug.Log("Lerp once" + i);
			yield return null;
		}
	}
}
