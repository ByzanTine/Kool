using UnityEngine;
using System.Collections;

public class ExplodeLink : MonoBehaviour {
	public GameObject caster;
	public GameObject explodePrefab;
	/// <summary>
	/// Casters the delegate destroy. 
	/// </summary>
	/// <param name="position">Position. Default is position </param>
	public virtual void CasterDelegateDestroy (Vector3 position) {
		GameObject gb = Instantiate(explodePrefab, position, Quaternion.identity) as GameObject;
		gb.GetComponent<ColliderExplode> ().caster = caster;
		Destroy(gameObject);
	}

}
