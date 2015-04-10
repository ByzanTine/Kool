using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonView : MonoBehaviour {
	// use a hot link rather than playerId 
	public int playerId;
	private Camera viewCamera;
	PlayerData pd;
	TutorialView TV;
	GameObject Player;
	
	// now for y-axis
	private float heightOffset = 800.0f; 
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
		if (TV) {
			Debug.Log("button strat");
			img.enabled = true;
			img.sprite = TV.CurButton;
			if (!img.sprite)
				img.enabled = false;
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
			Player = GameStatus.GetPlayerObjById(playerId);
			if(Player){
				TV = UnityEngine.Object.FindObjectOfType<TutorialView>();
			}
		}
	}
	
	
	
	
	
	
}
