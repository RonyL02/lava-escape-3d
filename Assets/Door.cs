using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator animator;

    public bool isOpenOnTrigger = true;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collided with player");
            animator.SetBool("open", isOpenOnTrigger);
        }
    }
}
