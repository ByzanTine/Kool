using UnityEngine;
using System.Collections;

public class ItemGenerator : MonoBehaviour {
	private GameObject item;
	public float GenerateInterval = 5;
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

			GenerateItem();

		}
	}

	void GenerateItem() {
		GameObject randomItem = ItemDB.GetRandomItemPrefab ();
		item = Instantiate (randomItem, transform.position, Quaternion.identity) as GameObject;
		generationClock = Time.time;
	}


}