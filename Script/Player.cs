using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;
    private enum MovementState { idle,running,jumping,attack,death}
    private float dirX = 0;
    [SerializeField] private float movementSpeed = 7f;
    [SerializeField] private float junpForce = 16f;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {   
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * movementSpeed,rb.velocity.y);
        if(Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector3(rb.velocity.x ,junpForce);
        }
        if(Input.GetMouseButtonDown(0))
        {
            Attack();
        }
        UpdateAnimationState();
    }
    private void UpdateAnimationState()
    {
        MovementState state;
        if(dirX > 0f) 
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if(dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }
        if(rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        anim.SetInteger("state",(int)state);
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
    private void Attack()
    {
        anim.SetTrigger("Attack");//play animation attack
    }
}