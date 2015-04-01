using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ColliderExplode))]
public class IceBurstControl : MonoBehaviour {

	// Need to listen on some event for interrupt
	public GameObject caster;

	public float auraShrinkTime;
	private GameObject aura;
	private GameObject iceRing; 
	void Start () {
		// TODO disable control
		// create a aura shrinking.

		aura = Resources.Load ("MagicSpells/IceBurst/CFXM3_DarkMagicAura_B_Runic") as GameObject;
		Instantiate (aura, caster.transform.position, caster.transform.rotation);
		// Control Particle system inside script
		ParticleSystem ps = aura.GetComponent<ParticleSystem> ();
		ps.startLifetime = auraShrinkTime;
		ps.enableEmission = true;
		iceRing = Resources.Load ("MagicSpells/IceBurst/CFXM3_Hit_Ice_B_Ground") as GameObject;
		// after finished 

		// create a iceRing

		StartCoroutine (CreateIceRing());
	}

	IEnumerator CreateIceRing() {
		yield return new WaitForSeconds (auraShrinkTime);

		GameObject iBurst = Instantiate (iceRing, 
		                                 caster.transform.position, 
		                                 caster.transform.rotation) as GameObject;
		// Now use overlap Sphere to detect Players

		ColliderExplode ce = GetComponent<ColliderExplode> ();
		Debug.DrawLine (caster.transform.position, 
		                caster.transform.forward * ce.ExplodeRadius,
		                Color.blue,
		                2.0f);

		ce.GenerateSphereCast (caster.transform.position);


	}


}
