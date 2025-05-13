using UnityEngine;

public class WallSpawner : MonoBehaviour
{
    public GameObject prefab;
    public float spawnInterval = 2f;
    private float timer = 0f;

    private bool startSpawning = false;

    void Update()
    {
        if (startSpawning)
        {
            timer += Time.deltaTime;

            if (timer >= spawnInterval)
            {
                timer = 0f;
                Instantiate(prefab);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            startSpawning = true;
        }
    }
}
