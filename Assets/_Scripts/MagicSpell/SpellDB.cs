using UnityEngine;
using System.Collections;

public class SpellDB : MonoBehaviour {
	// TODO scale this up!
	// NOTICE: Still has a coupling with the WizardState
	 public enum AttackID{
		fireball,
		morefireball,
		bigfireball,
		morebigfireball,
		iceball,
		moreiceball,
		bigiceball,
		morebigiceball,
		meteor,
		reflect,
		swap,
		iceBurst,
		fireNova,
		None
	};

	static public string[] attackIDnames = {
		"fireball", 
		"morefireball",
		"bigfireball",
		"morebigfireball",
		"iceball",  
		"moreiceball", 
		"bigiceball", 
		"morebigiceball", 
		"meteor", 
		"reflect", 
		"swap",
		"iceBurst", 
		"fireNova",
		"None"
	};
	static public float[] SPELL_DAMAGE = {0.1f, 0.1f, 0.3f, 0.2f, 0.6f};// 0 for fireball, 1 for iceball, 2 for iceburst, 3 for big fire ball, 4 for meteor

	static public Hashtable spellDamage;

	static public float GetSpellDamage (AttackID index) {
		if (spellDamage.ContainsKey(index)) {
			return (float)spellDamage[index];
		}
		else {
			Debug.LogError("[SpellDB] no object with the index" + attackIDnames[(int)index]);
			return 0.0f; // not crashed anyway
		}
	}

	// public GameObject meteor_;
	static public GameObject fireball;
	static public GameObject iceball;
	static public GameObject meteor;
	static public GameObject reflector;
	static public GameObject swap;
	static public GameObject iceBurst;
	static public GameObject Bigfireball;
	static public GameObject Bigiceball;
	static public GameObject fireNova;

	void Awake()
	{
		Debug.Log("INIT: Create Reference to Spells");
		spellDamage = new Hashtable () {
			{AttackID.fireball, 	0.1f},
			{AttackID.iceball,  	0.08f},
			{AttackID.iceBurst, 	0.3f},
			{AttackID.bigfireball, 	0.2f},
			{AttackID.bigiceball, 	0.16f},
			{AttackID.meteor, 		0.6f}
		};

		InitSpells ();
	}
	// May take some time
	void InitSpells () {

		fireball = Resources.Load ("MagicSpells/Fireball") as GameObject;
		Bigfireball = Resources.Load ("MagicSpells/BigFireball") as GameObject;
		Bigiceball = Resources.Load ("MagicSpells/BigIceball") as GameObject;
		meteor = Resources.Load ("MagicSpells/meteorController") as GameObject;
		iceball = Resources.Load ("MagicSpells/Iceball") as GameObject;
		reflector = Resources.Load ("MagicSpells/Reflector") as GameObject;
		// aerolite = Resources.Load ("MagicSpells/Aerolite") as GameObject;
		fireNova = Resources.Load ("MagicSpells/fireNova") as GameObject;
		swap = Resources.Load ("MagicSpells/Swapper") as GameObject;
		iceBurst = Resources.Load ("MagicSpells/IceBurst/IceBurst") as GameObject;
	}
}
