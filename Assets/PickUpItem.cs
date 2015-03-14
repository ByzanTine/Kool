using UnityEngine;
using System.Collections;

public class PickUpItem : MonoBehaviour {

	// Update is called once per frame
	public void PickUp (GameObject item) {//TODO destroy the item after using the info inside



		Destroy(item);
	}
}
