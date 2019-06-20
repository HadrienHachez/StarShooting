using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    public float speed; //the speed of the stars

    Color[] starColors =
    {
        new Color(0.5f, 0.5f, 1f), //blue
        new Color(0, 1f, 1f), //green
        new Color(1f, 1f, 0f), //yellow
        new Color(1f, 1f, 1f), //white
    };

    public Sprite[] sprites;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().color = starColors[Random.Range(0, starColors.Length)];
        gameObject.GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        //Get the current position of the star
        Vector2 position = transform.position;

        //Compute the start's new position
        position = new Vector2(position.x, position.y + speed * Time.deltaTime);

        //Update the star's position
        transform.position = position;

        //this is the bottom-left point of the screen
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        //this is the top-right point of the screen
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        //If the star goes outside the screen on the bottom,
        //then position the star on the top edge of the screen
        //and randomly between the left and right side of the screen.
        if(transform.position.y < min.y)
        {
            gameObject.GetComponent<SpriteRenderer>().color = starColors[Random.Range(0, starColors.Length)];
            transform.position = new Vector2(Random.Range(min.x, max.x), max.y);
        }
    }
}
