using UnityEngine;
using System.Collections;

public class ItemGenerator : MonoBehaviour {
	public float timeInterval = 10;
	private GameObject item;
	private float CreatingTime = 0;
	public float CoolDown = 5;
	private float destroyTime = 0;
	private bool ShouldExist = false;

	void Start(){
		CreatingTime = Time.time;
		destroyTime = Time.time;
	}
	// Update is called once per frame
	void Update () {
		if (item) {
			if (Time.time - CreatingTime > timeInterval) {
				Destroy (item);
				item = null;
				destroyTime = Time.time;
				ShouldExist = false;
			}
		} else {
			if (ShouldExist){
				destroyTime = Time.time;
				ShouldExist = false;
			}

			if (Time.time - destroyTime > CoolDown){
				int num = Random.Range (0, ItemDB.Number_Of_Items);
//				Debug.Log("item num: " + num + "  " +ItemDB.Number_Of_Items);
				item = Instantiate (ItemDB.items [num], transform.position, Quaternion.identity) as GameObject;
				CreatingTime = Time.time;
				ShouldExist = true;
			}
		}
	}
}