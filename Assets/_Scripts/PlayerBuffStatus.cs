using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerBuffStatus : MonoBehaviour {
	public float effectiveTime = 3;
	// this should not index with 0 1 2 3.
	// Should be some Enum
	private List<float> Buff_time_List;	// keep track of the cool down time for all the buffs
	private List<bool> Buff_valid_List;	// keep track of the all the buff which is valid 
	private PlayerData pd;

	public bool IncreaseNumber = false;
	public bool Bigger = false;
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
			if ((Time.time - Buff_time_List [i] > effectiveTime )&& Buff_valid_List[i]) {// if it is not effective
				Buff_valid_List[i] = false;
				DebuffPlayerData(i);
			}
		}
		UpdateFireballID ();
	}

	public void UpdateBuff(int mode){
		Buff_time_List [mode] = Time.time;
		//ChangeIceFire ();
		if (!Buff_valid_List [mode]){
			BuffPlayerData (mode);
			Buff_valid_List [mode] = true;
		}
	}

	private void BuffPlayerData(int i){
		Item itemInfo = ItemDB.items [i].GetComponent<Item> ();
		IncreaseNumber = IncreaseNumber || itemInfo.IncreaseNumber;
		Bigger = itemInfo.Bigger || Bigger;
	}

	private void DebuffPlayerData(int i){
		Item itemInfo = ItemDB.items [i].GetComponent<Item> ();
		IncreaseNumber = (itemInfo.IncreaseNumber)? false : IncreaseNumber;
		Bigger = (itemInfo.Bigger)? false : Bigger;
	}

	// only change spell id 
	private void UpdateFireballID (){
		if (IncreaseNumber && Bigger)
			pd.spellID = SpellDB.AttackID.morebigfireball;
		if (IncreaseNumber && !Bigger)
			pd.spellID = SpellDB.AttackID.morefireball;
		if (!IncreaseNumber && Bigger)
			pd.spellID = SpellDB.AttackID.bigfireball;
		if (!IncreaseNumber && !Bigger)
			pd.spellID = SpellDB.AttackID.fireball;
		if (pd.ice_fire == Constants.SpellMode.Ice)
			pd.spellID += 4;
	}
}
