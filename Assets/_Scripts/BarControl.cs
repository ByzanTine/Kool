using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BarControl : MonoBehaviour {
	
	public Vector3 startPos;
	public Vector3 endPos;
	public float progress = 0;
	private RectTransform castTransform;
		
	// Use this for initialization
	void Start () {
		castTransform = GetComponent<RectTransform> ();
		endPos = castTransform.position;
//		print ("x"+castTransform.position.x);
//		print ("w"+castTransform.rect.width);
		startPos = new Vector3 (castTransform.position.x - castTransform.rect.width,castTransform.position.y, castTransform.position.z);
		castTransform.position = startPos;
	}
	
	// Update is called once per frame
	void Update () {
		castTransform.position = Vector3.Lerp(startPos, endPos, progress);
	}

	public void SetBar (float newProgress){
		progress = newProgress;
	}

	public float GetBar(){
		return progress;
	}


}