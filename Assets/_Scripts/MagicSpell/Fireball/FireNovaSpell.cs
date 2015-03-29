using UnityEngine;
using System.Collections;

public class FireNovaSpell : FireballSpell {

	public FireNovaSpell() : base (1, 1, 1.0f){
		fireball = SpellDB.fireNova;
		
	}
	public override IEnumerator castMagic (GameObject caster, Vector3 hitpoint = default(Vector3)) 
	{
		
		//		Debug.Log("Fireball Activiated!");
		//		float transformed_angle = Vector3.Angle (new Vector3 (0, 0, 1), hitpoint-caster.transform.position);
		//
		//		Debug.Log ("Angle: " + transformed_angle);
		Quaternion lookedQua = Quaternion.LookRotation (hitpoint - caster.transform.position);
		for (int i = 0; i < 1; i++) { // TODO num of fireballs 
			float randomAngle = Random.Range(-1 * 1, 1); // TODO range angles 
			
			GameObject gb = GameObject.Instantiate (fireball, caster.transform.position, lookedQua) as GameObject;
			//  add caster delegate 
			NovaExplodeLink explodeLink = gb.GetComponent<NovaExplodeLink>();
			if (explodeLink) {
				explodeLink.caster = caster;
				explodeLink.depth = 2;
			}
			else {
				Debug.LogWarning("[Spell] Spell has no explode delegate");
			}
			
			
			// scale 
			
			
			gb.transform.Rotate(gb.transform.up, randomAngle, Space.Self);
			
			MovableUnit movUnit = gb.GetComponent<MovableUnit> ();
			Vector3 newHitPoint = MathUtil.RotatePointAroundPivot(hitpoint, caster.transform.position, 
			                                                      new Vector3(0, randomAngle, 0));
			movUnit.MoveTo (newHitPoint);
			yield return new WaitForSeconds(TimeInterval);
			
		}
		// GameObject gb = GameObject.Instantiate (fireball, caster.transform.position, lookedQua) as GameObject;
		
		
		
		yield return null;
	}
}
