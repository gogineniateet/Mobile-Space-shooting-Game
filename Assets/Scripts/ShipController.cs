using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipController : MonoBehaviour
{
    #region PUBLIC VARIABLES 
    public float rotationSpeed = 10f; //Rotation of ship in degrees per second
    public float movementSpeed = 2f;    // Force applied to ship in unit per second.
    public Transform launcher;
    GameManager gameManager;
    public int lives = 3;
    public bool isGameOver = false;
    public bool isWon = false;
    public Text healthText;
    public Text scoreText;
    #endregion


    #region PRIVATE VARIABLES 
    bool isMoving = false;
    const string TURN_COROUTINE_FUNCTION = "MoveTowardsTouch";
    private bool useAccelerometer;
    private int score;
    #endregion


    #region MONOBEHAVIOUR METHODS
    public static ShipController instance;
    public static ShipController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<ShipController>();

                if (instance == null)
                {
                    GameObject container = new GameObject("Ship");
                    instance = container.AddComponent<ShipController>();
                }
            }
            return instance;
        }
    }




    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
    }
    private void OnEnable() //Subscribing event when a GameObject is active
    {
        SpaceShooter.UserInputHandler.OnTouchAction += ToWardsTouch;
    }
    private void OnDisable()    //DeSubscribing event when a GameObject is active
    {
        SpaceShooter.UserInputHandler.OnTouchAction -= ToWardsTouch;
    }
    #endregion



    #region PUBLIC METHODS
    public void ToWardsTouch(Touch touch)
    {
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(touch.position); //It converts pixel coordinates to world coordinates.
        StopCoroutine(TURN_COROUTINE_FUNCTION);
        StartCoroutine(TURN_COROUTINE_FUNCTION, targetPosition);
    }
    public void LostLife(int life)
    {
        lives = lives - life;
        //Debug.Log("life" + lives);
        StartCoroutine(StartInvincibilityTimer(2.5f));
        healthText.text = lives.ToString();
        if (lives <= 0)
        {
            isGameOver = true;
            isWon = false;
            gameManager.GameOver();
        }
    }

    // updating score when eenmy destroyed
    public void UpdateScore(int value)
    {
        score = score + value;
        //Debug.Log(score);
        scoreText.text = score.ToString();
        if(score == 100)
        {
            isWon = true;
        }
    }
    #endregion



    #region PRIVATE METHODS
    private void Shoot()
    {
        BulletScript bullet = PoolManager.Instance.Spawn("Bullet").GetComponent<BulletScript>();
        bullet.SetPosition(launcher.position);
    }
    #endregion



    IEnumerator MoveTowardsTouch(Vector3 tempPoint)
    {
        isMoving = true;
        tempPoint.z = transform.position.z;  //Assigning z value of ship position to touch position
        tempPoint.y = transform.position.y;  //Assigning y value of ship position to touch position

        for (float i = 0; i < 1; i = i + Time.deltaTime)
        {
            transform.position = Vector3.Lerp(transform.position, tempPoint, i);
            yield return null;
        }
        transform.position = tempPoint;
        Shoot();
        isMoving = false;

    }

    private IEnumerator StartInvincibilityTimer(float timeLimit)
    {
        GetComponent<Collider2D>().enabled = false;

        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        float timer = 0;
        float blinkSpeed = 0.25f;

        while (timer < timeLimit)
        {
            yield return new WaitForSeconds(blinkSpeed);

            spriteRenderer.enabled = !spriteRenderer.enabled;
            timer += blinkSpeed;
        }
        spriteRenderer.enabled = true;
        GetComponent<Collider2D>().enabled = true;
    }


}
