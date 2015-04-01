using UnityEngine;
using System.Collections;

public class PlayerData : MonoBehaviour {

	public SpellDB.AttackID spellID = SpellDB.AttackID.fireball;



	public int teamNum = 0;
	public float health = 1;
	public float mana = 1;
	public bool frozen; //TODO all the buff status, frozen is just a example;
	// public GameObject HPBar;
	public Constants.SpellMode ice_fire = Constants.SpellMode.Fire;
//	public GameObject ManaBar;
	// private BarControl HPbarControl;
	private BarControl ManabarControl;
	private PlayerControl playerCtrl;
	public float increment = 0.05f;

	public SpellDB.AttackID SpecialSpellID = SpellDB.AttackID.None;

	public bool isAlive = true;

	Vector3 damagePosDelta = new Vector3(-1.0f,0,7.0f);
	void Start () {
		// HPbarControl = HPBar.GetComponent<BarControl> ();
//		ManabarControl = ManaBar.GetComponent<BarControl> ();
	}

	public void DamageHP (float damage){
		// damage effect
		GameObject damageNumberPrefab = Resources.Load ("PlayerEffect/DamageNumber") as GameObject;
		Vector3 pos = transform.position;
		pos += damagePosDelta;
		GameObject newDamageNum = Instantiate (damageNumberPrefab, pos, Quaternion.identity) as GameObject;
		newDamageNum.GetComponent<DamageNumber> ().number = (int)(damage * 1000f * Random.Range(0.9f,1.1f));


		health -= damage;

		// HPbarControl.SetBar (health);

		// update player status
		if(health <= 0 && isAlive)
		{
			isAlive = false;
			DecreaseMana(mana);
//			Destroy(this.gameObject);
			playerCtrl = GetComponent<PlayerControl> ();
			playerCtrl.Die();
			return;
		}
		health = Mathf.Clamp (health, 0.0f, 1.0f);
	}

	public bool DecreaseMana(float Dmana){
		if (Dmana < mana) {
			mana -= Dmana;
//			ManabarControl.SetBar (mana);
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
//		ManabarControl.SetBar (mana);
	}

	public void ChangeIceFire(){
		ice_fire = (ice_fire == Constants.SpellMode.Fire) ? 
			Constants.SpellMode.Ice : Constants.SpellMode.Fire;
		// HACK change special attack as well
		if (SpecialSpellID == SpellDB.AttackID.meteor && ice_fire == Constants.SpellMode.Ice)
			SpecialSpellID = SpellDB.AttackID.iceBurst;
		if (SpecialSpellID == SpellDB.AttackID.iceBurst && ice_fire == Constants.SpellMode.Fire)
			SpecialSpellID = SpellDB.AttackID.meteor;


		Debug.Log ("[Spell] spell id now is : " + spellID);
	}

	public void frozenCheck (){
		if (frozen) {
			Debug.Log("get frozen and slow down");
			// TODO slow down the speed
		}
	}
}
