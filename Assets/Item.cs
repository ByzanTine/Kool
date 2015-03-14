using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

	public float attack_increase;
	public bool ice_fire;
	public int increase_fireball_number;
	// Use this for initialization
	void OnTriggerEnter(Collider other){
		if (other.tag == TagList.Player) {
			Debug.Log ("destroy item pick up by player");
			Destroy(gameObject);
			other.GetComponent<PickUpItem>().buff(attack_increase,ice_fire,increase_fireball_number);
		}
	}
}
