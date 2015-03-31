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
		}

		if (NumOfCurStep == TutorialSteps.Length) {
			Debug.Log("End of tutorial and switch to main map!!");

		}
	}
}
