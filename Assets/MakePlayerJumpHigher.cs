using UnityEngine;

public class MakePlayerJumpHigher : MonoBehaviour
{
    public FmsScript player;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.jumpSpeed = 26;
        }
    }
}
