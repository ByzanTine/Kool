using UnityEngine;
using System.Collections;

public class ItemGenerator : MonoBehaviour {
	private GameObject item;
	public float TimeInterval = 5;
	// private float destroyTime = 0;

	// private bool ShouldExist = false;

	private float generationClock;

	void Start(){
		// destroyTime = Time.time;
		generationClock = Time.time;
		// InvokeRepeating ("CheckCreate", 0, 1);
	}
	// Update is called once per frame
	// TODO the ShouldExist lock may not be neccessary 
	void Update () {
		// if item is not there
		// And it has been TimeInterval time
		// Generate a item then
		if (!item && Time.time - generationClock > TimeInterval) 
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