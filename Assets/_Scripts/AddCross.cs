using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AddCross : MonoBehaviour {
	public float scale = 2.5f;
	public float shrinkSpeed = 1.2f;
	private Vector3 initScale; 
	private bool adding;
	// Use this for initialization
	void Start () {
		initScale = transform.localScale;
		transform.localScale *= scale;
		gameObject.GetComponent<Image> ().enabled = false;
	}

	void Update (){
		if (initScale.x < transform.localScale.x && adding)
			transform.localScale -= Vector3.one*Time.deltaTime*shrinkSpeed;
	}
	

	public void AddingCross(){
		adding = true;
		gameObject.GetComponent<Image> ().enabled = true;
	}
}
