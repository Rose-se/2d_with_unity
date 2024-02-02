using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] private Canvas gamePauseText;
    public void Resume()
    {
        Time.timeScale = 1;
        Debug.Log("Enter");
        gamePauseText.gameObject.SetActive(false);
    }
}
