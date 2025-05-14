using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    float initialSpeed = 0.1f;
    float acceleration = 0.5f;

    private float currentSpeed;
    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = initialSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * currentSpeed * Time.deltaTime;
        currentSpeed += acceleration * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Platform")
        {
            Destroy(other.gameObject); // Remove the object that triggered this one
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
