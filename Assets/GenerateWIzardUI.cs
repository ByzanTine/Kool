using UnityEngine;
using System.Collections;

public class GenerateWIzardUI : MonoBehaviour {

	public GameObject playerHealthPrefab;
	public GameObject namePrefab;

	// Use this for initialization
	void Start () {
		for(int i = 0; i < GameStatus.TotalPlayerNum; ++i)
		{
			GameObject health = Instantiate(playerHealthPrefab) as GameObject;
			health.transform.parent = transform;
			GameObject name = Instantiate(namePrefab) as GameObject;
			name.transform.parent = transform;

			PlayerHealthView healthUI  = health.GetComponent<PlayerHealthView>();
			PlayerNameView nameUI = name.GetComponent<PlayerNameView>();

			healthUI.playerId = i;
			nameUI.playerId = i;
		}
	}
	
	// Update is called once per frame
	void Update () {
	}
}
