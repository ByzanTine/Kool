using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {
	public SpellDB.AttackID AttID = SpellDB.AttackID.reflect;
	private bool PickedUP = false;
	public bool buff;
	public bool TriggerNow;
	// Use this for initialization
	void OnTriggerEnter(Collider other){
		if (other.tag == TagList.Player && !PickedUP) {
			PickedUP = true;
			Debug.Log ("destroy item pick up by player");

			other.GetComponent<PlayerPickUpItem>().PickUp(buff, TriggerNow, AttID);
			Destroy(gameObject);
		}
	}


}
