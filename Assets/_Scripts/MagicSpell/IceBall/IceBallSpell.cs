using UnityEngine;
using System.Collections;

public class IceBallSpell : MagicSpell {
	private GameObject iceball;
	private int NumberOfBalls;
	private float Range;
	private float scale;
	/// <summary>
	/// Initializes a new instance of the <see cref="FireballSpell"/> class.
	/// </summary>
	public IceBallSpell()
	{
		NumberOfBalls = 4;
		Range = 10;
		scale = 1;
		iceball = SpellDB.iceball;
	}
	/// <summary>
	/// Initializes a new instance of the <see cref="FireballSpell"/> class.
	/// </summary>
	/// <param name="num">Number.</param>
	/// <param name="range">Range.</param>
	/// <param name="scale_">Scale_.</param>
	
	public IceBallSpell(int num, float range, float scale_)
	{
		NumberOfBalls = num;
		Range = range;
		scale = scale_;
		iceball = SpellDB.iceball;
	}
	

	public override IEnumerator castMagic (GameObject caster, Vector3 hitpoint = default(Vector3)) 
	{
//		Quaternion lookedQua = Quaternion.LookRotation (hitpoint - caster.transform.position);
//		GameObject gb = GameObject.Instantiate (iceball, caster.transform.position, lookedQua) as GameObject;
//		
//		
//		MovableUnit movUnit = gb.GetComponent<MovableUnit> ();
//		movUnit.MoveTo (hitpoint);
//		yield return new WaitForSeconds(0.1f);

		Quaternion lookedQua = Quaternion.LookRotation (hitpoint - caster.transform.position);
		for (int i = 0; i < NumberOfBalls; i++) { // TODO num of fireballs 
			float randomAngle = Random.Range(-1 * Range, Range); // TODO range angles 
			
			GameObject gb = GameObject.Instantiate (iceball, caster.transform.position, lookedQua) as GameObject;
			gb.transform.localScale *= scale;
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
