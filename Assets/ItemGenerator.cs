using UnityEngine;
using System.Collections;

public class ItemGenerator : MonoBehaviour {
	private GameObject item;
	public float TimeInterval = 5;
	private float destroyTime = 0;
	private bool ShouldExist = false;

	void Start(){
		destroyTime = Time.time;
		InvokeRepeating ("check_create", 0, 1);
	}
	// Update is called once per frame
	void check_create () {
		if (!item) 
		{
			if (ShouldExist){
				destroyTime = Time.time;
				ShouldExist = false;
			}

			if (Time.time - destroyTime > TimeInterval){
				int num = Random.Range (0, ItemDB.Number_Of_Items);
				item = Instantiate (ItemDB.items [num], transform.position, Quaternion.identity) as GameObject;
				ShouldExist = true;
			}
		}
	}
}