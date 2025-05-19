using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public GameObject player;

    public static int score;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        int currentHeight = (int)player.transform.position.y;
        if (currentHeight > score)
        {
            score = currentHeight;
            Text scoreText = GetComponent<Text>();

            scoreText.text = score.ToString();
        }
    }
}
