using System.Collections;
using UnityEngine;

public class FireOffline : MonoBehaviour
{
    void Update()
    {
        if(gameObject.activeSelf)
        {
        StartCoroutine(LifeTimeCoroutine());
        }
    }

    private IEnumerator LifeTimeCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
    }
}
