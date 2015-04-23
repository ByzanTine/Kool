using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FetchStatistic : MonoBehaviour {

	Text txt;
	public GameObject TextObjPrefab;

	// the horizontal position of two column
	// the vertical position is depended on the font size
	int[] UIposX = new int[2] {-40, 160};

	float popUpSpeed = 1.5f;
	// Use this for initialization
	void Start () {
		txt = GetComponent<Text>();
		StartCoroutine( FetchStatistics ());
	}

	IEnumerator FetchStatistics()
	{
		GameStatistic.Instance.GetRates ();
		txt.text = "scores\n";
		for(int i = 0; i < 2; ++i)
		{
			GameObject textObj = Instantiate(TextObjPrefab) as GameObject;
			textObj.transform.parent = this.gameObject.transform;
			Text unitTxt = textObj.GetComponent<Text>();
			unitTxt.transform.localPosition = new Vector3(UIposX[i], txt.fontSize * 2.0f);
			unitTxt.text = GameStatistic.Instance.Scores[i].ToString();
			yield return new WaitForSeconds(popUpSpeed);
		}

		txt.text += "attacks\n";
		for(int i = 0; i < 2; ++i)
		{
			GameObject textObj = Instantiate(TextObjPrefab) as GameObject;
			textObj.transform.parent = this.gameObject.transform;
			Text unitTxt = textObj.GetComponent<Text>();
			unitTxt.transform.localPosition = new Vector3(UIposX[i], txt.fontSize * 1.0f);
			unitTxt.text = GameStatistic.Instance.Attacks[i].ToString();
			yield return new WaitForSeconds(popUpSpeed);

		}

		txt.text += "ult used\n";
		for(int i = 0; i < 2; ++i)
		{
			GameObject textObj = Instantiate(TextObjPrefab) as GameObject;
			textObj.transform.parent = this.gameObject.transform;
			Text unitTxt = textObj.GetComponent<Text>();
			unitTxt.transform.localPosition = new Vector3(UIposX[i], txt.fontSize * 0.0f);
			unitTxt.text = GameStatistic.Instance.Ults[i].ToString();
			yield return new WaitForSeconds(popUpSpeed);

		}

		txt.text += "rate\n";
		for(int i = 0; i < 2; ++i)
		{
			GameObject textObj = Instantiate(TextObjPrefab) as GameObject;
			textObj.transform.parent = this.gameObject.transform;
			Text unitTxt = textObj.GetComponent<Text>();
			unitTxt.transform.localPosition = new Vector3(UIposX[i], txt.fontSize * -1.0f);
			unitTxt.text = GameStatistic.Instance.Rates[i];
			yield return new WaitForSeconds(popUpSpeed);

		}
	}

}
