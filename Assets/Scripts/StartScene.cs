using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScene : MonoBehaviour
{

    public Text record;

    void Start()
    {
        record.text = record.text + " " + PlayerPrefs.GetInt("score").ToString();
    }
    public void StartGame()
    {
        Score.score = 0;
        SceneManager.LoadSceneAsync("FinalScene");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
