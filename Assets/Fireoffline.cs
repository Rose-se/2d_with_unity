using System.Collections;
using UnityEngine;

public class Fireoffline : MonoBehaviour
{
    void Awake()
    {
        StartCoroutine(LifeTimeCoroutine());
    }

    private IEnumerator LifeTimeCoroutine()
    {
        // Wait for 2 seconds
        yield return new WaitForSeconds(2f);

        // Check if the object is still active before destroying
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Mob"))
        {
            // Do something, e.g., set a score, play a sound, etc.
            // For now, let's just destroy the GameObject.
            gameObject.SetActive(false);
        }
    }
}
