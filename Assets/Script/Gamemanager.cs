using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    [SerializeField] private GameObject prefab, player;
    [SerializeField] private Vector2 spawnpoint;
    [SerializeField] private bool random;
    private Animator playerAnimator;
    private bool isPlayerDeath, isRestarting;

    private const float RestartDelay = 2.0f;
    private const float SpawnDelay = 1.5f;
    private const float ObjectLifetime = 10.0f;

    private List<GameObject> spawnedObjects = new List<GameObject>();

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        if (!player || !prefab)
        {
            Debug.LogError("Player or Prefab not set in GameManager!");
            return;
        }
        playerAnimator = player.GetComponent<Animator>();

        StartCoroutine(SpawnPrefabWithDelay());
        StartCoroutine(CleanupObjects());
    }

    private void Update()
    {
        CheckPlayerDeath();
    }

    private void CheckPlayerDeath()
    {
        if (player.CompareTag("Player") && !isPlayerDeath)
        {
            isPlayerDeath = player.GetComponent<Player>().Isdeath;

            if (isPlayerDeath && !isRestarting)
            {
                StartCoroutine(RestartGameAfterDelay(RestartDelay));
            }
        }
    }

    private IEnumerator SpawnPrefabWithDelay()
    {
        while (true)
        {
            SpawnPrefab();
            yield return new WaitForSeconds(SpawnDelay);
        }
    }

    private void SpawnPrefab()
    {
        Vector2 spawnPosition = random
            ? new Vector2(Random.Range(-8, 80), Random.Range(-4, 40))
            : spawnpoint;

        GameObject newObject = Instantiate(prefab, spawnPosition, Quaternion.identity);
        spawnedObjects.Add(newObject);
        Debug.Log("Spawning Prefab");
    }

    private IEnumerator CleanupObjects()
    {
        while (true)
        {
            yield return new WaitForSeconds(ObjectLifetime);

            foreach (GameObject obj in spawnedObjects)
            {
                if (obj != null)
                {
                    Destroy(obj);
                }
            }

            spawnedObjects.Clear();
        }
    }

    private IEnumerator RestartGameAfterDelay(float delay)
    {
        isRestarting = true;
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        isRestarting = false;
    }
}