using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class barControl : MonoBehaviour {
	
	private Vector3 startPos;
	private Vector3 endPos;
	private float progress = 0;
	private RectTransform castTransform;
	public float increment = 0.05f;
	
	// Use this for initialization
	void Start () {
		castTransform = GetComponent<RectTransform> ();
		endPos = castTransform.position;
		startPos = new Vector3 (castTransform.position.x - castTransform.rect.width,castTransform.position.y, castTransform.position.z);
		castTransform.position = startPos;
	}
	
	// Update is called once per frame
	void Update () {
		if (progress <= 1.0){
			castTransform.position = Vector3.Lerp(startPos, endPos, progress);
			autoIncrement();
		}
	}

	public bool decreaseMana(float mana){
		if (mana < progress) {
			progress -= mana;
			return true;
		} 
		return false;
	}

	public void setBar (float newProgress){
		progress = newProgress;
	}

	public float getBar(){
		return progress;
	}

	private void autoIncrement(){
		progress += increment * Time.deltaTime;
	}
}