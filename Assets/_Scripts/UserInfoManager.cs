using UnityEngine;
using System.Collections;

public class UserInfoManager : MonoBehaviour {

	public GameObject PlayerRootPrefab;
	public GameObject[] ModelPrefabs = new GameObject[2]; // MagicanPrefab, PriestPrefab;
	
	// Make this avaliable in inspector to intialize manually
	public string[] Usernames = new string[4];
	public Color[] UserColors = new Color[4]; 
	public Material[] UserMaterials = new Material[4];

	// Static user data, contains all the information of each user.
	// Such as Name, Death times in one game, current Wizard instance, etc.
	// Read Only Access
	private static UserData[] userDataCollection = new UserData[4];	
	public static UserData[] UserDataCollection
	{
		get
		{
			return userDataCollection;
		}
	}

	// total player number in the game. 
	// Read Only Access
	private static int totalPlayerNum = 0;
	public static int TotalPlayerNum
	{
		get
		{
			return totalPlayerNum;
		}
	}

	// Singleton instance
	private static UserInfoManager instance;	
	public static UserInfoManager Instance
	{
		get
		{
			//If _instance hasn't been set yet, we grab it from the scene!
			//This will only happen the first time this reference is used.
			if(instance == null)
				instance = GameObject.FindObjectOfType<UserInfoManager>();
			return instance;
		}
	}

	private bool isBindSucceed = false;

	// Singleton instance control
	void Awake() 
	{
		
		// Scene transition protection for singleton
		if(instance == null)
		{
			//If I am the first instance, make me the Singleton
			instance = this;
			DontDestroyOnLoad(this);
			BindAllUserData ();
		}
		else
		{
			//If a Singleton already exists and you find
			//another reference in scene, destroy it!
			if(this != instance)
				Destroy(this.gameObject);
		}
		
	}

	
	// Use this for initialization
	// Check if the scene contains wizard instances, 
	// if so, bind all user data with wizard instance,
	// will always record the initial position on the map
	// for later regeneration
	void Start () {
		
		GameObject[] playerCollection = GameObject.FindGameObjectsWithTag (TagList.Player);
		if(playerCollection.Length > 0 && playerCollection[0].GetComponent<PlayerControl>() != null)
		{
			BindAllWizardToUser();
		}
		if (CameraStartAnim.Instance) {
			CameraStartAnim.Instance.UpdateAllTarget();
		}

	}
	
	void OnLevelWasLoaded(int level) {
		
		// Avoid duplicated call by other instance
		if(this != instance) return;

		if(!isBindSucceed)
			BindAllUserData();

		GameObject[] playerCollection = GameObject.FindGameObjectsWithTag (TagList.Player);
		if(playerCollection.Length > 0 && playerCollection[0].GetComponent<PlayerControl>() != null)
		{
			BindAllWizardToUser();
		}

	}

	// bind each user with the wizards in the current scene
	void BindAllWizardToUser()
	{
		Debug.Log ("binding wizard to user");
		int id = 0;
		totalPlayerNum = 0;
		int[] materialIdCount = new int[2] {0, 2};
		GameObject[] playerCollection = GameObject.FindGameObjectsWithTag (TagList.Player);
		foreach(GameObject player in playerCollection)
		{
			// bind input manager
			UserInputManager userCtrl = player.GetComponent<UserInputManager>();
			// assign player ID
			userCtrl.playerNum = id;
			player.name = userDataCollection[id].Username;
			userDataCollection[id].initPosition = player.transform.position;
			if(userDataCollection[id].wizardInstance != null)
			{
				Debug.LogError ("Already bind to another player");
				continue;
			}
			
			totalPlayerNum++;
			
			userDataCollection[id].wizardInstance = player;
			
			Destroy(player);
			
			Debug.Log ("binding wizard" + id + " to user");
			
			// seperate team for default/unassigned player:
			// default: 0 & 1 in team 0, 2 & 3 in team 1;
			// unassigned: destroy the corresponding player object;
			if(userDataCollection[id].teamID >= 2)
			{
				// default: 0 & 1 in team 0, 2 & 3 in team 1;
				userDataCollection[id].teamID = id / 2;
				
				InstantiateWizardInstanceWithId(id);
			}
			else if(userDataCollection[id].teamID == -1)
			{
				totalPlayerNum--;
			}
			else // already got a valid team id
			{
				InstantiateWizardInstanceWithId(id);
			}
			
			// Distribute material to user and wizard instance by team id
			int materialId = materialIdCount[userDataCollection[id].teamID];
			materialIdCount[userDataCollection[id].teamID]++;
			userDataCollection[id].wizardMaterial = UserMaterials[materialId];
			BindWizardMaterial(userDataCollection[id].wizardInstance, userDataCollection[id].wizardMaterial);
			
			id++;
		}
	}

	// store into hashtable as well
	void BindAllUserData()
	{
		Debug.Log("[INIT]: Bind All User Data from scene setup");
		int playerID = 0;
		GameObject[] playerCollection = GameObject.FindGameObjectsWithTag (TagList.Player);

		if(playerCollection.Length == 0)
		{
			isBindSucceed = false;
			Debug.LogWarning ("Bind fail, no controller found");
			return;
		}
		isBindSucceed = true;

		foreach(GameObject player in playerCollection)
		{
			// bind input manager
			UserInputManager userCtrl = player.GetComponent<UserInputManager>();
			// assign player ID
			userCtrl.playerNum = playerID;
			userDataCollection[playerID] = new UserData();
			userDataCollection[playerID].userID = playerID;
			
			// bind the provided player info into User data.
			userDataCollection[playerID].Username = Usernames[playerID];
			userDataCollection[playerID].Usercolor = UserColors[playerID];
			userDataCollection[playerID].wizardMaterial = UserMaterials[playerID];
			playerID++;
		}
	}

