using UnityEngine;
using System.Collections;

public class PlayerPickUpItem : MonoBehaviour {
	private PlayerBuffStatus BH;
	private WizardAttackMeans WAM;
	private PlayerData pd;

	void Start (){
		BH = GetComponent<PlayerBuffStatus> ();
		WAM = GetComponent<WizardAttackMeans> ();
		pd = GetComponent<PlayerData> ();
	}
	// Update is called once per frame
	public void PickUp (SpellDB.AttackID ID) {
		BH.UpdateBuff (ID);

	}
}
