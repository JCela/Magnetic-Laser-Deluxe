using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

	Rigidbody2D myRigidbody;
	SpriteRenderer mySpriteRenderer;

	GameObject player;
	PlayerScript playerScript;

	public float moveSpeed;
	public int magneticCharge;
	float distanceFromPlayer;

	public Sprite positiveSprite;
	public Sprite negativeSprite;

	void Awake(){
		myRigidbody = GetComponent<Rigidbody2D> ();
		mySpriteRenderer = GetComponent<SpriteRenderer> ();

		player = GameObject.Find ("Player");
		playerScript = player.GetComponent<PlayerScript> ();
	}

	void Start(){
		if (magneticCharge == 1) {
			//mySpriteRenderer.color = new Color (0, 0, 1, 1);
			mySpriteRenderer.sprite = positiveSprite;
		} else if (magneticCharge == -1) {
			//mySpriteRenderer.color = new Color (1, 0, 0, 1);
			mySpriteRenderer.sprite = positiveSprite;
		} else {
			mySpriteRenderer.color = new Color (1, 1, 1, 1);
		}
	}

	void Update () {
		LookAtPlayer ();
		Move ();
		DistanceFromPlayer ();
	}

	void LookAtPlayer(){
		transform.up = player.transform.position - transform.position;
	}

	void DistanceFromPlayer(){
		distanceFromPlayer = Vector3.Distance (transform.position, player.transform.position);	
	}

	void Move(){
		myRigidbody.AddRelativeForce(new Vector2 (0f, 1f) * moveSpeed);
	}

	void OnTriggerStay2D(Collider2D collisionInfo){
		if (collisionInfo.gameObject.tag == "Beam") {
			if (magneticCharge == playerScript.magneticCharge) {
				//if same charge, repel
				myRigidbody.AddRelativeForce (new Vector2 (0f,playerScript.beamStr * -100) * Time.deltaTime / (distanceFromPlayer/3));
			} else if (magneticCharge == playerScript.magneticCharge * -1) {
				//if opposite charge, attract
				myRigidbody.AddRelativeForce (new Vector2 (0f,playerScript.beamStr * 25) * Time.deltaTime / (distanceFromPlayer/3));
			}
		}
		if (collisionInfo.gameObject.tag == "Laser") {
			if (collisionInfo.gameObject.name == "LaserLeft") {
				if (magneticCharge == 1) {
					playerScript.score += 1;
				}
			}
			if (collisionInfo.gameObject.name == "LaserRight") {
				if (magneticCharge == -1) {
					playerScript.score += 1;
				}
			}
			Destroy (this.gameObject);
			Destroy (this);
		}
	}
}
