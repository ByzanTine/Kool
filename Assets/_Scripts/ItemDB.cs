using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDB : MonoBehaviour {
	static public List<GameObject> items;
	static public int Number_Of_Items;



	void Awake()
	{
		items = new List<GameObject> {
			Resources.Load ("Items/fireball_attack_increase") as GameObject,
			Resources.Load ("Items/six_fireballs") as GameObject
		};

//		for(int i = 0 ;i < items.Count; i++){
//			items[i].GetComponent<Item>().index_in_spellDB = i;
//		}
		Number_Of_Items = items.Count;
	}
}
