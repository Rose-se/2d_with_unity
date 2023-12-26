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
    private CapsuleCollider2D coll;
    private Animator anim;
    private enum MovementState { idle,running,jumping,falling,attack}
    private float dirX = 0;
    [SerializeField] private float movementSpeed = 7f;
    [SerializeField] private float jumpForce = 16f;
    [SerializeField] private LayerMask jumpableGround;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<CapsuleCollider2D>();
    }
    private void Update()
    {   
        UpdateAnimationState();
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * movementSpeed,rb.velocity.y);
        if(Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector3(rb.velocity.x ,jumpForce);
        }
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
        else  if(rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }
        if(Input.GetKeyDown(KeyCode.F))
        {
            state = MovementState.attack;
        }
        anim.SetInteger("state",(int)state);
    }
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center,coll.bounds.size,0f,Vector2.down,1f,jumpableGround);
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
        anim.SetBool("death",true);
        RestartGame();
    }
    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}