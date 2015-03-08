using UnityEngine;
using System.Collections;

public class playerData : MonoBehaviour {
	public float health = 100;
	public float mana = 1;
	public bool frozen; //TODO all the buff status, frozen is just a example;
	public GameObject HPBar;
	private BarControl HPbarControl;

	void Start () {
		HPbarControl = HPBar.GetComponent<BarControl> ();
	}

	public void damageHP (float damage){
		health -= damage;
		HPbarControl.SetBar (health);
	}
}
