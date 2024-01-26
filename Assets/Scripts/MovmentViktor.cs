using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovmentViktor : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 8f;
    public int maxJumps = 1; // Maximum number of jumps
    public Transform groundCheck;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;
    private int remainingJumps;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        remainingJumps = maxJumps;
    }

    void Update()
    {
        // Handle horizontal movement
        float horizontalInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

        // Ground check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        // Jumping
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded || remainingJumps > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                remainingJumps--;

                // Reset remaining jumps when grounded
                if (isGrounded)
                {
                    remainingJumps = maxJumps;
                }
            }
        }
    }
}
