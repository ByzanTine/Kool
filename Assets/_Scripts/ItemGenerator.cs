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
	// Update is called once per frame
	// TODO the ShouldExist lock may not be neccessary 
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
		int num = Random.Range (0, ItemDB.Number_Of_Items);
		item = Instantiate (ItemDB.items [num], transform.position, Quaternion.identity) as GameObject;
		generationClock = Time.time;
	}


}