using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovmentViktorTemp : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isMoving;

    private Animator anim;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        HandleMovement();
        HandleJump();
        UpdateGroundedStatus();
    }

    void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
        rb.velocity = movement;

        // Flip character sprite based on movement direction
        if (horizontalInput < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (horizontalInput > 0)
            transform.localScale = new Vector3(1, 1, 1);

        isMoving = Mathf.Abs(horizontalInput) > 0.1f;
    }

    void HandleJump()
    {
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    void UpdateGroundedStatus()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }



}
