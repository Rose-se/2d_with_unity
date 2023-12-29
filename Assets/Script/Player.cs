using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private CapsuleCollider2D coll;
    private Animator anim;

    private enum MovementState { Idle, Running, Jumping, Falling, Attack }

    private float dirX = 0;
    [SerializeField] private float movementSpeed = 7f;
    [SerializeField] private float jumpForce = 16f;
    [SerializeField] private float fallSpeed = 5f;
    [SerializeField] private LayerMask jumpableGround;
    private bool attacking = false;

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
        rb.velocity = new Vector2(dirX * movementSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce);
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity -= Vector2.up * fallSpeed * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            attacking = !attacking; // Toggle attacking
        }
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if (attacking)
        {
            state = MovementState.Attack;
        }
        else if (dirX > 0f)
        {
            state = MovementState.Running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.Running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.Idle;
        }

        if (rb.velocity.y > 0.1f)
        {
            state = MovementState.Jumping;
        }
        else if (rb.velocity.y < -0.1f)
        {
            state = MovementState.Falling;
        }

        anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 1f, jumpableGround);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Mob"))
        {
            Die();
        }
    }

    private void Die()
    {
        anim.SetBool("death", true);
        StartCoroutine(RestartGameWithDelay());
    }

    private IEnumerator RestartGameWithDelay()
    {
        yield return new WaitForSeconds(2.0f); // Add delay before restarting the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
