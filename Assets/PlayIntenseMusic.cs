using UnityEngine;

public class PlayIntenseMusic : MonoBehaviour
{
    private bool isPlaying = false;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !isPlaying)
        {
            isPlaying = true;
            other.gameObject.GetComponent<AudioSource>().Play();
        }
    }
}
