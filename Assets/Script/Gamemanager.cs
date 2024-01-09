using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    [Header("Game Objects")]
    [SerializeField] private GameObject player;
    private Animator playerAnimator;
    private bool isPlayerDeath;
    private bool isRestarting;

    private const float restartDelay = 2.0f;
    private const float spawnDelay = 1.5f;
    private const float objectLifetime = 10.0f;

    private List<GameObject> SpawnedObjects = new List<GameObject>();

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
        InitializePlayer();
        StartCoroutine(CleanupObjectsCoroutine());
    }

    private void InitializePlayer()
    {
        if (player == null)
        {
            LogError("Player not set in GameManager! Please assign it in the Unity Editor.");
            return;
        }

        playerAnimator = player.GetComponent<Animator>();

        if (playerAnimator == null)
        {
            LogError("Animator component not found on the player GameObject.");
        }
    }

    private void Update()
    {
        CheckPlayerDeath();
    }

    private void CheckPlayerDeath()
    {
        bool isPlayerDeath = false;

        if (player != null && player.CompareTag("Player") && !isPlayerDeath)
        {
            Player playerComponent = player.GetComponent<Player>();

            if (playerComponent != null)
            {
                isPlayerDeath = playerComponent.IsDeath;

                if (isPlayerDeath && !isRestarting)
                {
                    StartCoroutine(RestartGameAfterDelay(restartDelay));
                }
            }
            else
            {
                LogError("Player component not found on the player GameObject.");
            }
        }
    }

    private IEnumerator CleanupObjectsCoroutine()
    {
        while (!isPlayerDeath)
        {
            yield return new WaitForSeconds(objectLifetime);

            foreach (GameObject obj in SpawnedObjects)
            {
                if (obj != null)
                {
                    Destroy(obj);
                }
            }

            SpawnedObjects.Clear();
        }
    }

    private IEnumerator RestartGameAfterDelay(float delay)
    {
        isRestarting = true;
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        isRestarting = false;
    }

    private void LogError(string message)
    {
        Debug.LogError($"[{nameof(GameManager)}] {message}");
    }
}