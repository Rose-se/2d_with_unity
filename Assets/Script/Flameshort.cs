using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flameshort : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public GameObject destroyEffect;
    private void Start()
    {
        Invoke("DestroyFlameshort",lifetime);
    }
    private void Update()
    {
        transform.Translate(transform.up * speed * Time.deltaTime);
    }
    void DestroyFlameshort(){
    Instantiate(destroyEffect,transform.position,Quaternion.identity);
    Destroy(gameObject);
    }      
}