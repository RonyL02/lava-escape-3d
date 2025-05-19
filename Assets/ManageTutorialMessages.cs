using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageTutorialMessages : MonoBehaviour
{
    public GameObject room1Prompt;
    public GameObject room2Prompt;
    public GameObject score;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            room1Prompt.SetActive(false);
            room2Prompt.SetActive(true);
            score.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
