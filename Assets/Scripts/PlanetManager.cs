using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetManager : MonoBehaviour
{
    public static PlanetManager instance = null;

    public GameObject[] Planets;//an array of Planets Prefabs

    //Queue to hold the planets
    Queue<GameObject> availablePlanets = new Queue<GameObject>();

    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a PlanetManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        //add the planets to the Queue
        availablePlanets.Enqueue(Planets[0]);
        availablePlanets.Enqueue(Planets[1]);
        availablePlanets.Enqueue(Planets[2]);
        availablePlanets.Enqueue(Planets[3]);
        
        //call the MovePlanetDown every 20 seconds
        InvokeRepeating("MovePlanetDown", 0, 20f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Function to dequeue a planet, and set its isMoving flag to true
    //so that the planet starts scrolling down the screen
    void MovePlanetDown()
    {

        EnqueuePlanets();

        //if the Queue is empty, then return
        if (availablePlanets.Count == 0)
            return;

        //get the planet from the queue
        GameObject planet = availablePlanets.Dequeue();

        //set the planet isMoving flag to true
        planet.GetComponent<Planet>().isMoving = true;
    }

    void EnqueuePlanets()
    {
        foreach(GameObject planet in Planets)
        {
            //if the planet is below the screen, and the planet is not moving
            if((planet.transform.position.y < 0) && (!planet.GetComponent<Planet>().isMoving))
            {
                //reset the planet position
                planet.GetComponent<Planet>().ResetPosition();

                //Enqueue the planet
                availablePlanets.Enqueue(planet);
            }
        }
    }
}
