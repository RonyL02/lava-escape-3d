using UnityEngine;

public class MakePlayerJumpHigher : MonoBehaviour
{
    public FmsScript player;
    public float jumpSpeed = 28;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.jumpSpeed = jumpSpeed;
        }
    }
}
