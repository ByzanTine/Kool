using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ShowAndFade : MonoBehaviour {
	private Image image;
	// private float alpha = 1.0f;
	private float fadeRate = 0.1f;
	public float startDelay = 0.0f;
	public Color curColor;
	public int blinkNum = 1;
	public float lingerlength = 2.0f;
	// Use this for initialization
	public void Start () {
		image = GetComponent<Image> ();
		curColor = image.color;

		StartCoroutine (ShowAndFadeFunc (lingerlength));
	}

	public void ResetColor () {
		image.color = new Color(0, 0, 0, 0);
	}
	public IEnumerator ShowAndFadeFunc(float lingerlength) {

		ResetColor ();
		yield return new WaitForSeconds(startDelay);
		// use exp for effect
		for (int k = 0; k < blinkNum; k++) {
			for (float i = 0.01f; i < 1.0f; i*= 1.1f) {
				image.color = new Color(curColor.r,
				                       curColor.g,
				                       curColor.b,
				                       i);
				yield return new WaitForSeconds(Time.fixedDeltaTime);
			}

			yield return new WaitForSeconds (lingerlength);

			for (float i = 1.0f; i > 0.1f; i*= 0.9f) {
				image.color = new Color(curColor.r,
				                       curColor.g,
				                       curColor.b,
				                       i);
				yield return new WaitForSeconds (Time.fixedDeltaTime);
			}
			image.color = new Color(0, 0, 0, 0);
		}
	}
	

}
