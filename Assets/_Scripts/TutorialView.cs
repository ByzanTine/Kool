﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using InControl;

public class TutorialView : MonoBehaviour {
	public Text Text;
	public Text Text2;
	private int NumOfCurStep;
	private Text _txt;
	private Text _txt2;
	public GameObject[] itemGenerators;
	public int roundToRemoveWalls;
	public Sprite CurButton;
	static public bool[] StopBlinking = {false, false, false, false};
//	static public bool[] StopBlinking = {false, true, true, true};
	public bool tempBool = false;
	float time;

	[System.Serializable]
	public class tutorialStep{
		public string txt;
		public Sprite button;
		public GameObject item;
		public float LeastTimeInterval = 5;
		public UserInputManager.InputSource inputSource;
 	}

	public tutorialStep[] TutorialSteps;
	public GameObject[] itemPos;
	public GameObject[] walls;
	GameObject[] playerCollection;
	private UserInputManager.InputSource curInstructionInput;

	// Use this for initialization
	void Awake(){
		TutorialSteps [0].txt = "Shooting";

		TutorialSteps [1].txt = "Aiming";

		TutorialSteps [2].txt = "Moving";
		
		TutorialSteps [3].txt = "Holding left trigger to run";
		
		TutorialSteps [4].txt = "While running, press right trigger, you gonna cast a melee attack.";
		
		TutorialSteps [5].txt = "Switching between Ice and fire.";
		
		TutorialSteps [6].txt = "Pick up the shiny mega spell box.";
		
		TutorialSteps [7].txt = "15 seconds to start the real fight!";
	}

	void Start () {
		time = Time.time;
		NumOfCurStep = 0;
		_txt = Text.GetComponent<Text> ();
		_txt.text = TutorialSteps[NumOfCurStep].txt;
		_txt2 = Text2.GetComponent<Text> ();
		_txt2.text = TutorialSteps[NumOfCurStep].txt;
		curInstructionInput = TutorialSteps [NumOfCurStep].inputSource;
		for (int i = 0; i < itemGenerators.Length; i++) {
			itemGenerators[i].SetActive(false);
		}
		CurButton = TutorialSteps [NumOfCurStep].button;
		playerCollection = GameObject.FindGameObjectsWithTag (TagList.Player);
		foreach (GameObject player in playerCollection){
			UserInputManager Uinput = player.GetComponent<UserInputManager>();
			Uinput.LockControl(UserInputManager.InputSource.AllControl);
			Uinput.UnlockControl(curInstructionInput);
		}
	} 
	
	// Update is called once per frame
	void Update () {
		if (checkUserFollowInstruction()  &&  time + TutorialSteps[NumOfCurStep].LeastTimeInterval < Time.time) {
			time = Time.time;
			SetStopBlinkingFalse();
			NumOfCurStep = NumOfCurStep + 1;

			if (NumOfCurStep == TutorialSteps.Length) {
				Debug.Log("End of tutorial and switch to main map!!");
				Application.LoadLevel("MainMap");
				return;
			}

			CurButton = TutorialSteps [NumOfCurStep].button;
			_txt.text = TutorialSteps[NumOfCurStep].txt;
			_txt2.text = TutorialSteps[NumOfCurStep].txt;
			curInstructionInput = TutorialSteps [NumOfCurStep].inputSource;
			unlockCtrl(curInstructionInput);
			if (TutorialSteps[NumOfCurStep].item){
				for (int j = 0; j < itemPos.Length; j ++){
					Instantiate(TutorialSteps[NumOfCurStep].item, itemPos[j].transform.position, Quaternion.identity);
				}
			}

			if (NumOfCurStep == TutorialSteps.Length - 1) {
				for (int i = 0; i < itemGenerators.Length; i++) {
					itemGenerators[i].SetActive(true);
				}
			}

			if (NumOfCurStep == roundToRemoveWalls) {
				for (int i = 0; i < walls.Length; i++) {
					walls[i].SetActive(false);
				}
			}
		}
	}

	bool checkUserFollowInstruction(){
		playerCollection = GameObject.FindGameObjectsWithTag (TagList.Player);
		if (curInstructionInput == UserInputManager.InputSource.None)
			return true;
		bool returnV = true;
		for (int i = 0; i < 4; i ++) {
			if (!StopBlinking[i]){
				UserInputManager Uinput = playerCollection[i].GetComponent<UserInputManager>();
				if(curInstructionInput == UserInputManager.InputSource.RBumper) {
					if (playerCollection[i].GetComponent<PlayerData>().SpecialSpellID != SpellDB.AttackID.None){
						StopBlinking[i] = Uinput.CheckInputControl(curInstructionInput);
					} 
				} else {
					StopBlinking[i] = Uinput.CheckInputControl(curInstructionInput);
				}
			}
			returnV = returnV & StopBlinking[i];
		}
		return returnV;
	}



	void SetStopBlinkingFalse(){
		for (int i = 0; i < 4; i ++)
			StopBlinking[i] = false;
//		StopBlinking[0] = false; // for debug
	}

	void unlockCtrl(UserInputManager.InputSource ctrl){
		foreach (GameObject player in playerCollection){
			UserInputManager Uinput = player.GetComponent<UserInputManager>();
			Uinput.UnlockControl(ctrl);
		}
	}


}
