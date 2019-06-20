using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    public GameObject EnemyBulletPrefab;// this is our enemy bullet prefab

    GameObject player;

    float fireDelay = 0.75f;
    float cooldownTimer = 0;

    // Start is called before the first frame update
    void Awake()
    {
        //get a reference to the player's ship
        player = GameObject.FindGameObjectWithTag("PlayerShip");
    }

    // Update is called once per frame
    void Update()
    {
        //get a reference to the player's ship
        //GameObject player = GameObject.Find("Player");

        if (player != null)//if the player is not dead
        {
            cooldownTimer -= Time.deltaTime;
            if(cooldownTimer <= 0)
            {
                cooldownTimer = fireDelay;
                //instantiate an enemy bullet
                GameObject bullet = (GameObject)Instantiate(EnemyBulletPrefab);

                //set the bullet's initial position
                bullet.transform.position = transform.position;

                //compute the bullet's direction toward the player's shipp
                Vector2 direction = player.transform.position - bullet.transform.position;

                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;

                //set the bullet's direction
                bullet.GetComponent<EnemyBullet>().SetDirection(direction);

                //set the bullet's rotation
                bullet.GetComponent<EnemyBullet>().SetRotation(angle);
            }
        }
    }

    //function to fire an enemy bullet
    void FireEnemyBullet()
    {
        if (player != null)//if the player is not dead
        {
            //instantiate an enemy bullet
            GameObject bullet = (GameObject)Instantiate(EnemyBulletPrefab);

            //set the bullet's initial position
            bullet.transform.position = transform.position;

            //compute the bullet's direction toward the player's shipp
            Vector2 direction = player.transform.position - bullet.transform.position;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;

            //set the bullet's direction
            bullet.GetComponent<EnemyBullet>().SetDirection(direction);

            //set the bullet's rotation
            bullet.GetComponent<EnemyBullet>().SetRotation(angle);
        }
    }
}
