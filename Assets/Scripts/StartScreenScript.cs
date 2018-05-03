using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StartScreenScript : MonoBehaviour {
    private Animator FadeAway;
    IEnumerator FadeTime;
    public GameObject StartText;
	// Use this for initialization
	void Start () {
        FadeTime = FadeAways();
        FadeAway = GetComponent<Animator>();
        FadeAway.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown) {
            StartCoroutine(FadeTime);
            Destroy(StartText);
            
        }
	}
    private IEnumerator FadeAways()
    {
       
        
            FadeAway.enabled = true;

            yield return new WaitForSeconds(1f);
                
            SceneManager.LoadScene("Main",LoadSceneMode.Single);
        
    }
}

