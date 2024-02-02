using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveMain : MonoBehaviour
{
    [SerializeField] private Canvas setting;
    [SerializeField] private Canvas mainscene;
    public void ActiveScene(string sceneName)
    {
        setting.gameObject.SetActive(false);
        mainscene.gameObject.SetActive(true);
    }
}
