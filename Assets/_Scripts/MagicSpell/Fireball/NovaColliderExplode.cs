using UnityEngine;
using System.Collections;

public class NovaColliderExplode : ColliderExplode {
	public GameObject subFireball;
	private int NumberOfBalls = 15;
	private float distance = 15.0f;
	// Use this for initialization
	void Start () {
		if (ExplodeEffect) {
			Instantiate (ExplodeEffect, transform.position, Quaternion.identity);
			// GenerateSphereCast (transform.position);

			StartCoroutine(generateNovaSpell());


		}


	}
	// generate a nova effect 
	// 360 degree spilt
	private IEnumerator generateNovaSpell() {
		Vector3 hitpoint = transform.position + distance * caster.transform.forward;
		
		Quaternion lookedQua = Quaternion.LookRotation (hitpoint - transform.position);
		
		for (int i = 0; i < NumberOfBalls; i++) { // TODO num of fireballs 
			float randomAngle = 360.0f * i/NumberOfBalls; // TODO range angles 
			
			GameObject gb = GameObject.Instantiate (subFireball, transform.position, lookedQua) as GameObject;
			//  add caster delegate 
			ExplodeLink explodeLink = gb.GetComponent<ExplodeLink>();
			if (explodeLink) {
				explodeLink.caster = caster;
			}
			else {
				Debug.LogWarning("[Spell] Spell has no explode delegate");
			}
			
			
			
			gb.transform.Rotate(gb.transform.up, randomAngle, Space.Self);
			
			MovableUnit movUnit = gb.GetComponent<MovableUnit> ();
			Vector3 newHitPoint = MathUtil.RotatePointAroundPivot(hitpoint, transform.position, 
			                                                      new Vector3(0, randomAngle, 0));
			movUnit.MoveTo (newHitPoint);
			yield return new WaitForSeconds(Time.fixedDeltaTime);
			
		}
	}


}
