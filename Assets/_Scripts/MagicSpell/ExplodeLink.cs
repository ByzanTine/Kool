using UnityEngine;
using System.Collections;

public class ExplodeLink : MonoBehaviour {
	public GameObject caster;
	public GameObject explodePrefab;
	/// <summary>
	/// Casters the delegate destory. 
	/// </summary>
	/// <param name="position">Position. Default is position </param>
	public void CasterDelegateDestory (Vector3 position) {
		Instantiate(explodePrefab, position, Quaternion.identity);
		explodePrefab.GetComponent<ColliderExplode> ().caster = caster;
		Destroy(gameObject);
	}
}
