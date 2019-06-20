using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public void PlayGame() {
        SceneManager.LoadScene("Intro");
    }

    public void HighScore()
    {
        SceneManager.LoadScene("ScoreBoard");
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void SetUsername(TMP_InputField input)
    {
        GameManager.instance.SetUsername(input.text);
    }
}
