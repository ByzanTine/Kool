using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {
	// must be defined in inspector
	public ItemDB.ItemType itemType;
	// TODO more variables for specific item description
	private bool PickedUP = false;

	// Use this for initialization
	void OnTriggerEnter(Collider other){
		if (other.tag == TagList.Player && !PickedUP) {
			PickedUP = true;
			Debug.Log ("destroy item pick up by player");

			other.GetComponent<PlayerBuffStatus>().UpdateBuff(itemType);
			Destroy(gameObject);
		}
	}


}
