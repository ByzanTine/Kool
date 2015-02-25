using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WizardAttackMeans : MonoBehaviour {


	// Use this for initialization
	private Animator wizardAnimator;
	private MagicSpell magicSpell;
	private List<MagicSpell> magicPool;
	public GameObject muzzle;
	public float scale = 100;
	void Start () {
		int enumSize = System.Enum.GetValues (typeof(SpellDB.AttackID)).Length;
		Debug.Log ("INIT: Number of Spells a wizard can use: " + enumSize);
		magicPool = new List<MagicSpell>{new FireballSpell(), new IceBallSpell(), new MeteorSpell(), new ReflectSpell()};

		wizardAnimator = gameObject.GetComponentInChildren<Animator> ();
	}
	
	public IEnumerator Attack(SpellDB.AttackID id, Vector2 to = default(Vector2)){
		wizardAnimator.SetBool ("Attack", true);
		yield return new WaitForSeconds (1.0f);

		magicSpell = magicPool[(int)id];
		Vector3 newTo = Vector3.zero;
		newTo.x = to.x*scale + transform.position.x;
		newTo.z = to.y*scale + transform.position.z;
		newTo.y = 3;
	
		StartCoroutine (magicSpell.castMagic (muzzle, newTo));

		Debug.Log ("Attack using " + SpellDB.attackIDnames[(int)id]);


		wizardAnimator.SetBool ("Attack", false);
	}

}
