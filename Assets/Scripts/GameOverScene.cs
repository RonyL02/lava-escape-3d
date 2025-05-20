using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScene : MonoBehaviour
{
    public Text score;

    public Text record;

    void Start()
    {
        record.text += " " + PlayerPrefs.GetInt("score").ToString();
        score.text += " " + Score.score.ToString();
    }
    public void StartGame()
    {
        Score.score = 0;
        SceneManager.LoadSceneAsync("FinalScene");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadSceneAsync("StartScene");

    }
}
