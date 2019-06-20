using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameScore : MonoBehaviour
{
    TextMeshProUGUI scoreTextUI;

    int score;
    // Start is called before the first frame update
    void Awake()
    {
        scoreTextUI = GetComponent<TextMeshProUGUI>();
    }

    public void AddScore(int value)
    {
        score += value;
        UpdateScoreTextUI();
    }

    public void ResetScore()
    {
        score = 0;
        UpdateScoreTextUI();
    }

    public int GetScore()
    {
        return score;
    }

    //function to update the score text ui
    void UpdateScoreTextUI()
    {
        scoreTextUI.text = string.Format("{0:000000}", score);
    }
}
