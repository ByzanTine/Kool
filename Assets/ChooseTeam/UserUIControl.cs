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

	private Position position = Position.Middle;

	private void MoveRight() {
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
	}
	private void MoveLeft() {
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

	void NavLeft()
	{
		// if confirmed, don't move then
		if(Confirmed) return;
		// if(currentUIPos > 0) currentUIPos--;
		MoveLeft ();
		SetPosition ();
	}

	void NavRight()
	{
		// if confirmed, don't move then
		if(Confirmed) return;
		// if(currentUIPos < 2) currentUIPos++;
		MoveRight ();
		SetPosition ();
	}

	void SetPosition()
	{
		txt.text = GameStatus.UserDataCollection [inputManager.playerNum].Username;
		float PosXoffset = GetTextPositionOffset (position);
		txt.rectTransform.localPosition = new Vector3 (PosXoffset, 
		                                               txt.rectTransform.localPosition.y,
		                                               txt.rectTransform.localPosition.z);
	}
	void Back()
	{
		if (Confirmed) {
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
		if (TeamSelectionControl.Instance.CanStart) {
			TeamSelectionControl.Instance.StartGame();
		}
		if(txt.enabled == false) 
		{
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
			break;
		case Position.Right:
			return 1;
			break;
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
			break;
		case Position.Right:
			return txtPos[2];
			break;
		case Position.Middle:
			return txtPos[1];
			break;
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
		ReadyEffect.SetActive (true);
	}

	void disableReady() {
		ReadyEffect.SetActive (false);
	}


}
