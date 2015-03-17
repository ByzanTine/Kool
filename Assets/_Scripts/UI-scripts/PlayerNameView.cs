using UnityEngine;
using System.Collections;
using UnityEngine.UI;
[RequireComponent(typeof(Text))]
public class PlayerNameView : MonoBehaviour {

	// use a hot link rather than playerId 
	public GameObject Player;
	public int id;
	private Camera camera;
	PlayerData pd;
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
		pd = Player.GetComponent<PlayerData> ();
		if (!pd) {
			Debug.LogError("[UI] No Player attached to this health bar");
			
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (pd) {

			// get.sizeDelta *= 10.0f / Camera.main.fieldOfView;
			// get the new transform position
			Vector3 rawPos = camera.WorldToScreenPoint (Player.transform.position);
			rawPos.y += heightOffset / camera.fieldOfView;
			rawPos.x -= widthOffset / camera.fieldOfView;
			
			
			transform.position = rawPos;
			GetComponent<RectTransform>().sizeDelta =  rectOrigin * 10.0f/Camera.main.fieldOfView;
			// Debug.Log(pd.health);
			textUI.fontSize = Mathf.RoundToInt(originSize * 10.0f/Camera.main.fieldOfView);
			textUI.text = "Weddy";

		}
		else {
			// TODO fetch the player by id, and get the player data
			
		}
		
		
	}
}
