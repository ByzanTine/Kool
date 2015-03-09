using UnityEngine;
using System.Collections;

public class PlayerData : MonoBehaviour {
	public float health = 1;
	public float mana = 1;
	public bool frozen; //TODO all the buff status, frozen is just a example;
	public GameObject HPBar;
	public GameObject ManaBar;
	private BarControl HPbarControl;
	private BarControl ManabarControl;
	public float increment = 0.05f;

	void Start () {
		HPbarControl = HPBar.GetComponent<BarControl> ();
		ManabarControl = ManaBar.GetComponent<BarControl> ();
	}

	public void DamageHP (float damage){
		health -= damage;
		HPbarControl.SetBar (health);
	}

	public bool DecreaseMana(float Dmana){
		if (Dmana < mana) {
			mana -= Dmana;
			ManabarControl.SetBar (mana);
			return true;
		} 
		return false;
	}

	void Update () {
		if (mana <= 1.0){
			AutoIncrement();
		}
	}

	private void AutoIncrement(){
		mana += increment * Time.deltaTime;
		ManabarControl.SetBar (mana);
	}
}
