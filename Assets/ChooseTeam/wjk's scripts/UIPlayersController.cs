using UnityEngine;
using System.Collections;

public class UIPlayersController : MonoBehaviour {
	public Transform leftUp;
	public Transform leftDown;
	public Transform rightUp;
	public Transform rightDown;


	bool left1 = false;
	bool left2 = false;
	bool right1 = false;
	bool right2 = false;

	public GameObject player1;
	public GameObject player2;	
	public GameObject player3;
	public GameObject player4;



	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.A)) {
			if(Input.GetKey(KeyCode.Alpha1)){
				moveLeft(player1);
			}
			if(Input.GetKey(KeyCode.Alpha2)){
				moveLeft(player2);
			}
			if(Input.GetKey(KeyCode.Alpha3)){
				moveLeft(player3);
			}
			if(Input.GetKey(KeyCode.Alpha4)){
				moveLeft(player4);
			}

		}
		if (Input.GetKeyDown (KeyCode.D)) {
			if(Input.GetKey(KeyCode.Alpha1)){
				moveRight(player1);
			}
			if(Input.GetKey(KeyCode.Alpha2)){
				moveRight(player2);
			}
			if(Input.GetKey(KeyCode.Alpha3)){
				moveRight(player3);
			}
			if(Input.GetKey(KeyCode.Alpha4)){
				moveRight(player4);
			}
		}
	}
	void moveLeft(GameObject p){
		if (p.GetComponent<PlayerTag> ().pos == "CENTER") {
			if (left1 == false) {
				left1 = true;
				p.GetComponent<PlayerTag>().pos = "LEFT";
				p.GetComponent<PlayerTag>().posId = 1;
				p.GetComponent<PlayerTag> ().moveTo (leftUp.position); 
			} else if(!left2 ) {
				left2 = true;
				p.GetComponent<PlayerTag>().pos = "LEFT";
				p.GetComponent<PlayerTag>().posId = 2;
				p.GetComponent<PlayerTag> ().moveTo (leftDown.position); 
			}
			return;
		} 
		if (p.GetComponent<PlayerTag> ().pos == "RIGHT") {
			if(p.GetComponent<PlayerTag>().posId == 1){
				right1 = false;
			} else {
				right2 = false;
			}
			p.GetComponent<PlayerTag> ().moveTo (p.GetComponent<PlayerTag> ().initPos); 
			p.GetComponent<PlayerTag>().pos = "CENTER";
		}
	}
	void moveRight(GameObject p){
		if (p.GetComponent<PlayerTag> ().pos == "CENTER") {
			if (right1 == false) {
				right1 = true;
				p.GetComponent<PlayerTag>().pos = "RIGHT";
				p.GetComponent<PlayerTag>().posId = 1;

				p.GetComponent<PlayerTag> ().moveTo (rightUp.position); 
			} else if(!right2 ) {
				right2 = true;
				p.GetComponent<PlayerTag>().pos = "RIGHT";
				p.GetComponent<PlayerTag>().posId = 2;

				p.GetComponent<PlayerTag> ().moveTo (rightDown.position); 
			}
			return;
		} 
		if (p.GetComponent<PlayerTag> ().pos == "LEFT") {
			if(p.GetComponent<PlayerTag>().posId == 1){
				left1 = false;
			} else {
				left2 = false;
			}
			p.GetComponent<PlayerTag> ().moveTo (p.GetComponent<PlayerTag> ().initPos); 
			p.GetComponent<PlayerTag>().pos = "CENTER";
		}
	}
}
