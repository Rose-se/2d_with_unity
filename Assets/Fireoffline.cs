using System.Collections;
using UnityEngine;

public class Fireoffline : MonoBehaviour
{
    [SerializeField] private float objectLifetime = 1.5f;
    private GameObject myGameObject;

    void Awake()
    {

        StartCoroutine(LifeTimeCoroutine(objectLifetime));
        myGameObject = gameObject;
    }

    private IEnumerator LifeTimeCoroutine(float lifetime)
    {
        // Wait for 2 seconds
        yield return new WaitForSeconds(lifetime);
        if (myGameObject.activeSelf)
        {
            myGameObject.SetActive(false);
        }
        
    }
}
