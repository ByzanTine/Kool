using UnityEngine;
using System.Collections;

public class ItemGenerator : MonoBehaviour {
	private GameObject item;
	public float GenerateInterval = 10f;
	// private float destroyTime = 0;

	private float generationClock;

	void Start(){
		generationClock = Time.time;

	}
	
	void Update () {
		// if item is not there
		// And it has been GenerateInterval time
		// Generate a item then
		if (!item && Time.time - generationClock > GenerateInterval) 
		{
			generationClock = Time.time;
			GenerateItem();
		}
	}
	public void ItemDestroyHandler() {
		generationClock = Time.time;
	}

	void GenerateItem() {
		GameObject randomItem = ItemDB.GetRandomItemPrefab ();
		item = Instantiate (randomItem, transform.position + 5 * Vector3.up, Quaternion.identity) as GameObject;
		item.GetComponent<Item> ().itemGen = this;
	}


}