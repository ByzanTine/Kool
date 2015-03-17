using UnityEngine;
using System.Collections;

public class IceBurstSpell : MagicSpell {

	private GameObject iceBurst;
	
	public IceBurstSpell()
	{
		iceBurst = SpellDB.iceBurst;
		// Debug.Log("Constructor Loaded");
	}
	public override IEnumerator castMagic (GameObject caster, Vector3 hitpoint = default(Vector3)) 
	{

		GameObject gb = GameObject.Instantiate (iceBurst, 
		                                        caster.transform.position, 
		                                        Quaternion.identity) as GameObject;
		gb.GetComponent<IceBurstControl> ().caster = caster;
		
	
		yield return new WaitForSeconds(0.1f);
	}
}
