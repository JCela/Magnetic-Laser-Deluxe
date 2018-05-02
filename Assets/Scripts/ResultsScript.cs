using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultsScript : MonoBehaviour {

	public GameObject highScoreText;
	public GameObject yourScoreText;
	public GameObject wavesText;

	EventScript eventScript;

	// Use this for initialization
	void Start () {
		eventScript = GameObject.Find ("EventManager").GetComponent<EventScript> ();
		yourScoreText.GetComponent<Text> ().text = "Your Score: " + eventScript.score;
		wavesText.GetComponent<Text> ().text = "You survived " + (eventScript.round - 2).ToString() + " waves";
	}

	void Update(){
		if (Input.GetKey (KeyCode.Space)) {
			SceneManager.LoadScene ("Main");
		}
	}
}
