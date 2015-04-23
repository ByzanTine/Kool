using UnityEngine;
using System.Collections;

public class GameStatistic : MonoBehaviour {

	private static GameStatistic _instance;

	public int[] Scores = new int[2] {0, 0};
	public int[] Attacks = new int[2] {0, 0};
	public int[] Ults = new int[2] {0, 0};
	public string[] Rates = new string[2] {"C", "C"};

	
	//This is the public reference that other classes will use
	public static GameStatistic Instance
	{
		get
		{
			//If _instance hasn't been set yet, we grab it from the scene!
			//This will only happen the first time this reference is used.
			if(_instance == null)
				_instance = GameObject.FindObjectOfType<GameStatistic>();
			return _instance;
		}
	}
	
	void Awake() 
	{
		// Scene transition protection for singleton
		if(_instance == null)
		{
			//If I am the first instance, make me the Singleton
			_instance = this;
			DontDestroyOnLoad(this);
		}
		else
		{
			//If a Singleton already exists and you find
			//another reference in scene, destroy it!
			if(this != _instance)
				Destroy(this.gameObject);
		}
		
	}

	public void GetRates()
	{
		int grade;
		string[] AllRates = new string[4] {"C", "B", "A", "S"};
		for(int i = 0; i < 2; ++i)
		{
			grade = Scores[i] * 10 + Attacks[i] + Ults[i] * 10;
			grade = Mathf.Clamp(grade / 80, 0, 3);
			Rates[i] = AllRates[grade];
		}
	}

	public void ResetStatistics()
	{
		Scores = new int[2] {0, 0};
		Attacks = new int[2] {0, 0};
		Ults = new int[2] {0, 0};
		Rates = new string[2] {"C","C"};
	}
}
