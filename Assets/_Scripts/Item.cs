using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

	public bool isBuff = true;
	public bool IncreaseNumber;
	public bool Bigger;
	private bool PickedUP = false;
	public int index_number;
	public SpellDB.AttackID AttID = SpellDB.AttackID.reflect;

	// Use this for initialization
	void OnTriggerEnter(Collider other){
		if (other.tag == TagList.Player && !PickedUP) {
			PickedUP = true;
			Debug.Log ("destroy item pick up by player");
			Item itemInfo = GetComponent<Item> ();
			other.GetComponent<PickUpItem>().PickUp(itemInfo.index_number, isBuff, AttID);
			Destroy(gameObject);
		}
	}


}
