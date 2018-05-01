using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventScript : MonoBehaviour {

	public GameObject enemyPrefab;
	public int round;
	public float roundTimer;
	public float roundDuration;
	List <string> spawnData = new List<string>();

	void Awake () {
		round = 0;
		roundTimer = 0;
		spawnData.Add ("");
		spawnData.Add ("1%P|2%P|3%P|4%P");
		spawnData.Add ("1%N|2%N|3%N|4%N");
		spawnData.Add ("1%P|2%N|3%P|4%N");
	}

	void Update () {
		RoundTimer ();
	}

	void RoundTimer(){
		roundTimer -= Time.deltaTime;
		if (roundTimer <= 0) {
			NextRound ();
			roundTimer = roundDuration;
		}
	}

	void NextRound(){
		round++;
		string[] spawns = spawnData [round].Split ('|');
		for (int i = 0; i < spawns.Length; i++) {
			GameObject enemy = Instantiate (enemyPrefab);
			EnemyScript enemyScript = enemy.GetComponent<EnemyScript> ();
			string[] data = spawns [i].Split ('%');
			switch (data [0]) {
			case "1":
				enemy.transform.position = new Vector3 (-5, 5, 0);
				break;
			case "2":
				enemy.transform.position = new Vector3 (5, 5, 0);
				break;
			case "3":
				enemy.transform.position = new Vector3 (5, -5, 0);
				break;
			case "4":
				enemy.transform.position = new Vector3 (-5, -5, 0);
				break;
			default:
				enemy.transform.position = new Vector3 (-5, 5, 0);
				break;
			}

			if (data [1] == "P") {
				enemyScript.magneticCharge = 1;
			} else if (data [1] == "N") {
				enemyScript.magneticCharge = -1;
			} else {
				enemyScript.magneticCharge = 0;
			}

		}
	}
}
