using UnityEngine;
using System.Collections;

public class ExplodeLink : MonoBehaviour {
	public GameObject caster;
	public GameObject explodePrefab;
	/// <summary>
	/// Casters the delegate destroy. 
	/// </summary>
	/// <param name="position">Position. Default is position </param>
	public void CasterDelegateDestroy (Vector3 position) {
		Instantiate(explodePrefab, position, Quaternion.identity);
		explodePrefab.GetComponent<ColliderExplode> ().caster = caster;
		Destroy(gameObject);
	}
}
