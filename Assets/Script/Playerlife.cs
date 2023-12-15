using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Playerlife : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Mob"))
        {
            Die();
        }
    }
    private void Die()
    {
        anim.SetTrigger("death");
    }
    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }
    private void Attack()
    {
        //play animation attack
        anim.SetTrigger("Attack");
        
    }
}
