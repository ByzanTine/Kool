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
	void Start () {
		time = 0;
		NumOfCurStep = 0;
		_img = Img.GetComponent<Image>();
		_txt = Text.GetComponent<Text> ();
		_img.sprite = TutorialSteps[NumOfCurStep].img;
		_txt.text = TutorialSteps[NumOfCurStep].txt;

		TutorialSteps [0].txt = "Two joysticks for moving and aiming";

		TutorialSteps [1].txt = "Right Trigger to Shoot magic balls according to " +
			"your aiming direction. The magic balls around you indicate the number " +
			"and size of magic ball you gonna shoot";

		TutorialSteps [2].txt = "Run:  It is much harder to control. " +
			"Cannot shoot and run at the same time.";

		TutorialSteps [3].txt = "After you pick a special spell box with light, " +
			"there will be a special spell icon above your name.  Try it out.";

		TutorialSteps [4].txt = "Switch between fire mode and ice mode";

		TutorialSteps [5].txt = "Special Spell's mode can also be changed by switch button.";

		TutorialSteps [6].txt = "While running, press Attack, you gonna hit instead of shoot.";

		TutorialSteps [7].txt = "Powerup boxes gonna change your status and " +
			"indicator around you will show the status";

		TutorialSteps [8].txt = "Special Spell boxes are shining!!!";

		TutorialSteps [9].txt = "30 seconds to start the real fight!";
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
