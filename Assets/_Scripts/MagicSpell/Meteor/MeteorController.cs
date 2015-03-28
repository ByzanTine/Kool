using UnityEngine;
using System.Collections;

public class MeteorController : MonoBehaviour {
	public GameObject meteor;
	public GameObject targetIndicator;
	public GameObject caster;
	// Use this for initialization
	void Start () {
		targetIndicator = Resources.Load ("MagicSpells/CFXM3_MagicAura_B_Runic") as GameObject;
		meteor = Resources.Load ("MagicSpells/Meteor/meteor") as GameObject;
		MeteorDrop ();
	}

	public void MeteorDrop() {

		GameObject[] players = GameObject.FindGameObjectsWithTag (TagList.Player);
		foreach (GameObject player in players) {
			if (player.GetInstanceID() == caster.GetInstanceID())
				continue;
			
			StartCoroutine(CastMeteorTo(player.transform.position));
		}
	}
	
	private IEnumerator CastMeteorTo(Vector3 targetPosition) {
		Vector3 hitpoint = targetPosition;
		Vector3 castLocation = targetPosition + new Vector3 (0, 20, 0);
		// TODO Hard code
		Quaternion lookedQua = Quaternion.LookRotation (castLocation - hitpoint);
		
		
		
		// TODO create a indicator on the ground 
		
		
		GameObject.Instantiate (targetIndicator, hitpoint, Quaternion.identity);
		yield return new WaitForSeconds (1.0f);
		GameObject gb = GameObject.Instantiate (meteor, castLocation, lookedQua) as GameObject;
		MovableUnit movUnit = gb.GetComponent<MovableUnit> ();
		movUnit.MoveTo (hitpoint);
	}
}
