using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class wingame : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
        LoadScene();
        }
    }
    private void LoadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(5);
    }
}
