using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuffHandler : MonoBehaviour {
	public float coolDown = 3;
	private List<float> Buff_time_List;	
	private List<bool> Buff_valid_List;	
	private PlayerData pd;

	// Use this for initialization
	void Start () {
		Buff_time_List = new List<float>();
		Buff_valid_List = new List<bool> ();
		pd = GetComponent<PlayerData> ();
		for (int i = 0 ; i < ItemDB.Number_Of_Items;i ++){
			Buff_time_List.Add(0);
			Buff_valid_List.Add(false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < ItemDB.Number_Of_Items; i ++) {
			if ((Time.time - Buff_time_List [i] > coolDown )&& Buff_valid_List[i]) {
				Buff_valid_List[i] = false;
				DebuffPlayerData(i);
			}
		}
	}

	public void updateBuff(int mode){
		Buff_time_List [mode] = Time.time;
		if (!Buff_valid_List [mode]){
			BuffPlayerData (mode);
			Buff_valid_List [mode] = true;
		}
	}

	private void BuffPlayerData(int i){
		Item itemInfo = ItemDB.items [i].GetComponent<Item> ();
		pd.number_of_balls += itemInfo.increase_fireball_number;
		pd.Spell_size += itemInfo.increase_size;
	}

	private void DebuffPlayerData(int i){
		Item itemInfo = ItemDB.items [i].GetComponent<Item> ();
		pd.number_of_balls -= itemInfo.increase_fireball_number;
		pd.Spell_size -= itemInfo.increase_size;
	}
}
