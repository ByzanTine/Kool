using UnityEngine;
using System.Collections;
using UnityEngine.UI;
[RequireComponent(typeof(Text))]
public class PlayerNameView : MonoBehaviour {

	// use a hot link rather than playerId 
	GameObject Player;
	public int playerId;
	private Camera viewCamera;
	private PlayerData pd;
	private BarControl barCon;
	
	// now for y-axis
	private float heightOffset = 320.0f; 
	private float widthOffset = 25.0f;
	private Vector2 rectOrigin;
	private float originSize;
	// Text UI
	private Text textUI;

	// Use this for initialization
	void Start () {
		// adjust Position.
		viewCamera = Camera.main;
		rectOrigin = GetComponent<RectTransform> ().sizeDelta;
		textUI = GetComponent<Text> ();
		originSize = textUI.fontSize;

	}
	
	// Update is called once per frame
	void Update () {
		if (pd) {
			textUI.enabled = true;
			// get.sizeDelta *= 10.0f / Camera.main.fieldOfView;
			// get the new transform position
			Vector3 rawPos = viewCamera.WorldToScreenPoint (Player.transform.position);
			rawPos.y += heightOffset / viewCamera.fieldOfView;
			rawPos.x -= widthOffset / viewCamera.fieldOfView;

			transform.position = rawPos;
			GetComponent<RectTransform>().sizeDelta =  rectOrigin * 10.0f/viewCamera.fieldOfView;
			// Debug.Log(pd.health);
			textUI.fontSize = Mathf.RoundToInt(originSize * 10.0f/viewCamera.fieldOfView);
			textUI.text = GameStatus.Instance.Usernames[playerId];
			textUI.color = GameStatus.Instance.UserColors[playerId];
			// Debug.Log ("Printing name");
		}
		else {
			textUI.enabled = false;
			Player = GameStatus.GetPlayerObjById(playerId);
			if(Player)
				pd = Player.GetComponent<PlayerData> ();
		}
	}
	

}
