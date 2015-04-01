using UnityEngine;
using System.Collections;

public class GenerateWizardUI : MonoBehaviour {

	public GameObject playerHealthPrefab;
	public GameObject namePrefab;
	public GameObject specialSpellPrefab;

	// Use this for initialization
	void Start () {
		for(int i = 0; i < GameStatus.TotalPlayerNum; ++i)
		{
			GameObject health = Instantiate(playerHealthPrefab) as GameObject;
			health.transform.SetParent(this.transform);
			GameObject name = Instantiate(namePrefab) as GameObject;
			name.transform.SetParent(this.transform);
			GameObject specialSpell = Instantiate(specialSpellPrefab) as GameObject;
			specialSpell.transform.SetParent(this.transform);

			PlayerHealthView healthUI  = health.GetComponent<PlayerHealthView>();
			PlayerNameView nameUI = name.GetComponent<PlayerNameView>();
			SpecialSpellView specialSpellUI = specialSpell.GetComponent<SpecialSpellView>();

			healthUI.playerId = i;
			nameUI.playerId = i;
			specialSpellUI.playerId = i;
		}
	}
	
	// Update is called once per frame
	void Update () {
	}
}
