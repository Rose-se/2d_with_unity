using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private Vector2 spawnpoint;
    [SerializeField] private bool random;
    
    public void OnspwanPrefab()
    {
        if(random)
        {
            float x = Random.Range(-8,80);
            float y = Random.Range(-4,40);
            Instantiate(prefab, new Vector2(x,y),Quaternion.identity);
        }
        else
        {
        Instantiate(prefab, spawnpoint,Quaternion.identity);
        }
    }
}
