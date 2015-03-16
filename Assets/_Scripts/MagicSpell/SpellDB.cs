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
		meteor,
		reflect,
		swap
	};

	static public string[] attackIDnames = {"fireball", "iceball", "meteor", "reflect", "swap"};
	
	// public GameObject meteor_;
	static public GameObject fireball;
	static public GameObject iceball;
	static public GameObject meteor;
	static public GameObject reflector;
	static public GameObject swap;

	// static public GameObject aerolite;

	void Awake()
	{
		Debug.Log("INIT: Create Reference to Spells");

		InitSpells ();
	}
	// May take some time
	void InitSpells () {

		fireball = Resources.Load ("MagicSpells/Fireball") as GameObject;
		meteor = Resources.Load ("MagicSpells/meteor") as GameObject;
		iceball = Resources.Load ("MagicSpells/Iceball") as GameObject;
		reflector = Resources.Load ("MagicSpells/Reflector") as GameObject;
		// aerolite = Resources.Load ("MagicSpells/Aerolite") as GameObject;

		swap = Resources.Load ("MagicSpells/Swapper") as GameObject;
	}
}
