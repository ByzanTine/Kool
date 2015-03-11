using UnityEngine;
using System.Collections;

public class WizardInsideArea : MonoBehaviour {

	// Use this for initialization
	public bool onFloor;
	Wizard wizard;
	// add some listeners 
	// Say onBlink listener
	// Say onSwap listener
	void Awake () {
		wizard = GetComponent<Wizard> ();
		onFloor = true;

	}
	


	void OnTriggerStay(Collider coll) {
		Debug.Log("On Floor");
		wizard.standState = Wizard.WizardStandState.OnSafe;
	}

	void OnTriggerExit(Collider coll) {
		onFloor = false;
		wizard.standState = Wizard.WizardStandState.OnDanger;
	}
}
