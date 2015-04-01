﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerBuffStatus : MonoBehaviour {
	public float effectiveTime = 3;
//	private List<float> Buff_time_List;	// keep track of the cool down time for all the buffs
//	private List<bool> Buff_valid_List;	// keep track of the all the buff which is valid 

	private bool _IncreaseNumber = false;
	public bool IncreaseNumber
	{
		get{return _IncreaseNumber;}
	}

	private bool _Bigger = false;
	public bool Bigger 
	{
		get{return _Bigger;}
	}
	// only need one timer 
	// when eat another, multiply, refresh timer
	// when eat the same, refresh timer
	// when eat special skill or reflect, no need for a timer.
	private float timer; // for public access and view display
	public float RemainBuffTime {
		get {
			return effectiveTime -  Mathf.Clamp(Time.time - timer, 0, effectiveTime);
		}
	}
	// some private components
	private WizardAttackMeans wizardAttackMeans;
	private PlayerData pd;
	// Use this for initialization
	void Start () {
//		Buff_time_List = new List<float>();
//		Buff_valid_List = new List<bool> ();
		pd = GetComponent<PlayerData> ();
		timer = Time.time - effectiveTime;// make sure start remain time is zero
		wizardAttackMeans = GetComponent<WizardAttackMeans> ();

//		for (int i = 0 ; i < ItemDB.Number_Of_Items;i ++){
//			Buff_time_List.Add(0);
//			Buff_valid_List.Add(false);
//		}
	}
	
	// Update is called once per frame
	void Update () {
//		for (int i = 0; i < ItemDB.Number_Of_Items; i ++) {
//			if ((Time.time - Buff_time_List [i] > effectiveTime )&& Buff_valid_List[i]) {// if it is not effective
//				Buff_valid_List[i] = false;
//				DebuffPlayerData(i);
//			}
//		}
//		UpdateFireballID ();
		// Debug.Log ("RemainBuffTime: " + RemainBuffTime);

		if (RemainBuffTime < Time.deltaTime && 
		    (IncreaseNumber || Bigger)) {
			_IncreaseNumber = false;
			_Bigger = false;
		}
		UpdateFireballID();
	}
	// event delegate 
	// TODO extend this interface if more parameter is needed
	public void UpdateBuff(ItemDB.ItemType mode){
		// switch on ID
		switch (mode) {
		case ItemDB.ItemType.protection:
			// cast directly
			// may not do animation somehow
			wizardAttackMeans.AttackByDiretion(SpellDB.AttackID.reflect, transform.position);
			break;
		case ItemDB.ItemType.specialSkill:
			// add one energy point for special skill
			if (pd.ice_fire == Constants.SpellMode.Ice)
				pd.SpecialSpellID = SpellDB.AttackID.iceBurst;
			else if (pd.ice_fire == Constants.SpellMode.Fire)
				pd.SpecialSpellID = SpellDB.AttackID.meteor;
			else 
				Debug.LogError("I don't know what happened");
			break;
		case ItemDB.ItemType.numberBoost:
			// create a timed reset 
			// if alreay sizeBoosted, do number Boost as well
			_IncreaseNumber = true;
			UpdateFireballID();
			timer = Time.time;
			break;
		case ItemDB.ItemType.sizeBoost:
			_Bigger = true;
			UpdateFireballID();
			timer = Time.time;
			break;
		case ItemDB.ItemType.HealthUp:
			pd.DamageHP(-0.5f);
			break;
		}

	}

//	private void BuffPlayerData(SpellDB.AttackID mode){
//		_IncreaseNumber = _IncreaseNumber || (mode == SpellDB.AttackID.morefireball)? true : false;
//		_Bigger = _Bigger || (mode == SpellDB.AttackID.bigfireball)? true : false;
//	}
//
//	private void DebuffPlayerData(int mode){
//		_IncreaseNumber = (mode == (int)SpellDB.AttackID.morefireball)? false : _IncreaseNumber;
//		_Bigger = (mode == (int)SpellDB.AttackID.bigfireball)? false : _Bigger;
//	}

	// only change spell id 
	private void UpdateFireballID (){
		if (_IncreaseNumber && _Bigger)
			pd.spellID = SpellDB.AttackID.morebigfireball;
		if (_IncreaseNumber && !_Bigger)
			pd.spellID = SpellDB.AttackID.morefireball;
		if (!_IncreaseNumber && _Bigger)
			pd.spellID = SpellDB.AttackID.bigfireball;
		if (!_IncreaseNumber && !_Bigger)
			pd.spellID = SpellDB.AttackID.fireball;
		if (pd.ice_fire == Constants.SpellMode.Ice)
			pd.spellID += 4;
	}
}
