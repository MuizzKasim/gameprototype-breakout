using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour {

	//Declaring all float
	public float velocity =23f;
	public float platformMin = -20;
	public float platformMax = 20;

	public GameObject Player1;

	//Declaring all boolean
	bool moveLeft = true;
	bool moveRight= true;

	Rigidbody P1;

	//Initialization
	void Start () {
		P1 = Player1.GetComponent<Rigidbody> ();
	}
	

	void Update () {
		//Platform is controlled using Right and Left Arrow keys
		if (Input.GetKey (KeyCode.LeftArrow) && moveLeft == true) {
			P1.velocity = new Vector3 (-velocity, 0f, 0f);
		} else if (Input.GetKey (KeyCode.RightArrow) && moveRight == true) {
			P1.velocity = new Vector3 (velocity, 0f, 0f);
		} else {
			P1.velocity = new Vector3 (0f, 0f, 0f);
		}

		//Prevents player from colliding with WorldBounds:East/Right
		if(Player1.transform.position.x >= platformMax){
			moveRight = false;
		}else{
			moveRight = true;
		}

		//Prevents player from colliding with WorldBounds:West/Left
		if(Player1.transform.position.x <= platformMin){
			moveLeft = false;
		}else{
			moveLeft = true;
		}

	}
}
