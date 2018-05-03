using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{

    SpriteRenderer mySpriteRenderer;
    Rigidbody2D myRigidbody;
    public AudioClip shootAudioP;
    public AudioClip shootAudioN;
    public AudioClip LaserHum;
    public AudioClip HitByEnemy;
    public AudioClip DeathAudio;
    public GameObject TopPos;
    private Vector3 TPos;
    public GameObject PosBeam;
    public GameObject NegBEam;
    public bool StartCour=true;
    private IEnumerator GB;
    public GameObject Anchor;




    public GameObject healthText;

    public int lives;

    public float moveSpeed = 1.0f;

    public GameObject Beam;
    public int magneticCharge; //1 = positive, -1 = negative
    public float beamStr;

    Vector3 cursorPosition;

    public Sprite idle;
    public Sprite shootPositive;
    public Sprite shootNegative;


    void Awake()
    {
        //GB = GrowBeam();
        
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myRigidbody = GetComponent<Rigidbody2D>();
        lives = 5;
        healthText.GetComponent<Text>().text = "" + lives;


    }

    void Update()
    {
        CursorPosition();
        Move();
        LookAtCursor();
        Shoot();
        ShootAudio();
       // ResetCour();
        //UpdateScoreText ();
    }

    void Move()
    {
        float verticalVelocity = Input.GetAxis("Vertical");
        float horizontalVelocity = Input.GetAxis("Horizontal");
        Vector2 velocity = new Vector2(horizontalVelocity, verticalVelocity);

        myRigidbody.velocity += velocity * moveSpeed * Time.deltaTime;
    }

    void CursorPosition()
    {
        cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cursorPosition.z = 0;
    }

    void LookAtCursor()
    {
        transform.up = cursorPosition - transform.position;
    }

    void Shoot()
    {
        if (Input.GetMouseButton(0))
        {
            mySpriteRenderer.sprite = shootPositive;
            Beam.SetActive(true);
            Beam.GetComponent<SpriteRenderer>().color = new Color(0, 0, 1, .2f);
            magneticCharge = 1;
            SoundController.me.PlaySound(LaserHum, .7f);
          


        }
        else if (Input.GetMouseButton(1))
        {
            mySpriteRenderer.sprite = shootNegative;
            Beam.SetActive(true);
            Beam.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, .2f);
            magneticCharge = -1;
            SoundController.me.PlaySound(LaserHum, .7f);
           


        }
        else
        {
            mySpriteRenderer.sprite = idle;
            Beam.SetActive(false);
            magneticCharge = 0;
            

        }
    }

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if (collisionInfo.gameObject.tag == "Enemy")
        {
            Destroy(collisionInfo.gameObject);
            lives -= 1;
            SoundController.me.PlaySound(HitByEnemy, 1f);
            healthText.GetComponent<Text>().text = "" + lives;
            if (lives <= 0)
            {
                Death();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collisionInfo)
    {
        if (collisionInfo.gameObject.tag == "Laser")
        {
            lives -= 1;
            healthText.GetComponent<Text>().text = " " + lives;
            SoundController.me.PlaySound(LaserHum, .8f, .5f);
            if (lives <= 0)
            {
                Death();
            }
        }
    }

    void Death()
    {
        SoundController.me.PlaySound(DeathAudio, 1f);
        SceneManager.LoadScene("GameEnd", LoadSceneMode.Single);
    }

    void ShootAudio()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SoundController.me.PlaySound(shootAudioP, .5f);
           // StartCour = true;
        }
        else if (Input.GetMouseButtonDown(1))
        {
            SoundController.me.PlaySound(shootAudioN, .5f, .5f);
           
            //StartCour = true;
        }
       
    }

    //void UpdateScoreText(){
    //	scoreText.GetComponent<Text> ().text = "Score: " + score;
    //}
    /*private void ResetCour()
    {
        if (StartCour == true)
        {
            StartCoroutine(GB);
        }
        else
        {
            StopCoroutine(GB);
            StartCour = !StartCour;
        }
        


    }
    public IEnumerator GrowBeam()
    {
        //if (GrowBeamed==true)
        // {

        
        Anchor.transform.localScale = new Vector3(1, 0f);

      
            Debug.Log("Go");
            Anchor.transform.localScale = new Vector3(1, .2f);

            yield return new WaitForSeconds(.3f);
            Anchor.transform.localScale = new Vector3(1, .4f);

            yield return new WaitForSeconds(.2f);
            Anchor.transform.localScale = new Vector3(1, .7f);

            yield return new WaitForSeconds(.2f);
            Anchor.transform.localScale = new Vector3(1, 1f);


            yield return new WaitForSeconds(.2f);
            Anchor.transform.localScale = new Vector3(1, 1.5f);

            yield return new WaitForSeconds(.2f);
            Anchor.transform.localScale = new Vector3(1, 2f);

            yield return new WaitForSeconds(.2f);
            Anchor.transform.localScale = new Vector3(1, 2.5f);

        StartCour = false;


        }
         

   


       // }*/
        
    }

