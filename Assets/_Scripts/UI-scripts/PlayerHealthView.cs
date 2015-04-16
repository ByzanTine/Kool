using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealthView : MonoBehaviour {
	// use a hot link rather than playerId 
	public int playerId;
	private Camera viewCamera;
	PlayerData pd;
	BarControl barCon;
	GameObject Player;

	// now for y-axis
	private float heightOffset = 250.0f; 
	private float widthOffset = 125.0f;
	private Vector2 rectOrigin;
	private Image img;

	
	// Use this for initialization
	void Start () {
		// adjust Position.
		viewCamera = Camera.main;
		rectOrigin = GetComponent<RectTransform> ().sizeDelta;
		img = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {

		if (pd) {
			img.enabled = true;

			barCon = GetComponentInChildren<BarControl>();
			barCon.SetBar(pd.health);

			// get.sizeDelta *= 10.0f / Camera.main.fieldOfView;
			// get the new transform position
			Vector3 rawPos = viewCamera.WorldToScreenPoint (Player.transform.position);
			rawPos.y += heightOffset / viewCamera.fieldOfView;
			rawPos.x -= widthOffset / viewCamera.fieldOfView;


			transform.position = rawPos;
			GetComponent<RectTransform>().sizeDelta =  rectOrigin * 10.0f/viewCamera.fieldOfView;
			// Debug.Log(pd.health);

		}
		else {
			// fetch the player by id, and get the player data
			img.enabled = false;
			Player = UserInfoManager.Instance.GetPlayerObjById(playerId);
			if(Player)
				pd = Player.GetComponent<PlayerData> ();
		}
	}






}
