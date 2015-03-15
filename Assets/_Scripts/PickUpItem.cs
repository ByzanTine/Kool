using UnityEngine;
using System.Collections;

public class PickUpItem : MonoBehaviour {
	private BuffHandler BH;

	void Start (){
		BH = GetComponent<BuffHandler> ();
	}
	// Update is called once per frame
	public void PickUp (int mode, bool isBuff) {
		if (isBuff)
			BH.UpdateBuff (mode);
		else 
			Debug.Log ("da zhao");// TODO da zhao
	}
}
