using UnityEngine;
using System.Collections;

public class PickUpItem : MonoBehaviour {
	private BuffHandler BH;
	private WizardAttackMeans WAM;
	private PlayerData pd;

	void Start (){
		BH = GetComponent<BuffHandler> ();
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
			} else if (ID == SpellDB.AttackID.meteor){
				pd.SpecialSpellID = ID;
			}
			Debug.Log ("da zhao");// TODO da zhao
		}
	}
}
