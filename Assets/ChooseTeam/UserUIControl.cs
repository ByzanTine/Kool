using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UserUIControl : MonoBehaviour {

	// Local components
	private UserInputManager inputManager;

	private Text txt;
	private float[] txtPos = new float[3] {0, 150, -150};
	private int currentUIPos = 0;
	// Local variables & local status
	private bool isChosen = false;
	
	void Start()
	{
		
		// mapping input events
		inputManager = GetComponent<UserInputManager> ();
		//		inputManager.OnPressHit += TryCombatAttack;
		
		inputManager.OnPressConfirm += Confirm;
		inputManager.OnPressBack += Back;
		inputManager.OnPressNavUp += NavUp;
		inputManager.OnPressNavDown += NavDown;

		txt = GetComponent<Text> ();
		txt.text = GameStatus.UserDataCollection [inputManager.playerNum].Username;
		txt.enabled = false;

		GameStatus.UserDataCollection [inputManager.playerNum].teamID = currentUIPos - 1;

	}

	void NavUp()
	{
		if(isChosen) return;
		currentUIPos++;
		SetPosition ();
	}

	void NavDown()
	{
		if(isChosen) return;
		currentUIPos += 2;
		SetPosition ();
	}

	void SetPosition()
	{
		txt.text = GameStatus.UserDataCollection [inputManager.playerNum].Username;
		txt.rectTransform.localPosition = new Vector3 (txtPos [currentUIPos % 3], txt.rectTransform.localPosition.y,
		                                               txt.rectTransform.localPosition.z);
	}

	void Confirm()
	{
		if(txt.enabled == false) 
		{
			txt.enabled = true;
			return;
		}
		currentUIPos = currentUIPos % 3;
		if(currentUIPos == 0)
		{
			txt.text = "Please Choose Team!";
		}
		else
		{
			isChosen = true;
			txt.color = Color.red;
			GameStatus.UserDataCollection [inputManager.playerNum].teamID = currentUIPos - 1;
			ChooseTeamStartCount.ConfirmedAndCount();
		}
	}
	
	void Back()
	{
		
	}

}
