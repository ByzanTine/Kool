using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UserUIControl : MonoBehaviour {

	// Local components
	private UserInputManager inputManager;

	private Text txt;
	private float[] txtPos = new float[3] {-150, 0, 150};
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

		GameStatus.UserDataCollection [inputManager.playerNum].teamID = -1;

	}

	void NavUp()
	{
		if(isChosen) return;
		if(currentUIPos > 0) currentUIPos--;
		SetPosition ();
	}

	void NavDown()
	{
		if(isChosen) return;
		if(currentUIPos < 2) currentUIPos++;
		SetPosition ();
	}

	void SetPosition()
	{
		txt.text = GameStatus.UserDataCollection [inputManager.playerNum].Username;
		txt.rectTransform.localPosition = new Vector3 (txtPos [currentUIPos], txt.rectTransform.localPosition.y,
		                                               txt.rectTransform.localPosition.z);
	}

	void Confirm()
	{
		if(txt.enabled == false) 
		{
			ChooseTeamStartCount.PlayerEntered();
			txt.enabled = true;
			return;
		}

		if(currentUIPos == 1)
		{
			StartCoroutine(Blink ());
		}
		else if(!isChosen)
		{
			bool status = ChooseTeamStartCount.ConfirmedAndCount(currentUIPos/2);
			if(status)
			{
				isChosen = true;
				txt.color = Color.red;
				GameStatus.UserDataCollection [inputManager.playerNum].teamID = currentUIPos/2;
			}
			else
			{
				StartCoroutine(Blink ());
			}
		}
	}

	IEnumerator Blink()
	{
		Color originalColor = txt.color;
		for(int i = 0; i < 5; ++i)
		{
			yield return new WaitForSeconds(0.2f);
			txt.color = i % 2 == 0 ? Color.red : originalColor;
		}
		txt.color = originalColor;
	}
	
	void Back()
	{
	}

}
