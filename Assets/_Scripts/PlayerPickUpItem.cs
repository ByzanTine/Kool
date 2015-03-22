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
	public void PickUp (int mode, bool isBuff, SpellDB.AttackID ID) {
		if (isBuff)
			BH.UpdateBuff (mode);
		else {
			if (ID == SpellDB.AttackID.reflect) // reflect
			{
				WAM.AttackToPosition (ID);
			} else {
				pd.SpecialSpellID = ID;
			}
			Debug.Log ("[Spell] Super Skill");// TODO da zhao
		}
	}
}
