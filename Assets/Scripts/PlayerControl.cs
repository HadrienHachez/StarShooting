using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerControl : MonoBehaviour
{
    public GameObject PlayerBulletPrefab;
    public GameObject bulletPosition;
    public GameObject ExplosionPrefab;

    // Reference to Rigidbody2D component of the ball game object
    Rigidbody2D rb;

    float shipBoundaryRadius = 0.3f;

    // Direction variables that read acceleration input to be added
    // as velocity to Rigidbody2d component
    float directionX;
    float directionY;

    // Range option so moveSpeedModifier can be modified in Inspector
    // this variable helps to simulate objects acceleration
    [Range(1f, 10f)]
    public float speed = 10f;

    //Reference to the lives ui text
    TextMeshProUGUI LivesUIText;

    const int MaxLives = 3;//Maxium player lives
    int lives;//current player lives

    public void Init()
    {
        lives = MaxLives;

        //update the lives UI text
        LivesUIText.text = lives.ToString();
    }

    void Awake()
    {
        LivesUIText = GameObject.FindGameObjectWithTag("Lives").GetComponent<TextMeshProUGUI>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // I like to process touch phases in switch statement
            switch (touch.phase)
            {

                // So if touch began
                case TouchPhase.Began:

                    //Instantiate the first bullet
                    GameObject bullet = (GameObject)Instantiate(PlayerBulletPrefab);
                    bullet.transform.position = bulletPosition.transform.position;//set the bullet initial position
                    break;
            }
        }

        directionX = Input.acceleration.x * speed;
        directionY = Input.acceleration.y * speed;

        Vector2 pos = new Vector2(transform.position.x, transform.position.y);

        //first vertical, because it's simpler
        if(pos.y + shipBoundaryRadius > Camera.main.orthographicSize)
        {
            pos.y = Camera.main.orthographicSize - shipBoundaryRadius;
        }
        if (pos.y - shipBoundaryRadius < -Camera.main.orthographicSize)
        {
            pos.y = -Camera.main.orthographicSize + shipBoundaryRadius;
        }

        //Now calculate the orthographic width based on the screen ratio
        float screenRatio = (float)Screen.width / (float)Screen.height;
        float widthOrtho = Camera.main.orthographicSize * screenRatio;

        //Now do horizontal bounds
        if(pos.x + shipBoundaryRadius > widthOrtho)
        {
            pos.x = widthOrtho - shipBoundaryRadius;
        }

        if (pos.x - shipBoundaryRadius < -widthOrtho)
        {
            pos.x = -widthOrtho + shipBoundaryRadius;
        }

        transform.position = pos;
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(directionX, directionY);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //Detect collision of the player ship with an enemy, or with an enemy bullet
        if((collision.tag == "EnemyShip") || (collision.tag == "EnemyBullet"))
        {

            lives--;//substract one live
            LivesUIText.text = lives.ToString();//update lives UI Text

            if(lives == 0)
            {
                Debug.Log("Play Explosion");
                PlayExplosion();

                Debug.Log("GameOver");
                GameManager.instance.GetComponent<GameManager>().SetGameState(GameManager.GameState.GameOver);

                Debug.Log("Disable Player");
                gameObject.SetActive(false);
            }
        }
    }

    void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(ExplosionPrefab);

        //set the position of the explosion
        explosion.transform.position = transform.position;
    }
}
