using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public bool Isdeath { get; private set; }
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private CapsuleCollider2D coll;
    private Animator anim;

    private enum MovementState { Idle, Running, Jumping, Falling, Attack }

    private float dirX = 0;
    [SerializeField] private float movementSpeed = 7f;
    [SerializeField] private float jumpForce = 16f;
    [SerializeField] private float fallSpeed = 2.5f;
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private float raycastRadius = 0.1f;
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
        dirX = Input.GetAxisRaw("Horizontal");

        // Jump logic
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // Attack logic
        if (Input.GetKey(KeyCode.F))
        {
            attacking = true; // Start attacking
        }
        else
        {
            attacking = false; // Stop attacking when the key is released
        }
    }

    private void FixedUpdate()
    {
        // Movement logic
        rb.velocity = new Vector2(dirX * movementSpeed, rb.velocity.y);

        // Falling logic
        if (rb.velocity.y < 0 && !IsGrounded())
        {
            // Apply additional gravity when falling
            rb.velocity += Vector2.up * Physics2D.gravity.y * fallSpeed * Time.deltaTime;
        }

        // Update animation state
        UpdateAnimationState();
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
        else if (rb.velocity.y < -0.5f && state != MovementState.Jumping)
        {
            state = MovementState.Falling;
        }

        anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        Vector2 colliderCenter = new Vector2(coll.bounds.center.x, coll.bounds.center.y) + coll.offset;
        float colliderExtentsY = coll.bounds.extents.y;

        // Perform a raycast to check if the player is grounded on a regular surface
        RaycastHit2D hitRegularGround = Physics2D.Raycast(colliderCenter, Vector2.down, colliderExtentsY + raycastRadius, jumpableGround);

        if (hitRegularGround.collider != null)
        {
            return true;
        }
        // If not grounded on a regular surface, check if the object below is a falling object
        RaycastHit2D hitFallingObj = Physics2D.Raycast(colliderCenter, Vector2.down, colliderExtentsY + raycastRadius);
        if (hitFallingObj.collider != null && hitFallingObj.collider.CompareTag("Untagged"))
        {
            return true;
        }
        return false;
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
        Isdeath = true;
    }
}