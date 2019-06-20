using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Proyecto26;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField]
    GameObject timerCounter;
    [SerializeField]
    GameObject scoreCounter;

    public GameObject playerShip;
    public GameObject enemySpawner;

    AudioSource soundSource;

    public AudioClip menuSound;
    public AudioClip gameSound;
    public AudioClip loseSound;
    public AudioClip startSound;

    public enum GameState
    {
        Intro,
        Opening,
        GamePlay,
        GameOver
    }

    GameState GMState = GameState.Intro;

    string username;
    int finalScore;
    float finalTime;
    

    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    // When the app starts, check to make sure that we have
    // the required dependencies to use Firebase, and if not,
    // add them if possible.
    void Start()
    {
        //GMState = GameState.Opening;
        UpdateGameState();
    }
    
    //function to update the game state
    void UpdateGameState()
    {
        switch(GMState)
        {
            case GameState.Intro:
                //Play menu sound
                soundSource = instance.GetComponent<AudioSource>();
                soundSource.clip = menuSound;
                soundSource.Play();
                soundSource.loop = true;

                break;

            case GameState.Opening:
                //Play menu sound
                soundSource = instance.GetComponent<AudioSource>();
                soundSource.clip = menuSound;
                soundSource.Play();
                soundSource.loop = true;

                break;

            case GameState.GamePlay:
                timerCounter = GameObject.FindGameObjectWithTag("Timer");
                scoreCounter = GameObject.FindGameObjectWithTag("Score");

                //reset the score
                scoreCounter.GetComponent<GameScore>().ResetScore();

                //Play game sound
                soundSource = instance.GetComponent<AudioSource>();
                soundSource.clip = gameSound;
                soundSource.Play();
                soundSource.loop = true;

                //set the player visible active and init the player lives
                GameObject player = (GameObject)Instantiate(playerShip);
                //reset the player position
                player.transform.position = new Vector2(0, -4);
                //set this player game object to active
                player.SetActive(true);
                player.GetComponent<PlayerControl>().Init();

                //start enemy spawner
                GameObject enemies = (GameObject)Instantiate(enemySpawner);
                enemies.GetComponent<EnemySpawner>().ScheduleEnemySpawn();

                //start the timer counter
                timerCounter.GetComponent<TimeCounter>().StartTimeCounter();

                break;

            case GameState.GameOver:
                timerCounter = GameObject.FindGameObjectWithTag("Timer");
                //stop the timer counter
                timerCounter.GetComponent<TimeCounter>().StopTimeCounter();

                scoreCounter = GameObject.FindGameObjectWithTag("Score");

                //get the score
                finalScore = scoreCounter.GetComponent<GameScore>().GetScore();
                //get the time
                finalTime = timerCounter.GetComponent<TimeCounter>().EllapsedTime;

                //Play lose sound
                soundSource = instance.GetComponent<AudioSource>();
                soundSource.clip = loseSound;
                soundSource.Play();
                soundSource.loop = false;

                //Stop enemy spawner
                GameObject spawner = GameObject.FindGameObjectWithTag("EnemySpawner");
                spawner.GetComponent<EnemySpawner>().UnscheduleEnemySpawn();

                Destroy(spawner);

                GameObject canvas = GameObject.Find("Canvas");
                FindChildObject(canvas, "Game").SetActive(false);
                FindChildObject(canvas, "GameOverMenu").SetActive(true);

                GameObject.Find("FinalScore").GetComponent<TextMeshProUGUI>().text = string.Format("{0:000000}", finalScore);

                AddScore();

                break; 
        }
    }

    //function to set the game state
    public void SetGameState(GameState state)
    {
        GMState = state;
        UpdateGameState();
    }

    //the playbutton will call this function
    public void StartGamePlay()
    {
        GMState = GameState.GamePlay;
        UpdateGameState();
    }

    public void StartIntro()
    {
        GMState = GameState.Intro;
        UpdateGameState();
    }

    //the playbutton will call this function
    public void ReloadGamePlay()
    {
        GMState = GameState.GamePlay;
        GameObject canvas = GameObject.Find("Canvas");
        FindChildObject(canvas, "Game").SetActive(true);
        FindChildObject(canvas, "GameOverMenu").SetActive(false);
        UpdateGameState();
    }

    //the mainMenuButton will call this function
    public void StartMenu()
    {
        SceneManager.LoadScene("Menu");
        GMState = GameState.Opening;
        UpdateGameState();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public GameObject FindChildObject(GameObject go, string name)
    {
        for (int i = 0; i < go.transform.childCount; i++)
        {
            if (go.transform.GetChild(i).gameObject.name == name) return go.transform.GetChild(i).gameObject;
        }

        return null;  //couldn't find crap
    }

    public void SetUsername(string value)
    {
        username = value;
        SceneManager.LoadScene("Game");
        StartGamePlay();
    }

    public void AddScore()
    {
        Score score = new Score(username, finalScore, finalTime);
        Debug.Log(score.score);
        RestClient.Post("https://spaceshooting-8ca14.firebaseio.com/.json", score);
    }
}
