using UnityEngine;
using System.Collections;
using UnityEngine.UI;
[RequireComponent(typeof(Image))]

public class SpecialSpellView : MonoBehaviour {
	
	public int playerId;
	private Camera viewCamera;
	PlayerData pd;
	GameObject Player;
	public Sprite Iceburst;
	public Sprite Meteor;
	public Sprite Reflect;

	// now for y-axis
	private float heightOffset = 435.0f; 
	private float widthOffset = 25f;
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
			switch (pd.SpecialSpellID)
			{
				case SpellDB.AttackID.iceBurst:
					img.sprite = Iceburst;
					img.enabled = true;
					break;
				case SpellDB.AttackID.meteor:
					img.sprite = Meteor;
					img.enabled = true;	
					break;
				default:
					// Debug.Log ("Not a special Spell");
					img.enabled = false;
					break;
			}


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
			if(Player)
				pd = Player.GetComponent<PlayerData> ();
		}
	}

	
	
}
