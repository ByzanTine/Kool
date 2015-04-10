using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TutorialView : MonoBehaviour {
	public Text Text;
	private float time;
	private int NumOfCurStep;
	private Text _txt;
	public GameObject[] itemGenerators;
	public int roundToRemoveWalls;
	public Sprite CurButton;

	[System.Serializable]
	public class tutorialStep{
		public string txt;
		public Sprite button;
		public float timeInterval;
		public GameObject item;
 	}

	public tutorialStep[] TutorialSteps;
	public GameObject[] itemPos;
	public GameObject[] walls;
	// Use this for initialization
	void Awake(){
		TutorialSteps [0].txt = "Shooting";

		TutorialSteps [1].txt = "Aiming";

		TutorialSteps [2].txt = "Moving";
		
		TutorialSteps [3].txt = "Running";
		
		TutorialSteps [4].txt = "While running, press right trigger, you gonna cast a melee attack.";
		
		TutorialSteps [5].txt = "Switching between Ice and fire.";
		
		TutorialSteps [6].txt = "Pick up the shiny mega spell box.";
		
		TutorialSteps [7].txt = "15 seconds to start the real fight!";
	}

	void Start () {
		time = 0;
		NumOfCurStep = 0;
		_txt = Text.GetComponent<Text> ();
		_txt.text = TutorialSteps[NumOfCurStep].txt;
		for (int i = 0; i < itemGenerators.Length; i++) {
			itemGenerators[i].SetActive(false);
		}
		CurButton = TutorialSteps [NumOfCurStep].button;
		GameObject[] playerCollection = GameObject.FindGameObjectsWithTag (TagList.Player);
		foreach (GameObject player in playerCollection) {
			UserInputManager Uinput = player.GetComponent<UserInputManager>();
			float time = 0;
			foreach (tutorialStep step in TutorialSteps){
				time += step.timeInterval;
				StartCoroutine(Uinput.lock(2.0F));
			}
		}
	} 
	
	// Update is called once per frame
	void Update () {
		
		
		if (NumOfCurStep == TutorialSteps.Length) {
			Debug.Log("End of tutorial and switch to main map!!");
			Application.LoadLevel("ChooseTeam");
		}

		if (Time.time - time > TutorialSteps [NumOfCurStep].timeInterval) {
			NumOfCurStep = NumOfCurStep + 1;
			CurButton = TutorialSteps [NumOfCurStep].button;
			_txt.text = TutorialSteps[NumOfCurStep].txt;
			time = Time.time;
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
}
