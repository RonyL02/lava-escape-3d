using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator animator;
    public AudioSource audioSource;

    public float audioDurationSeconds = 1f;
    public bool isOpenOnTrigger = true;
    void Start()
    {

    }

    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (animator.GetBool("open") != isOpenOnTrigger)
            {
                animator.SetBool("open", isOpenOnTrigger);
                StartCoroutine(PlayLimitedTime(audioDurationSeconds));
            }
        }
    }

    private IEnumerator PlayLimitedTime(float seconds)
    {
        audioSource.Play();
        yield return new WaitForSeconds(seconds);
        audioSource.Stop();
    }
}
