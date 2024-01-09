using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spwanfireballmob : MonoBehaviour
{   
    [SerializeField] private GameObject prefab;
    [SerializeField] private Vector2 spawnpoint;
    [SerializeField] private bool random;
    [SerializeField] private float spawnDelay = 1.0f;
    [SerializeField] private float objectLifetime = 5.0f; // Adjust the lifetime as needed

    private List<GameObject> spawnedObjects = new List<GameObject>();

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(SpawnPrefabWithDelay());
        StartCoroutine(CleanupObjects());
    }

    private void SpawnPrefab()
    {
        Vector2 spawnPosition = random
            ? new Vector2(Random.Range(-8, 80), Random.Range(-4, 40))
            : spawnpoint;

        GameObject newObject = Instantiate(prefab, spawnPosition, Quaternion.identity);
        spawnedObjects.Add(newObject);

        // Start a coroutine to destroy the object after its lifetime
        StartCoroutine(DestroyAfterLifetime(newObject, objectLifetime));

        Debug.Log("Spawning Prefab");
    }

    private IEnumerator SpawnPrefabWithDelay()
    {
        while (true)
        {
            SpawnPrefab();
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private IEnumerator CleanupObjects()
    {
        while (true)
        {
            // Your cleanup logic here

            yield return null;
        }
    }

    private IEnumerator DestroyAfterLifetime(GameObject obj, float lifetime)
    {
        yield return new WaitForSeconds(lifetime);
        if (obj != null)
        {
            spawnedObjects.Remove(obj);
            Destroy(obj);
            Debug.Log("Destroying Prefab");
        }
    }
}