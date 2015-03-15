using UnityEngine;
using System.Collections;

public class updateAnimation : MonoBehaviour {


	private PlayerControl playControl;
	private Animator animator;
	// Use this for initialization
	void Start () {
		playControl = transform.parent.GetComponent<PlayerControl>();
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		// animator.SetBool("isCasting", playControl.isCasting);
//		animator.SetInteger ("Speed", playControl.Speed);
	}
}
