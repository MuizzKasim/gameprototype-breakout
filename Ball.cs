using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	//Declaring variables
	public float SpeedUp = 0.5f;

	Rigidbody rb;

	//Initialization
	void Start () {
		rb = this.GetComponent<Rigidbody> ();
	}

	//Adds force into ball every time it collides
	void OnCollisionEnter(Collision hit){

		float ballZPosition = this.transform.position.z;
		float objectZPosition = hit.gameObject.transform.position.z;

		if (hit.gameObject.name == "World Bounds: North" || hit.gameObject.name == "World Bounds: East" || hit.gameObject.name == "World Bounds: West" || hit.gameObject.name == "Platform") {
			if (objectZPosition > ballZPosition) {
				rb.AddForce (new Vector3 ((-rb.velocity.x - objectZPosition) * SpeedUp, 0, -rb.velocity.z), ForceMode.Acceleration);

			} else if (objectZPosition < ballZPosition) {
				rb.AddForce (new Vector3 ((rb.velocity.x + objectZPosition) * SpeedUp, 0, rb.velocity.z), ForceMode.Acceleration);
			}
		} 

	}
}
