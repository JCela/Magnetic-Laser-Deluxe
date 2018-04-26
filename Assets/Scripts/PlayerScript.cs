using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

	Rigidbody2D myRigidbody;
	public float moveSpeed = 1.0f;


	void Awake() {
		myRigidbody = GetComponent<Rigidbody2D> ();
	}

	void Start () {
		
	}

	void Update () {
		float verticalVelocity = Input.GetAxis ("Vertical");
		float horizontalVelocity = Input.GetAxis ("Horizontal");
		Vector2 velocity = new Vector2 (horizontalVelocity, verticalVelocity);

		myRigidbody.velocity += velocity * moveSpeed;
	}
}
