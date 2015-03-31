using UnityEngine;
using System.Collections;

public class NovaExplodeLink : ExplodeLink {
	public int depth;
	/// <summary>
	/// Casters the delegate destroy. 
	/// </summary>
	/// <param name="position">Position. Default is position </param>
	public override void CasterDelegateDestroy (Vector3 position) {

		GameObject gb = Instantiate(explodePrefab, position, Quaternion.identity) as GameObject;
		gb.GetComponent<NovaColliderExplode> ().caster = caster;
		gb.GetComponent<NovaColliderExplode> ().depth = depth;
		Destroy(gameObject);
	}
}
