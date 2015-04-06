using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScreenFadeIn : MonoBehaviour {

	private Image image;
	// private float alpha = 1.0f;
	private float fadeRate = 0.1f;

	public Color curColor;

	public float lingerlength = 2.0f;
	// Use this for initialization
	public void Start () {
		image = GetComponent<Image> ();
		curColor = new Color(0, 0, 0, 255);
		
		StartCoroutine (ShowAndFadeFunc (lingerlength));
	}
	

	public IEnumerator ShowAndFadeFunc(float lingerlength) {

		// use exp for effect
		for (int k = 0; k < 1; k++) {

			for (float i = 1.0f; i > 0.1f; i*= 0.9f) {
				image.color = new Color(curColor.r,
				                        curColor.g,
				                        curColor.b,
				                        i);
				yield return new WaitForSeconds (Time.fixedDeltaTime);
			}

		}
	}
}
