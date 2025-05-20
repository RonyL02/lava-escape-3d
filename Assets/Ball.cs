using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("lava"))
        {
            Destroy(gameObject);
        }

        if (collision.collider.CompareTag("Player"))
        {
            int currentRecord = PlayerPrefs.GetInt("score");
            if (Score.score > currentRecord)
            {
                PlayerPrefs.SetInt("score", Score.score);
            }
            SceneManager.LoadSceneAsync("GameOver");
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
