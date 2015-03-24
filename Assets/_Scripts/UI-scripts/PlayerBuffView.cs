using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PlayerBuffView : MonoBehaviour {
	public int playerId;

	private Image image;
	private GameObject Player;
	private PlayerBuffStatus playerBuffStatus;
	// Use this for initialization
	void Start () {
		image = GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
//		image.fillAmount = ;
		if (playerBuffStatus) {
			image.fillAmount = playerBuffStatus.RemainBuffTime/playerBuffStatus.effectiveTime;
			// Debug.Log(pd.health);
			
		}
		else {
			// fetch the player by id, and get the player buff status 
			Player = GameStatus.GetPlayerObjById(playerId);
			if(Player)
				playerBuffStatus = Player.GetComponent<PlayerBuffStatus> ();
		}
	}
}
