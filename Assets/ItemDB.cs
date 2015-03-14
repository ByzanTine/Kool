using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDB : MonoBehaviour {
	static public GameObject fireball_attack_increase;
	static public GameObject three_fireballs;
	static public List<GameObject> items;
	static public int Number_Of_Items;



	void Awake()
	{
//		fireball_attack_increase = Resources.Load ("Items/fireball_attack_increase") as GameObject;
//		three_fireballs = Resources.Load ("Items/three_fireballs") as GameObject;
		items = new List<GameObject> {
			Resources.Load ("Items/fireball_attack_increase") as GameObject,
			Resources.Load ("Items/three_fireballs") as GameObject
		};
//		items.Add (three_fireballs);
		Number_Of_Items = items.Count;
	}
}
