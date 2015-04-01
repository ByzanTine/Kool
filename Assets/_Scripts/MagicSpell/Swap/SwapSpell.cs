using UnityEngine;
using System.Collections;

public class SwapSpell : MagicSpell {
	private GameObject swapSpell;
	
	public SwapSpell()
	{
		swapSpell = SpellDB.swap;
	}

	public override IEnumerator castMagic(GameObject caster, Vector3 hitpoint = default(Vector3)) {

		Quaternion lookedQua = Quaternion.LookRotation (caster.transform.forward);
		GameObject gb = GameObject.Instantiate (swapSpell, caster.transform.position, lookedQua) as GameObject;
		gb.GetComponent<Swapper> ().caster = caster;
		yield return null;
	}
}
