using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

	public bool isBuff = true;
	public int index_in_spellDB;
	public float increase_size;
	public int increase_fireball_number;
	private bool PickedUP = false;

	// Use this for initialization
	void OnTriggerEnter(Collider other){
		if (other.tag == TagList.Player && !PickedUP) {
			PickedUP = true;
			Debug.Log ("destroy item pick up by player");
			Item itemInfo = GetComponent<Item> ();
			other.GetComponent<PickUpItem>().PickUp(itemInfo.index_in_spellDB, isBuff);
			Destroy(gameObject);
		}
	}


}
