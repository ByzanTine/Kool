using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpdateText : MonoBehaviour {
	private Text text;
	private Slider slider;
	private string label;
	// Use this for initialization
	void Start () {
		text = GetComponent<Text> ();
		slider = GetComponentInParent<Slider> ();
		label = slider.GetComponent<SyncDebugVariables>().label;
		label = label.ToLower ();

	}
	
	// Update is called once per frame
	void Update () {
		text.text = label;
		text.text += " : ";
		text.text += slider.value;
	}
}
