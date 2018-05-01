using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour {

	SpriteRenderer mySpriteRenderer;
	Rigidbody2D myRigidbody;

	public GameObject healthText;
	public GameObject scoreText;

	public int lives;
	public int score;

	public float moveSpeed = 1.0f;

	public GameObject beam;
	public int magneticCharge; //1 = positive, -1 = negative
	public float beamStr;

	Vector3 cursorPosition;

	public Sprite idle;
	public Sprite shootPositive;
	public Sprite shootNegative;

	void Awake() {
		mySpriteRenderer = GetComponent<SpriteRenderer> ();
		myRigidbody = GetComponent<Rigidbody2D> ();
		lives = 3;
		healthText.GetComponent<Text> ().text = "Health: "+ lives;
	}

	void Update () {
		CursorPosition ();
		Move ();
		LookAtCursor ();
		Shoot ();
		UpdateScoreText ();
	}

	void Move(){
		float verticalVelocity = Input.GetAxis ("Vertical");
		float horizontalVelocity = Input.GetAxis ("Horizontal");
		Vector2 velocity = new Vector2 (horizontalVelocity, verticalVelocity);

		myRigidbody.velocity += velocity * moveSpeed * Time.deltaTime;
	}

	void CursorPosition(){
		cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		cursorPosition.z = 0;
	}

	void LookAtCursor(){
		transform.up = cursorPosition - transform.position;
	}
	void Shoot(){
		if (Input.GetMouseButton (0)) {
			mySpriteRenderer.sprite = shootPositive;
			beam.SetActive (true);
			beam.GetComponent<SpriteRenderer> ().color = new Color (0, 0, 1, 0.2f);
			magneticCharge = 1;
		} else if (Input.GetMouseButton (1)) {
			mySpriteRenderer.sprite = shootNegative;
			beam.SetActive (true);
			beam.GetComponent<SpriteRenderer> ().color = new Color (1, 0, 0, 0.2f);
			magneticCharge = -1;
		} else {
			mySpriteRenderer.sprite = idle;
			beam.SetActive (false);
			magneticCharge = 0;
		}
	}

	void OnCollisionEnter2D (Collision2D collisionInfo){
		if (collisionInfo.gameObject.tag == "Enemy") {
			Destroy (collisionInfo.gameObject);
			lives -= 1;
			healthText.GetComponent<Text> ().text = "Health: " + lives;
			if (lives <= 0) {
				Death ();
			}
		}
	}

	void Death(){
	}

	void UpdateScoreText(){
		scoreText.GetComponent<Text> ().text = "Score: " + score;
	}
}
