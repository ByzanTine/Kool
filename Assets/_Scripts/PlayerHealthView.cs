using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealthView : MonoBehaviour {
	// use a hot link rather than playerId 
	public GameObject Player;
	PlayerData pd;
	BarControl barCon;
	// Use this for initialization
	void Start () {

		pd = Player.GetComponent<PlayerData> ();
		if (!pd) {
			Debug.LogError("[UI] No Player attached to this health bar");

		}
	}
	
	// Update is called once per frame
	void Update () {
		if (pd) {
			barCon = GetComponentInChildren<BarControl>();
			barCon.SetBar(pd.health);
			// Debug.Log(pd.health);

		}
	}
}
