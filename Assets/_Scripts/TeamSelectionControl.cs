using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TeamSelectionControl : MonoBehaviour {

	private static TeamSelectionControl _instance;
	
	//This is the public reference that other classes will use
	public static TeamSelectionControl Instance
	{
		get
		{
			//If _instance hasn't been set yet, we grab it from the scene!
			//This will only happen the first time this reference is used.
			return _instance;
		}
	}
	// the final game player number of each team
	// -1 means 1v1, 2v2 are both OK;
	public int TeamSize = 1;
	public bool CanStart = false;
	public GameObject StartHint;
	private const float InitPrepareTime = 9999f;

	private const float MaxPrepareTime = 3f;
	private static float prepareTime;
	private static int enteredNum = 0;

	private static int[] teamNumCount = new int[2] {0, 0};
	private Text txt;
	// Use this for initialization
	void Awake() {
		_instance = this;
	}
	void Start () {

		txt = GetComponent<Text> ();
		txt.enabled = true;
		StartCoroutine(WaitToStart ());
	}

	public static void PlayerEntered()
	{
		prepareTime = InitPrepareTime;
		enteredNum++;
	}
	/// <summary>
	/// Checks if the team is full.
	/// </summary>
	/// <returns><c>true</c>, if team was not full, <c>false</c> otherwise.</returns>
	/// <param name="teamId">Team identifier.</param>
	public bool checkTeam(int teamId) {
		if (teamNumCount[teamId] < TeamSize) {
			return true;
		}
		else {
			return false;
		}
	}
	// check before you join
	// no error check
	public void joinTeam(int teamId) {
		teamNumCount [teamId]++;
	}
	// no error check 
	public void leaveTeam(int teamId) {
		teamNumCount [teamId]--;
	}
	// check team balancing 
	void Update() {
		if (teamNumCount[0] != teamNumCount[1]) {
			// unbalanced 
			StartHint.SetActive(false);
			CanStart = false;
			return;
		}
		if (teamNumCount [0] != TeamSize || teamNumCount [1] != TeamSize) {
			StartHint.SetActive(false);
			CanStart = false;
			return;
		}
		StartCoroutine (SetCanStart ());
	}
	private IEnumerator SetCanStart() {
		// hint
		if (StartHint) {
			StartHint.SetActive(true);
		}
		yield return new WaitForSeconds (2.0f);
		CanStart = true;

	}
	public void StartGame(){
		// check status of the game
		if (teamNumCount[0] != teamNumCount[1]) {
			// unbalanced 
			Debug.LogError("TeamSelection failed, team not balanced");
			return;
		}
		if (teamNumCount [0] != TeamSize || teamNumCount [1] != TeamSize) {
			Debug.LogError("TeamSelection failed, team not full");
			return;
		}

		prepareTime = MaxPrepareTime;

		// lock all UI and lanuch the game
	}
	//
//	public static bool ConfirmedAndCount(int teamId)
//	{
//		// check if some team have too much people
//		if(TeamSize != -1 && teamNumCount[teamId] > TeamSize)
//		{
//			return false;
//		}
//
//		teamNumCount [teamId]++;
//
//		// the last player, one more check to make sure team are balanced
//		if(enteredNum == 1)
//		{
//			if(teamNumCount[0] != teamNumCount[1])
//			{
//				teamNumCount [teamId]--;
//				return false;
//			}
//		}
//
//		enteredNum--;
//		if(enteredNum == 0)
//		{
//			prepareTime = MaxPrepareTime;
//		}
//		return true;
//	}

	IEnumerator WaitToStart()
	{
		prepareTime = InitPrepareTime;
		while(prepareTime > 0)
		{
			yield return new WaitForSeconds(1.0f);
			prepareTime--;
			txt.text = (prepareTime % 100).ToString();
		}
		txt.text = "Start!";

		Application.LoadLevel ("GameMap");
	}
}
