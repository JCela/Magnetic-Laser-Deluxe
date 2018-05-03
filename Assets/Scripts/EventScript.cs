using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EventScript : MonoBehaviour {

	public GameObject enemyPrefab;
	public int round;
	public int score;
	public GameObject scoreText;
	public float roundTimer;
	public float roundDuration;
	public GameObject roundText;
	List <string> spawnData = new List<string>();
    

	void Awake () {
		DontDestroyOnLoad (this);
		score = 0;
		round = 0;
		roundTimer = 0;
		spawnData.Add ("");
		spawnData.Add ("");
		spawnData.Add ("1%P|2%P|3%P|4%P");
		spawnData.Add ("1%N|2%N|3%N|4%N");
		spawnData.Add ("1%P|2%N|3%P|4%N");
		spawnData.Add ("5%P|6%P|7%P|8%P");
		spawnData.Add ("5%N|6%N|7%N|8%N");
		spawnData.Add ("1%P|2%P|5%P|6%P|3%N|4%N|7%N|8%N");
	}

	void Update () {
		if (SceneManager.GetActiveScene ().name == "Main") {
			RoundTimer ();
			UpdateScoreText ();
            
		}
	}

	void RoundTimer(){
		roundTimer -= Time.deltaTime;
		if (roundTimer <= 0) {
			if (round != 0 && round != 1) {
				score += 1000;
			}
            NextRound ();
			roundTimer = roundDuration;
            
		}
		roundText.GetComponent<Text> ().text = "  " + roundTimer.ToString ("0.0");
	}

	void NextRound(){
		round++;
		Debug.Log (spawnData.Count);
		Debug.Log (round);
		if (spawnData.Count <= round) {
			spawnData.Add ("1%P|2%P|5%P|6%P|3%N|4%N|7%N|8%N");
		}
		if (spawnData [round] == "") {
			return;
		}
		string[] spawns = spawnData [round].Split ('|');
		for (int i = 0; i < spawns.Length; i++) {
			GameObject enemy = Instantiate (enemyPrefab);
			EnemyScript enemyScript = enemy.GetComponent<EnemyScript> ();
			string[] data = spawns [i].Split ('%');
			switch (data [0]) {
			case "1":
				enemy.transform.position = new Vector3 (-5, 5, 0); //topleft
				break;
			case "2":
				enemy.transform.position = new Vector3 (5, 5, 0); //topright
				break;
			case "3":
				enemy.transform.position = new Vector3 (5, -5, 0); //bottomright
				break;
			case "4":
				enemy.transform.position = new Vector3 (-5, -5, 0); //bottomleft
				break;
			case "5":
				enemy.transform.position = new Vector3 (0, 5, 0); //top
				break;
			case "6":
				enemy.transform.position = new Vector3 (5, 0, 0); //right
				break;
			case "7":
				enemy.transform.position = new Vector3 (0, -5, 0); //bottom
				break;
			case "8":
				enemy.transform.position = new Vector3 (-5, 0, 0); //left
				break;
			default:
				enemy.transform.position = new Vector3 (-5, 5, 0);
				break;
			}

			if (data [1] == "P") {
				enemyScript.magneticCharge = 1;
				//enemy.GetComponent<SpriteRenderer> ().sprite = enemyScript.positiveSprite;
			} else if (data [1] == "N") {
				enemyScript.magneticCharge = -1;
				//enemy.GetComponent<SpriteRenderer> ().sprite = enemyScript.negativeSprite;
			} else {
				enemyScript.magneticCharge = 0;
			}

		}
	}

	void UpdateScoreText(){
		scoreText.GetComponent<Text> ().text = " " + score;
	}

  
}
