using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeCounter : MonoBehaviour
{
    TextMeshProUGUI timerText;

    float startTime;//the time when the user clicks on play
    float ellapsedTime;//the ellapsed time after the user clicks on play
    bool startCounter;//flag to start the counter

    int minutes;
    int seconds;

    public float EllapsedTime
    {
        get
        {
            return this.ellapsedTime;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        startCounter = false;

        //get the timer text component
        timerText = GetComponent<TextMeshProUGUI>();
    }

    //function to start the timer counter
    public void StartTimeCounter()
    {
        startTime = Time.time;
        startCounter = true;
    }

    //function to stop the timer counter
    public void StopTimeCounter()
    {
        startCounter = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(startCounter)
        {
            //computed the ellapsed time
            ellapsedTime = Time.time - startTime;

            minutes = (int)ellapsedTime / 60;
            seconds = (int)ellapsedTime % 60;

            //update the text
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}
