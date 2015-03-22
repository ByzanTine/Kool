using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

	public bool isBuff = true;
	public bool IncreaseNumber;
	public bool Bigger;

	public int index_number;
	public SpellDB.AttackID AttID = SpellDB.AttackID.reflect;

	private bool PickedUP = false;
	// Use this for initialization
	void OnTriggerEnter(Collider other){
		if (other.tag == TagList.Player && !PickedUP) {
			PickedUP = true;
			Debug.Log ("destroy item pick up by player");

			other.GetComponent<PlayerPickUpItem>().PickUp(index_number, isBuff, AttID);
			Destroy(gameObject);
		}
	}


}
