using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BarControl : MonoBehaviour {



	public float progress = 0;
	private RectTransform castTransform;

	private Vector2 rectOrigin;
	// Use this for initialization
	void Start () {
		castTransform = GetComponent<RectTransform> ();

		rectOrigin = GetComponent<RectTransform> ().sizeDelta;

	}

	// Update is called once per frame
	void Update () {
		// castTransform.position = Vector3.Lerp(startPos, endPos, progress);
		float cameraPara = 10.0f / Camera.main.fieldOfView;
		float curWidth = Mathf.Lerp (0, rectOrigin.x * cameraPara, progress);
		castTransform.sizeDelta =  rectOrigin * cameraPara;

		castTransform.sizeDelta = new Vector2 (curWidth, 
		                                       castTransform.rect.height
		                                       );


	}

	public void SetBar (float newProgress){
		progress = newProgress;
	}

	public float GetBar(){
		return progress;
	}


}