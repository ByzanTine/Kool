using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDB : MonoBehaviour {
	static public List<GameObject> items;
	static public int Number_Of_Items;


	// TODO more accessor but not the public variable 


	static public GameObject GetRandomItemPrefab() {
		int num = Random.Range (0, Number_Of_Items);
		return items [num];
	}

	void Awake()
	{
		items = new List<GameObject> {
			Resources.Load ("Items/attack_increase") as GameObject,
			Resources.Load ("Items/six_spells") as GameObject,
			Resources.Load ("Items/reflect") as GameObject,
			Resources.Load ("Items/IceBurst") as GameObject


		};

//		for(int i = 0 ;i < items.Count; i++){
//			items[i].GetComponent<Item>().index_in_spellDB = i;
//		}
		Number_Of_Items = items.Count;
	}


}
