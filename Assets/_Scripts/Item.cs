using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {


	public float attack_increase;
	public bool ice_fire;
	public int increase_fireball_number;
	private bool PickedUP = false;

	// Use this for initialization
	void OnTriggerEnter(Collider other){
		if (other.tag == TagList.Player && !PickedUP) {
			Debug.Log ("destroy item pick up by player");
			other.GetComponent<PickUpItem>().PickUp(gameObject);
			PickedUP = true;
			gameObject.SetActive(false);
		}
	}


}
