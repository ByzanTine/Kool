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
	public float heightOffset = 500.0f; 
	public float widthOffset = -250.0f;
	public Vector2 rectOrigin;
	private Button button;
	private Image img;
	
	
	// Use this for initialization
	void Start () {
		// adjust Position.
		viewCamera = Camera.main;
		rectOrigin = GetComponent<RectTransform> ().sizeDelta;
		img = GetComponent<Image> ();
		button = GetComponent<Button> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (TV) {

			button.enabled = true;
			img.enabled = true;
			img.sprite = TV.CurButton;
			if (!img.sprite){
				button.enabled = false;
				img.enabled = false;
			}

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
			button.enabled = false;
			Player = UserInfoManager.Instance.GetPlayerObjById(playerId);
			if(Player){
				TV = UnityEngine.Object.FindObjectOfType<TutorialView>();
			}
		}
	}

	public void changeOriginSize(Vector2 newSize){
		rectOrigin = newSize;

	}
}
