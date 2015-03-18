using UnityEngine;
using System.Collections;
using UnityEngine.UI;
[RequireComponent(typeof(Text))]
public class PlayerNameView : MonoBehaviour {

	// use a hot link rather than playerId 
	GameObject Player;
	public int playerId;
	private Camera camera;
	PlayerData pd;
	GameStatus gameStatus;
	BarControl barCon;
	
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
		camera = Camera.main;
		rectOrigin = GetComponent<RectTransform> ().sizeDelta;
		textUI = GetComponent<Text> ();
		originSize = textUI.fontSize;
		gameStatus = GameObject.Find ("GameStatus").GetComponent<GameStatus>();
	}
	
	// Update is called once per frame
	void Update () {
		if (pd) {
			textUI.enabled = true;
			// get.sizeDelta *= 10.0f / Camera.main.fieldOfView;
			// get the new transform position
			Vector3 rawPos = camera.WorldToScreenPoint (Player.transform.position);
			rawPos.y += heightOffset / camera.fieldOfView;
			rawPos.x -= widthOffset / camera.fieldOfView;

			transform.position = rawPos;
			GetComponent<RectTransform>().sizeDelta =  rectOrigin * 10.0f/Camera.main.fieldOfView;
			// Debug.Log(pd.health);
			textUI.fontSize = Mathf.RoundToInt(originSize * 10.0f/Camera.main.fieldOfView);
			textUI.text = gameStatus.Usernames[playerId];
			textUI.color = gameStatus.UserColors[playerId];
			Debug.Log ("Printing name");
		}
		else {
			textUI.enabled = false;
			GetPlayerObjById(playerId);
			pd = Player.GetComponent<PlayerData> ();
		}
	}
	
	void GetPlayerObjById(int playerId_in)
	{
		GameObject[] playerCollection = GameObject.FindGameObjectsWithTag (TagList.Player);
		foreach(GameObject player in playerCollection)
		{
			UserInputManager userCtrl = player.GetComponent<UserInputManager>();
			if(userCtrl.playerNum == playerId_in)
			{
				Player = player;
				return;
			}
		}
	}
}
