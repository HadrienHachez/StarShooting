using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    GameObject scoreText;//reference to the text ui game object

    public GameObject ExplosionPrefab;

    float speed;

    void Awake()
    {
        //get the score
        scoreText = GameObject.FindGameObjectWithTag("Score");
    }

    // Start is called before the first frame update
    void Start()
    {
        speed = 2f;//Set speed
    }

    // Update is called once per frame
    void Update()
    {
        //Get the current position of the enemy
        Vector2 position = transform.position;

        //Compute the enemy's new position
        position = new Vector2(position.x, position.y - speed * Time.deltaTime);

        //Update the enemy's position
        transform.position = position;


        //this is the top-right point of the screen
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        //If the enemy went outside the screen on the bottom, then destroy the bullet
        if (transform.position.y < min.y)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //Detect collision of the player ship with an enemy, or with an player bullet
        if ((collision.tag == "PlayerShip") || (collision.tag == "PlayerBullet"))
        {
            PlayExplosion();

            //add 50 points to the score
            scoreText.GetComponent<GameScore>().AddScore(50);

            Destroy(gameObject);//destroy the enemy ship
        }
    }

    void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(ExplosionPrefab);

        //set the position of the explosion
        explosion.transform.position = transform.position;
    }
}
