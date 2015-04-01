using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialView : MonoBehaviour {
	public GameObject Img;
	public GameObject Text;
	private float time;
	private int NumOfCurStep;
	private Image _img;
	private Text _txt;
	public GameObject[] itemGenerators;

	[System.Serializable]
	public class tutorialStep{
		public string txt;
		public Sprite img;
		public float timeInterval;
		public GameObject item;
 	}

	public tutorialStep[] TutorialSteps;
	public GameObject[] itemPos;
	// Use this for initialization
	void Awake(){
		TutorialSteps [0].txt = "Two joysticks for moving and aiming";
		
		TutorialSteps [1].txt = "Cast magic balls according to your aiming direction.";
		
		TutorialSteps [2].txt = "Run:  It is much harder to control. ";
		
		TutorialSteps [3].txt = "While running, press right trigger, you gonna cast a melee attack.";
		
		TutorialSteps [4].txt = "Switch between fire mode and ice mode";
		
		TutorialSteps [5].txt = "Pick up the shiny special spell box. Try it out.";
		
		TutorialSteps [6].txt = "Special Spell's mode can also be changed by switch button.";
		
		TutorialSteps [7].txt = "30 seconds to start the real fight!";
	}

	void Start () {
		time = 0;
		NumOfCurStep = 0;
		_img = Img.GetComponent<Image>();
		_txt = Text.GetComponent<Text> ();
		_img.sprite = TutorialSteps[NumOfCurStep].img;
		_txt.text = TutorialSteps[NumOfCurStep].txt;
		for (int i = 0; i < itemGenerators.Length; i++) {
			itemGenerators[i].SetActive(false);
		}

	} 
	
	// Update is called once per frame
	void Update () {
		if (Time.time - time > TutorialSteps [NumOfCurStep].timeInterval) {
			NumOfCurStep = NumOfCurStep + 1;
			_img.sprite = TutorialSteps[NumOfCurStep].img;
			_txt.text = TutorialSteps[NumOfCurStep].txt;
			time = Time.time;
			if (TutorialSteps[NumOfCurStep].item){
				for (int j = 0; j < itemPos.Length; j ++){
					Instantiate(TutorialSteps[NumOfCurStep].item, itemPos[j].transform.position, Quaternion.identity);
				}
			}

			if (NumOfCurStep == TutorialSteps.Length - 1) {
				Debug.Log("free to play");
				for (int i = 0; i < itemGenerators.Length; i++) {
					itemGenerators[i].SetActive(true);
				}
			}
		}

		if (NumOfCurStep == TutorialSteps.Length) {
			Debug.Log("End of tutorial and switch to main map!!");

		}
	}
}
