using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSetting : MonoBehaviour
{
    [SerializeField] private Canvas setting;
    [SerializeField] private Canvas mainscene;
    public void ActiveScene(string sceneName)
    {
        setting.gameObject.SetActive(true);
        mainscene.gameObject.SetActive(false);
    }
}
