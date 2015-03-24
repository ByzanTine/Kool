using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerBuffStatus : MonoBehaviour {
	public float effectiveTime = 3;
	private List<float> Buff_time_List;	// keep track of the cool down time for all the buffs
	private List<bool> Buff_valid_List;	// keep track of the all the buff which is valid 


	private bool IncreaseNumber = false;
	private bool Bigger = false;

	// some private components
	private WizardAttackMeans wizardAttackMeans;
	private PlayerData pd;
	// Use this for initialization
	void Start () {
		Buff_time_List = new List<float>();
		Buff_valid_List = new List<bool> ();
		pd = GetComponent<PlayerData> ();
		wizardAttackMeans = GetComponent<WizardAttackMeans> ();

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
	// event delegate 
	// TODO extend this interface if more parameter is needed
	public void UpdateBuff(SpellDB.AttackID mode){
		// switch on ID
		switch (mode) {
		case SpellDB.AttackID.reflect:
			// cast directly
			// may not do animation somehow
			wizardAttackMeans.AttackByDiretion(SpellDB.AttackID.reflect, transform.position);
			break;
		case SpellDB.AttackID.iceBurst:
			pd.SpecialSpellID = mode;
			break;
		default:
			Buff_time_List [(int)mode] = Time.time;
			//ChangeIceFire ();
			if (!Buff_valid_List [(int)mode]){
				BuffPlayerData (mode);
				Buff_valid_List [(int)mode] = true;
			}
			break;
		}

	}

	private void BuffPlayerData(SpellDB.AttackID mode){
		IncreaseNumber = IncreaseNumber || (mode == SpellDB.AttackID.morefireball)? true : false;
		Bigger = Bigger || (mode == SpellDB.AttackID.bigfireball)? true : false;
	}

	private void DebuffPlayerData(int mode){
		IncreaseNumber = (mode == (int)SpellDB.AttackID.morefireball)? false : IncreaseNumber;
		Bigger = (mode == (int)SpellDB.AttackID.bigfireball)? false : Bigger;
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
