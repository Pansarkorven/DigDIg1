using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterController : MonoBehaviour
{
    [SerializeField] float horizontal;
    public bool isRunning = false; // Flag to indicate if the player is running
    [SerializeField] float runningSpeed = 12f; // Speed when running
    [SerializeField] float walkingSpeed = 8f; // Speed when walking
    [SerializeField] float jumpingPower = 16f;
   public bool isFacingRight = true;
    [SerializeField] Vector2 boxSize = new Vector2(0.5f, 2f);
    [SerializeField] float flipDistance = 0.1f;

    [SerializeField] Animator Anim;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    // Update is called once per frame

    private void Start()
    {
        Anim = GetComponent<Animator>();
    }
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        // Toggle running
        if (Input.GetKeyDown(KeyCode.LeftShift) && horizontal != 0)
        {
            isRunning = true;
        }
        else if (!Input.GetKey(KeyCode.LeftShift) || horizontal == 0)
        {
            isRunning = false;
        }

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            CheckInteraction();
        }

        if (horizontal != 0)
        {
            Anim.SetBool("IsRunning", true);
        }
        else
        {
            Anim.SetBool("IsRunning", false);
        }

        Flip();
    }

    private void FixedUpdate()
    {
        // Calculate movement speed
        float currentSpeed = isRunning ? runningSpeed : walkingSpeed;
        rb.velocity = new Vector2(horizontal * currentSpeed, rb.velocity.y);
    }

    private void CheckInteraction()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, boxSize, 0);

        foreach (Collider2D collider in colliders)
        {
            Interaction interactionComponent = collider.GetComponent<Interaction>();

            if (interactionComponent != null)
            {
                interactionComponent.Interact();
                return;
            }
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if ((isFacingRight && horizontal < 0f) || (!isFacingRight && horizontal > 0f))
        {
            isFacingRight = !isFacingRight;

            // Store the current position
            Vector3 currentPosition = transform.position;

            // Flip the scale
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;

            // Adjust the position to reflect the flip
            // Adjust this value based on how much you want the character to move when flipping
            transform.position = new Vector3(currentPosition.x + (isFacingRight ? flipDistance : -flipDistance), currentPosition.y, currentPosition.z);
        }
    }

  
}
