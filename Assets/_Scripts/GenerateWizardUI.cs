using UnityEngine;
using System.Collections;

public class GenerateWizardUI : MonoBehaviour {

	public GameObject playerHealthPrefab;
	public GameObject namePrefab;
	public GameObject specialSpellPrefab;
	public GameObject buffStatusPrefab;
	// Use this for initialization
	void Start () {
		StartCoroutine (LateGenerateWizardUI ());
	}

	IEnumerator LateGenerateWizardUI()
	{
		// wait for load scene done
		Debug.Log ("[UI] Prepare load player");

		yield return new WaitForSeconds (0.1f);
		GameObject[] playerCollection = GameObject.FindGameObjectsWithTag (TagList.Player);
		foreach(GameObject player in playerCollection)
		{

			UserInputManager userCtrl = player.GetComponent<UserInputManager>();
			// assign player ID
			int playerId = userCtrl.playerNum;
			GameObject health = Instantiate(playerHealthPrefab) as GameObject;
			health.transform.SetParent(this.transform);
			GameObject name = Instantiate(namePrefab) as GameObject;
			name.transform.SetParent(this.transform);
			GameObject specialSpell = Instantiate(specialSpellPrefab) as GameObject;
			specialSpell.transform.SetParent(this.transform);

			// Generate a circle buff indicator 

			GameObject buffstatusUI = Instantiate(buffStatusPrefab, 
			                                      player.transform.position, 
			                                      player.transform.rotation) as GameObject;
			buffstatusUI.transform.SetParent(player.transform);


			PlayerHealthView healthUI  = health.GetComponent<PlayerHealthView>();
			PlayerNameView nameUI = name.GetComponent<PlayerNameView>();
			SpecialSpellView specialSpellUI = specialSpell.GetComponent<SpecialSpellView>();
			PlayerBuffView buffUI = buffstatusUI.GetComponentInChildren<PlayerBuffView>();

			buffUI.playerId = playerId;
			healthUI.playerId = playerId;
			nameUI.playerId = playerId;
			specialSpellUI.playerId = playerId;
		}
	}
	
	// Update is called once per frame
	void Update () {
	}
}
