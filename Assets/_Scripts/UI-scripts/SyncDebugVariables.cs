using UnityEngine;
using System.Collections;
using UnityEngine.UI;
// HOW TO USE:
// Add a const string for a indicator in Globals.
// Then enter it into the label placeholder
// Well, don't make it too long...
public class SyncDebugVariables : MonoBehaviour {
	private Slider slider;
	public string label; 
	void Start() {
		slider = GetComponent<Slider> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Globals.setValue (label, slider.value);
	}
}
