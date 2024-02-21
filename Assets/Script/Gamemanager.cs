using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    [Header("Game Objects")]
    [SerializeField] private GameObject player;
    [SerializeField] private UnityEngine.Screen screen;
    [SerializeField] private Canvas gameOverText;
    [SerializeField] private Canvas gamePauseText;
    private Animator playerAnimator;
    private bool isPlayerDeath;
    private bool isRestarting;
    private const float objectLifetime = 10.0f;

    private List<GameObject> SpawnedObjects = new List<GameObject>();

    private void Awake() 
    {
        Time.timeScale = 1;
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        InitializePlayer();
        StartCoroutine(CleanupObjectsCoroutine());
    }

    private void Start()
    {

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
        gamePause();
    }

    private void gamePause()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            gamePauseText.gameObject.SetActive(true);
            Time.timeScale = 0;
        
        }
        else
        {

        }
    }
    private IEnumerator DelayedGameOver()
    {
        yield return new WaitForSeconds(2f);  // 
        gameOverText.gameObject.SetActive(true);
        Time.timeScale = 0;
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
                    StartCoroutine(DelayedGameOver());  
                }
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
    private void LogError(string message)
    {
        Debug.LogError($"[{nameof(GameManager)}] {message}");
    }
}