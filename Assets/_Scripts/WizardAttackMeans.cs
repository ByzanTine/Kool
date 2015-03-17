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
			new MoreFireballSpell(),
			new BigFireballSpell(),
			new MoreBigFireballSpell(),
			new IceBallSpell(),
			new MeteorSpell(),
			// new AeroSpell(), 
			new ReflectSpell(),
			new SwapSpell(),
			new IceBurstSpell()

		};

		wizardAnimator = gameObject.GetComponentInChildren<Animator> ();
	}

	private IEnumerator AttackByPosition(SpellDB.AttackID id, Vector3 to = default(Vector3)){
		magicSpell = magicPool[(int)id];
		PlayerData pD = GetComponent<PlayerData>();
		if (pD.DecreaseMana (Constants.FIREBALL_MANA_COST)) {// TODO Mana cost is constant now
			wizardAnimator.SetBool ("isCasting", true);
			yield return new WaitForSeconds (Constants.MIN_CAST_COOL_DOWN);

			StartCoroutine (magicSpell.castMagic (gameObject, to));

			Debug.Log ("[Spell] Attack using " + SpellDB.attackIDnames [(int)id]);

			wizardAnimator.SetBool ("isCasting", false);
		} else {
			Debug.Log ("[Spell] not enough mana!");
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