	/// <summary>
	/// Instantiates the wizard instance with identifier.
	/// Will link the new instance to the corresponding user data by id.
	/// </summary>
	/// <param name="id">Identifier.</param>
	void InstantiateWizardInstanceWithId(int id)
	{
		GameObject modelPrefab = ModelPrefabs[userDataCollection [id].teamID];
		
		//		userDataCollection [id].wizardMaterial = UserMaterials [userDataCollection [id].teamID + 1];
		
		GameObject wizard = InstantiateWizardInstance (PlayerRootPrefab,
		                                               modelPrefab,
		                                               userDataCollection [id].initPosition, 
		                                               userDataCollection [id].wizardMaterial);
		// Bind control 
		UserInputManager userCtrl = wizard.GetComponent<UserInputManager>();
		userCtrl.playerNum = id;
		// add to table
		// Bind to user
		userDataCollection[id].wizardInstance = wizard;
		wizard.name = userDataCollection [id].Username;
	}
	
	/// <summary>
	/// Instantiates the wizard instance.
	/// can create either priest or magician 
	/// </summary>
	/// <returns>The wizard instance.</returns>
	/// <param name="ctrlPrefab"> Control Prefab.</param>
	/// <param name="modelPrefab"> Model Prefab.</param>
	/// <param name="Position"> Position.</param>
	/// <param name="mat"> Mat.</param>
	GameObject InstantiateWizardInstance(GameObject ctrlPrefab, GameObject modelPrefab, Vector3 Position, Material mat) 
	{
		GameObject wizard = 
			Instantiate(ctrlPrefab, Position, Quaternion.identity) 
				as GameObject;
		
		//		Destroy (playerPrefab.transform.GetChild (0));
		
		GameObject model = Instantiate(modelPrefab) as GameObject;
		model.transform.parent = wizard.transform;
		model.transform.localPosition = Vector3.zero;
		// skinn material
		BindWizardMaterial (wizard, mat);
		return wizard;
	}
	
	void BindWizardMaterial(GameObject wizard, Material mat) {
		// first get the Model
		// HACK
		Transform model = wizard.transform.GetChild (1);
		// then find model renderes
		if (model.GetComponent<Animator>() == null) {
			Debug.Log("[Model Material] the first child is not what we want: " + model.name);
		}
		Renderer[] renders = model.GetComponentsInChildren<Renderer> ();
		
		// Renderer[] renders = wizard.GetComponentsInChildren<Renderer>();
		foreach (Renderer r in renders) {
			r.material = mat;
		}
	}

	/// <summary>
	/// Destroies the player (wizard instance on the stage) with identifier.
	/// </summary>
	/// <param name="playerId">Player identifier.</param>
	public void DestroyPlayerWithId(int playerId)
	{
		if (userDataCollection[playerId].wizardInstance != null) {
			Destroy(userDataCollection[playerId].wizardInstance);
			userDataCollection[playerId].wizardInstance = null;
		}	
	}

	/// <summary>
	/// Gets the player (wizard instance on the stage) object by identifier.
	/// </summary>
	/// <returns>The player object by identifier.</returns>
	/// <param name="playerId">Player identifier.</param>
	public GameObject GetPlayerObjById(int playerId)
	{
		if (userDataCollection[playerId].wizardInstance != null)
			return userDataCollection[playerId].wizardInstance as GameObject;
		else 
			return null;
	}

	/// <summary>
	/// Gets the team mate object by identifier.
	/// </summary>
	/// <returns>The team mate object by identifier.</returns>
	/// <param name="playerId">Player identifier.</param>
	public GameObject GetTeamMateObjById(int playerId)
	{
		for(int i = 0; i < totalPlayerNum; ++i)
		{
			if(i == playerId) continue;

			if(userDataCollection[i].teamID == userDataCollection[playerId].teamID)
			{
				return userDataCollection[i].wizardInstance;
			}
		}
		return null;
	}

	/// <summary>
	/// Gets the team mate identifier by identifier.
	/// </summary>
	/// <returns>The team mate identifier by identifier.</returns>
	/// <param name="playerId">Player identifier.</param>
	public int GetTeamMateIdById(int playerId)
	{
		for(int i = 0; i < totalPlayerNum; ++i)
		{
			if(i == playerId) continue;

			if(userDataCollection[i].teamID == userDataCollection[playerId].teamID)
			{
				return i;
			}
		}
		return -1;
	}

	/// <summary>
	/// Reborns the player with identifier.
	/// </summary>
	/// <param name="id">Identifier.</param>
	public IEnumerator RebornPlayerWithId(int id)
	{
		for(int i = 4; i >= 0 ; --i)
		{
			userDataCollection[id].rebornTime = i;
			yield return new WaitForSeconds (1.0f);
		}
		userDataCollection [id].rebornTime = -1;
		InstantiateWizardInstanceWithId(id);
	}


	
}
