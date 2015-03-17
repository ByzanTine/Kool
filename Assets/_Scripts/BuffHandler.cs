using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuffHandler : MonoBehaviour {
	public float effectiveTime = 3;
	private List<float> Buff_time_List;	// keep track of the cool down time for all the buffs
	private List<bool> Buff_valid_List;	// keep track of the all the buff which is valid 
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
			if ((Time.time - Buff_time_List [i] > effectiveTime )&& Buff_valid_List[i]) {// if it is not effective
				Buff_valid_List[i] = false;
				DebuffPlayerData(i);
			}
		}
		updateFireballID ();
	}

	public void UpdateBuff(int mode){
		Buff_time_List [mode] = Time.time;
		if (!Buff_valid_List [mode]){
			BuffPlayerData (mode);
			Buff_valid_List [mode] = true;
		}
	}

	private void BuffPlayerData(int i){
		Item itemInfo = ItemDB.items [i].GetComponent<Item> ();
		pd.IncreaseNumber = pd.IncreaseNumber || itemInfo.IncreaseNumber;
		pd.Bigger = itemInfo.Bigger || pd.Bigger;
	}

	private void DebuffPlayerData(int i){
		Item itemInfo = ItemDB.items [i].GetComponent<Item> ();
		pd.IncreaseNumber = (itemInfo.IncreaseNumber)? false : pd.IncreaseNumber;
		pd.Bigger = (itemInfo.Bigger)? false : pd.Bigger;
	}

	private void updateFireballID (){
		if (pd.IncreaseNumber && pd.Bigger)
			pd.spellID = SpellDB.AttackID.morebigfireball;
		if (pd.IncreaseNumber && !pd.Bigger)
			pd.spellID = SpellDB.AttackID.morefireball;
		if (!pd.IncreaseNumber && pd.Bigger)
			pd.spellID = SpellDB.AttackID.bigfireball;
		if (!pd.IncreaseNumber && !pd.Bigger)
			pd.spellID = SpellDB.AttackID.fireball;
	}
}
