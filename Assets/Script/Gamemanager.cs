using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private Vector2 spawnpoint;
    [SerializeField] private bool random;
    [SerializeField] private GameObject player;
    private Animator playerAnimator;
    private bool isPlayerDeath;

    private void Start()
    {
        playerAnimator = player.GetComponent<Animator>();
    }

    private void Update()
    {
        // Check for player death condition
        if (player.CompareTag("Player") && !isPlayerDeath)
        {
            // Assuming you have a boolean variable indicating death
            isPlayerDeath = player.GetComponent<Player>().Isdeath;

            if (isPlayerDeath)
            {
                StartCoroutine(RestartGameAfterDelay(2.0f));
            }
        }
    }

    private void OnSpawnPrefab()
    {
        if (random)
        {
            float x = Random.Range(-8, 80);
            float y = Random.Range(-4, 40);
            Vector2 randomSpawnPoint = new Vector2(x, y);
            Instantiate(prefab, randomSpawnPoint, Quaternion.identity);
        }
        else
        {
            Instantiate(prefab, spawnpoint, Quaternion.identity);
        }
    }

    private IEnumerator RestartGameAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}