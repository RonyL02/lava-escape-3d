using System.Collections;
using UnityEngine;

public class OnFire : MonoBehaviour
{
    public bool onFire = false;
    public GameObject fire;
    private GameObject firePrefab = null;
    public Vector3 offset = Vector3.zero;

    public float burnTimeSeconds = 3f;
    private Coroutine burnCoroutine = null;
    void Update()
    {
        if (onFire && firePrefab == null)
        {
            firePrefab = Instantiate(fire, transform);
            firePrefab.transform.localPosition = offset;
        }
        else if (!onFire && firePrefab != null)
        {
            Destroy(firePrefab);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("lava"))
        {
            onFire = true;
            burnCoroutine = StartCoroutine(StartBurning());
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("lava"))
        {
            onFire = false;

            if (burnCoroutine != null)
            {
                StopCoroutine(burnCoroutine);
                burnCoroutine = null;
            }
        }
    }

    private IEnumerator StartBurning()
    {
        yield return new WaitForSeconds(burnTimeSeconds);
        Die();
    }

    private void Die()
    {
        Debug.Log("Character has died.");
    }
}
