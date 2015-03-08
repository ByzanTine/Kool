using UnityEngine;
using System.Collections;

public class CastingAid : MonoBehaviour {
	public GameObject trajectoryPrefab;
	private UserInputManager inputManager;
	private Animator animator;

	private bool isInitiated = false;
//	private WizardAttackMeans attackMeans;
	
	void Start()
	{
		// mapping input events
		inputManager = GetComponent<UserInputManager> ();
//		inputManager.OnPressLBumper += HandleLBumper;
//		inputManager.OnPressRBumper += HandleRBumper;
//		inputManager.OnPressButton += HandleButton;
		animator = GetComponentInChildren<Animator> ();
//		attackMeans = GetComponent<WizardAttackMeans> ();
	}

	// Update is called once per frame
	void Update () {
		showTrajAtAiming ();
	}

	void showTrajAtAiming ()
	{
		if(Mathf.Abs(inputManager.rightInput.x + inputManager.rightInput.y) > 0
		   && !animator.GetBool("isCasting")
		   && !animator.GetCurrentAnimatorStateInfo(0).IsName("isCasting")
		   && !isInitiated)
		{
			GameObject trajectory = 
				Instantiate(trajectoryPrefab,this.transform.position,transform.rotation) 
					as GameObject;
			StartCoroutine(innerCoolDown());
			trajectory.transform.Translate(new Vector3(0.0f, 0.0f, trajectory.transform.localScale.y));
			trajectory.transform.Rotate(new Vector3(1.0f, 0.0f, 0.0f), 90.0f);
			trajectory.transform.parent = this.gameObject.transform;
		}
	}

	IEnumerator innerCoolDown()
	{
		isInitiated = true;
		yield return new WaitForSeconds (0.5f);
		isInitiated = false;
	}

}
