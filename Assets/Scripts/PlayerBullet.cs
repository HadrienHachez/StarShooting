using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = 8f;
    }

    // Update is called once per frame
    void Update()
    {
        //Get the current position of the bullet
        Vector2 position = transform.position;

        //Compute the bullet's new position
        position = new Vector2(position.x, position.y + speed * Time.deltaTime);

        //Update the bullet's position
        transform.position = position;
       

        //this is the top-right point of the screen
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        //If the bullet went outside the screen on the top, then destroy the bullet
        if (transform.position.y > max.y)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //Detect collision of the player bullet with an enemy ship
        if ((collision.tag == "EnemyShip"))
        {
            Destroy(gameObject);//destroy the bullet
        }
    }
}
