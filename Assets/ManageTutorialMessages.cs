using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageTutorialMessages : MonoBehaviour
{
    public GameObject room1Prompt;
    public GameObject room2Prompt;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            room1Prompt.SetActive(false);
            room2Prompt.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
