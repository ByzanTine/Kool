using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ShowAndFade : MonoBehaviour {
	public Image image;
	// private float alpha = 1.0f;
	private float fadeRate = 0.1f;
	public Color curColor;
	// Use this for initialization
	public void Start () {
		image = GetComponent<Image> ();
		curColor = image.color;
		StartCoroutine (showAndFade (1.0f));

	}
	public void resetColor () {
		image.color = new Color(0, 0, 0, 0);
	}
	public IEnumerator showAndFade(float lingerlength) {

		image.color = new Color(0, 0, 0, 0);
		for (float i = 0.0f; i < 1.0f; i+= fadeRate) {
			image.color = new Color(curColor.r,
			                       curColor.g,
			                       curColor.b,
			                       i);
			yield return new WaitForSeconds (0.1f);;
		}
		yield return new WaitForSeconds (lingerlength);
		for (float i = 0.0f; i < 1.0f; i+= fadeRate) {
			image.color = new Color(curColor.r,
			                       curColor.g,
			                       curColor.b,
			                       1.0f - i);
			yield return new WaitForSeconds (0.1f);;
		}
		image.color = new Color(0, 0, 0, 0);
	}
	

}
