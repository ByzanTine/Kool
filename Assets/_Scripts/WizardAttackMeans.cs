using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WizardAttackMeans : MonoBehaviour {


	// Use this for initialization
	private Animator wizardAnimator;
	private MagicSpell magicSpell;
	private List<MagicSpell> magicPool;

	void Start () {
		int enumSize = System.Enum.GetValues (typeof(SpellDB.AttackID)).Length;
		Debug.Log ("INIT: Number of Spells a wizard can use: " + enumSize);
		magicPool = new List<MagicSpell>
		{
			new FireballSpell(), 
			new IceBallSpell(),
			new MeteorSpell(), 
			new ReflectSpell(),
			new SwapSpell()
		};

		wizardAnimator = gameObject.GetComponentInChildren<Animator> ();
	}

	private IEnumerator AttackByPosition(SpellDB.AttackID id, Vector3 to = default(Vector3)){
		magicSpell = magicPool[(int)id];
		PlayerData pD = GetComponent<PlayerData>();
		if (pD.DecreaseMana (Constants.MIN_CAST_COOL_DOWN)) {// TODO Mana cost is constant now
			wizardAnimator.SetBool ("isCasting", true);
			//yield return new WaitForSeconds (Constants.minCastCoolDown);

			StartCoroutine (magicSpell.castMagic (gameObject, to));

			Debug.Log ("Attack using " + SpellDB.attackIDnames [(int)id]);


			wizardAnimator.SetBool ("isCasting", false);
		} else {
			print ("not enough mana!");
		}
		yield return new WaitForSeconds (0.5f);
	}

	public void AttackToPosition(SpellDB.AttackID id, Vector3 to = default(Vector3)) {
		StartCoroutine (AttackByPosition(id, to));
	}
	public void AttackByDiretion(SpellDB.AttackID id, Vector3 diretion, 
	                                    float distance = Constants.DEFAULT_ATTACK_RADIUS) {

		StartCoroutine (AttackByPosition (id, gameObject.transform.position + distance * diretion.normalized));
	}
}
