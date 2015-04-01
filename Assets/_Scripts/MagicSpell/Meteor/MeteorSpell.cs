using UnityEngine;
using System.Collections;

public class MeteorSpell : MagicSpell {
	
	private GameObject meteorController;
	public MeteorSpell()
	{
		meteorController = SpellDB.meteor;
	}
	public override IEnumerator castMagic (GameObject caster, Vector3 hitpoint = default(Vector3)) 
	{
		
		Debug.Log("[Spell] Meteor Activiated!");
		GameObject gb = GameObject.Instantiate (meteorController, 
		                                        default(Vector3), 
		                                        Quaternion.identity) as GameObject;
		gb.GetComponent<MeteorController> ().caster = caster;
		yield return new WaitForSeconds (1.0f);

		// drop from a higher place
		// meteorController.GetComponent<MeteorController> ().MeteorDrop ();

		yield return null;
	}


}
