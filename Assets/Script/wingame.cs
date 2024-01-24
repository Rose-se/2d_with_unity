using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class wingame : MonoBehaviour
{
    [SerializeField] private Canvas gameWinText;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
        gameWinText.gameObject.SetActive(true);
        Time.timeScale=0;
        }
    }
}
