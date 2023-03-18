using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour {

	//Declaring all variables
	public GameObject deathZone;
	public GameManager gm;

	//Upon Colliding with the DeathZone, trigger this function
	void OnTriggerEnter (Collider col){
		Debug.Log ("Ball died.");

		//Calls GameManager LoseLife() function
		gm.LoseLife ();
	}
}
