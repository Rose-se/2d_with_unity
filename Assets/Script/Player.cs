using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool IsDeath { get; private set; }

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private CapsuleCollider2D coll;
    private Animator anim;

    private enum MovementState { Idle, Running, Jumping, Falling, Attack }

    [SerializeField] private float movementSpeed = 7f;
    [SerializeField] private float jumpForce = 16f;
    [SerializeField] private float fallSpeed = 2.5f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float raycastRadius = 0.1f;
    [SerializeField] private ParticleSystem collectParticle = null;
    [SerializeField] private GameObject tomb;
    [SerializeField] private Transform[] spawnPoint;
    [SerializeField] private AudioClip deadsound;
    [SerializeField] private AudioClip jumpsound;
    private int currentSpawnpointIndex = 0;

    private float movementInput = 0f;
    private bool attacking = false;
    private bool jumping = false;

    private const float FallingThreshold = -0.5f;
    private const KeyCode JumpKeyCode = KeyCode.Space;
    private const KeyCode AttackKeyCode = KeyCode.F;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<CapsuleCollider2D>();
    }

    private void Update()
    {
        movementInput = Input.GetAxisRaw("Horizontal");

        // Jump logic
        if(Input.GetKey(KeyCode.Space) && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            AudioSource audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.PlayOneShot(jumpsound);
            jumping = true;
        }
        else
        {
            jumping = false; // Stop jumping when the key is released
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
        rb.velocity = new Vector2(movementInput * movementSpeed, rb.velocity.y);

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
        MovementState state = MovementState.Idle;

        if (attacking)
        {
            state = MovementState.Attack;
        }
        else if (movementInput > 0f)
        {
            state = MovementState.Running;
            sprite.flipX = false;
        }
        else if (movementInput < 0f)
        {
            state = MovementState.Running;
            sprite.flipX = true;
        }

        if (jumping)
        {
            state = MovementState.Jumping;
        }
        else if (rb.velocity.y < FallingThreshold && !IsGrounded())
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
        RaycastHit2D hitRegularGround = Physics2D.Raycast(colliderCenter, Vector2.down, colliderExtentsY + raycastRadius, groundLayer);

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
    private void SpawnTomb()
    {
        if (currentSpawnpointIndex < spawnPoint.Length)
        {
        Vector2 playerPosition = transform.position; // transform.position ของผู้เล่น
        Instantiate(tomb, playerPosition, Quaternion.identity);
        Debug.Log("Spawning tomb at player position");
        }
        else
        {
            Debug.LogError("Invalid currentSpawnpointIndex. Check the length of spawnPoint array.");
        }
    }
    private void Die()
    {
        anim.SetBool("IsDeath", true);
        collectParticle.Play();
        SpawnTomb();
        IsDeath = true;
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.PlayOneShot(deadsound);
        currentSpawnpointIndex++;
    }
}