using UnityEngine;
using UnityEngine.SceneManagement;

public class Floor : MonoBehaviour
{
    public float initialSpeed = 0.001f;
    public float acceleration = 0.005f;

    private float currentSpeed;
    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = initialSpeed;
    }

    // Update is called once per frame

    void FixedUpdate()
    {
        transform.position += Vector3.up * currentSpeed * Time.fixedDeltaTime;
        currentSpeed += acceleration * Time.fixedDeltaTime;

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Platform")
        {
            Destroy(other.gameObject); // Remove the object that triggered this one
        }

        if (other.gameObject.tag == "Player")
        {
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadSceneAsync("GameOver");
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Wall")
        {
            Destroy(other.gameObject); // Remove the object that triggered this one
        }
    }
}
