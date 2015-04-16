#define MIDDLE
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UserUIControl : MonoBehaviour {
	public GameObject ReadyEffect;
	private enum Position
	{
		Left,
		Middle,
		Right
	};	
	// Local components
	private UserInputManager inputManager;

	private Text txt;
	private float[] txtPos = new float[3] {-200, 0, 200};
	int moveLock = 0;
#if (MIDDLE)
	private Position position = Position.Middle;
#else 
	private Position position = Position.Left;
#endif

	private void MoveRight() {
#if (MIDDLE)
		switch (position) {
		case Position.Left:
			position = Position.Middle;
			break;
		case Position.Middle:
			position = Position.Right;
			break;
		default:
			break;
		}
#else
		switch (position) {
		case Position.Left:
			position = Position.Right;
			break;
		default:
			break;
		}
#endif
	}

	private void MoveLeft() {
#if (MIDDLE)
		switch (position) {
		case Position.Right:
			position = Position.Middle;
			break;
		case Position.Middle:
			position = Position.Left;
			break;
		default:
			break;
		}
#else
		switch (position) {
		case Position.Right:
			position = Position.Left;
			break;
		default:
			break;
		}
#endif
	}
	// Local variables & local status
	private bool Confirmed = false;
	
	void Start()
	{
		
		// mapping input events
		inputManager = GetComponent<UserInputManager> ();
		//		inputManager.OnPressHit += TryCombatAttack;
		
		inputManager.OnPressConfirm += Confirm;
		inputManager.OnPressBack += Back;
		inputManager.OnPressNavUp += NavLeft;
		inputManager.OnPressNavDown += NavRight;

		txt = GetComponent<Text> ();
		txt.text = GameStatus.UserDataCollection [inputManager.playerNum].Username;
		txt.enabled = false;

		GameStatus.UserDataCollection [inputManager.playerNum].teamID = -1;
		ReadyEffect.SetActive (false);

	}

	IEnumerator BlockedMoveLeft() {
		moveLock = 1;
		yield return new WaitForSeconds (0.2f);
		MoveLeft ();
		SetPosition ();
		moveLock = 0;
	}
	IEnumerator BlockedMoveRight() {
		moveLock = 1;
		yield return new WaitForSeconds (0.2f);
		MoveRight ();
		SetPosition ();
		moveLock = 0;
	}
	void NavLeft()
	{
		// if text invisible yet, don't show
		if(txt.enabled == false) return;

		// if confirmed, don't move then
		if(Confirmed) return;
		if(moveLock > 0) return;
		// if(currentUIPos > 0) currentUIPos--;
		StartCoroutine (BlockedMoveLeft());
	}

	void NavRight()
	{
		// if text invisible yet, don't show
		if(txt.enabled == false) return;

		// if confirmed, don't move then
		if(Confirmed) return;
		if(moveLock > 0) return;
		// if(currentUIPos < 2) currentUIPos++;
		StartCoroutine (BlockedMoveRight ());
	}

	void SetPosition()
	{

		GameObject navigationSEPrefab = Resources.Load (Constants.AudioFileDir + "UINavSE") as GameObject;
		GameObject navigationSE = GameObject.Instantiate (navigationSEPrefab)	as GameObject;

		txt.text = GameStatus.UserDataCollection [inputManager.playerNum].Username;
		float PosXoffset = GetTextPositionOffset (position);
		txt.rectTransform.localPosition = new Vector3 (PosXoffset, 
		                                               txt.rectTransform.localPosition.y,
		                                               txt.rectTransform.localPosition.z);
	}
	void Back()
	{
		if (Confirmed && !TeamSelectionControl.Instance.GameStarting) 
		{
			Confirmed = false;
			disableReady ();
			// clean team count
			TeamSelectionControl.Instance.leaveTeam(GameStatus.UserDataCollection [inputManager.playerNum].teamID);
			// reset team id
			GameStatus.UserDataCollection [inputManager.playerNum].teamID = -1;
		}


	}
	void Confirm()
	{
		if (TeamSelectionControl.Instance.CanStart && !TeamSelectionControl.Instance.GameStarting ) {
			Debug.Log("Starting");
			TeamSelectionControl.Instance.StartGame();
		}
		if(txt.enabled == false) 
		{
			GameObject confirmSEPrefab = Resources.Load (Constants.AudioFileDir + "UIConSE") as GameObject;
			GameObject confirmSE = GameObject.Instantiate (confirmSEPrefab)	as GameObject;
			TeamSelectionControl.PlayerEntered();
			txt.enabled = true;
			return;
		}
		if (position == Position.Middle)
			return;
		if(!Confirmed)
		{
			int teamID = HashPosToTeamID(position);
			// check if the team spilt is correct
			// bool status = TeamSelectionControl.ConfirmedAndCount(teamID);

			if(TeamSelectionControl.Instance.checkTeam(teamID))
			{
				// confirm to join the team
				Confirmed = true;
				// create a confirm effect
				// txt.color = Color.red;
				IndicateReady();
				// hash currentUIPos to 0 and 1
				GameStatus.UserDataCollection [inputManager.playerNum].teamID = teamID;
				TeamSelectionControl.Instance.joinTeam(teamID);
			}
			else
			{
				// error indication 
				StartCoroutine(IndicateError ());
			}
		}
	}

	private int HashPosToTeamID(Position position_) {
		switch (position_) {
		case Position.Left:
			return 0;
		case Position.Right:
			return 1;
		case Position.Middle:
			Debug.LogError("UI: you shouldn't hash Middle to team ID");
			break;
		}
		return -1;

	}
	private float GetTextPositionOffset(Position position_) {
		switch (position_) {
		case Position.Left:
			return txtPos[0];
		case Position.Right:
			return txtPos[2];
		case Position.Middle:
			return txtPos[1];
		}
		return -1.0f;
		
	}

	IEnumerator IndicateError()
	{
		Color originalColor = txt.color;
		for(int i = 0; i < 5; ++i)
		{
			yield return new WaitForSeconds(0.2f);
			txt.color = i % 2 == 0 ? Color.red : originalColor;
		}
		txt.color = originalColor;
	}

	void IndicateReady() {

		GameObject confirmSEPrefab = Resources.Load (Constants.AudioFileDir + "UIConSE") as GameObject;
		GameObject confirmSE = GameObject.Instantiate (confirmSEPrefab)	as GameObject;

		ReadyEffect.SetActive (true);
	}

	void disableReady() {


			GameObject rejectSEPrefab = Resources.Load (Constants.AudioFileDir + "UIRejSE") as GameObject;
			GameObject rejectSE = GameObject.Instantiate (rejectSEPrefab) as GameObject;
			
			ReadyEffect.SetActive (false);

	}


}
