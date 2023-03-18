using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour {

	//Declaring variables
	public ParticleSystem CubeParticle;
	public GameManager gm;
		
	//On Collision with other game object (i.e. ball), run this function
	void OnCollisionEnter (Collision other){

		//Plays the CubeParticle
		Instantiate (CubeParticle, transform.position, Quaternion.identity);
		CubeParticle.Play ();

		//Depending on which cube color got hit, calls GameManager's DestroyCube(string type) function
		if (gameObject.CompareTag ("RedCubes")) {
			gm.DestroyCube ("Red");
		} else if (gameObject.CompareTag ("YellowCubes")) {
			gm.DestroyCube ("Yellow");
		}
		
		//Removes the collided cube
		Destroy (gameObject);
	}	

}
