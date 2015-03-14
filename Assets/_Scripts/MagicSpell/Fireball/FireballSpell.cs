using UnityEngine;
using System.Collections;

public class FireballSpell : MagicSpell {
	private GameObject fireball;

	public FireballSpell()
	{
		fireball = SpellDB.fireball;
		// Debug.Log("Constructor Loaded");
	}
	public override IEnumerator castMagic (GameObject caster, Vector3 hitpoint = default(Vector3)) 
	{

//		Debug.Log("Fireball Activiated!");
//		float transformed_angle = Vector3.Angle (new Vector3 (0, 0, 1), hitpoint-caster.transform.position);
//
//		Debug.Log ("Angle: " + transformed_angle);
		Quaternion lookedQua = Quaternion.LookRotation (hitpoint - caster.transform.position);
		for (int i = 0; i < 4; i++) { // TODO num of fireballs 
			float randomAngle = Random.Range(-10.0f, 10.0f); // TODO range angles 

			GameObject gb = GameObject.Instantiate (fireball, caster.transform.position, lookedQua) as GameObject;
			gb.transform.Rotate(gb.transform.up, randomAngle, Space.Self);

			MovableUnit movUnit = gb.GetComponent<MovableUnit> ();
			Vector3 newHitPoint = MathUtil.RotatePointAroundPivot(hitpoint, caster.transform.position, 
			                                                      new Vector3(0, randomAngle, 0));
			movUnit.MoveTo (newHitPoint);
			yield return new WaitForSeconds(0.1f);

		}
		// GameObject gb = GameObject.Instantiate (fireball, caster.transform.position, lookedQua) as GameObject;



		yield return null;
	}
}
