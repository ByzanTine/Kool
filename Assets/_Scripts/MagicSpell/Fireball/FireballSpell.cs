using UnityEngine;
using System.Collections;

public class FireballSpell : MagicSpell {
	public GameObject fireball;
	private int NumberOfBalls;
	private float Range;
	private float scale;
	/// <summary>
	/// Initializes a new instance of the <see cref="FireballSpell"/> class.
	/// </summary>
	public FireballSpell()
	{
		NumberOfBalls = 2;
		Range = 10;
		scale = 1;
		fireball = SpellDB.fireball;
	}
	/// <summary>
	/// Initializes a new instance of the <see cref="FireballSpell"/> class.
	/// </summary>
	/// <param name="num">Number.</param>
	/// <param name="range">Range.</param>
	/// <param name="scale_">Scale_.</param>

	public FireballSpell(int num, float range, float scale_)
	{
		NumberOfBalls = num;
		Range = range;
		scale = scale_;
		fireball = SpellDB.fireball;
	}
	// working with particle system and collider delay 
	private void ScaleFireball(GameObject gb, float scale) {
		gb.transform.localScale *= scale; // only fixed the collider size
		// get the particle system

	}



	public override IEnumerator castMagic (GameObject caster, Vector3 hitpoint = default(Vector3)) 
	{

//		Debug.Log("Fireball Activiated!");
//		float transformed_angle = Vector3.Angle (new Vector3 (0, 0, 1), hitpoint-caster.transform.position);
//
//		Debug.Log ("Angle: " + transformed_angle);
		Quaternion lookedQua = Quaternion.LookRotation (hitpoint - caster.transform.position);
		for (int i = 0; i < NumberOfBalls; i++) { // TODO num of fireballs 
			float randomAngle = Random.Range(-1 * Range, Range); // TODO range angles 

			GameObject gb = GameObject.Instantiate (fireball, caster.transform.position, lookedQua) as GameObject;
			//  add caster delegate 
			ExplodeLink explodeLink = gb.GetComponent<ExplodeLink>();
			if (explodeLink) {
				explodeLink.caster = caster;
			}
			else {
				Debug.LogWarning("[Spell] Spell has no explode delegate");
			}


			// scale 
			ScaleFireball(gb, scale);


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
