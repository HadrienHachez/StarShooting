using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public float speed; //the speed of the planet
    public bool isMoving;

    Vector2 min;
    Vector2 max;

    private void Awake()
    {
        isMoving = false;
        min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        //add the planet sprite half height to max y
        max.y = max.y + GetComponent<SpriteRenderer>().sprite.bounds.extents.y;
        //substract the planet sprite half height to min y
        min.y = min.y - GetComponent<SpriteRenderer>().sprite.bounds.extents.y;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving)
            return;

        //Get the current position of the planet
        Vector2 position = transform.position;

        //Compute the planet's new position
        position = new Vector2(position.x, position.y + speed * Time.deltaTime);

        //Update the planet's position
        transform.position = position;

        //If the planet gets to the minimun y position, then stop moving the planet
        if (transform.position.y < min.y)
        {
            isMoving = false;
        }
    }

    public void ResetPosition()
    {
        //reset the position of the planet to random x and max y
        transform.position = new Vector2(Random.Range(min.x, max.x), max.y);
    }
}
