using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingViktor : MonoBehaviour
{
    public int maxJumps;
    public float jumpForce;
    public float maxButtonHoldTime;
    public float holdForce;
    public float distanceToCollider;
    public float maxJumpSpeed;
    public float maxFallSpeed;
    public float fallSpeed;
    public float gravityMultipler;
    public LayerMask collisionLayer;

    public Rigidbody2D rb;

    private bool jumpPressed;
    private bool jumpHeld;
    private float buttonHoldTime;
    private float originalGravity;
    private int numberOfJumpsLeft;
    private CharachterViktor charachter;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        buttonHoldTime = maxButtonHoldTime;
        originalGravity = rb.gravityScale;
        numberOfJumpsLeft = maxJumps;
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            jumpPressed = true;
        }
        else
            jumpPressed = false;
        if(Input.GetKey(KeyCode.Space))
        {
            jumpHeld = true;
        }
        else
            jumpHeld = false;
        CheckForJump();
        GroundCheck();
    }

    private void FixedUpdate()
    {
        isJumping();
    }

    private void CheckForJump()
    {
        if (jumpPressed) 
        {
          if(!charachter.isGrounded && numberOfJumpsLeft == maxJumps)
            {
                charachter.isJumping = false;
                return;
            }
          numberOfJumpsLeft--;
          if(numberOfJumpsLeft >= 0) 
            {
             rb.gravityScale = originalGravity;
             rb.velocity = new Vector2(rb.velocity.x, 0);
                buttonHoldTime = maxButtonHoldTime;
                charachter.isJumping = true;
            }
        }
    }

    private void isJumping()
    {
        if(charachter.isJumping) 
        {
            rb.AddForce(Vector2.up * jumpForce);
            AdditionalAir();
        }
        if(rb.velocity.y > maxJumpSpeed)
        {
            rb.velocity = new Vector2(rb.velocity.x, maxJumpSpeed);
        }
        Falling();
    }

    private void AdditionalAir()
    {
        if (jumpHeld) 
        {
         buttonHoldTime -= Time.deltaTime;
            if (buttonHoldTime < 0)
            {
                buttonHoldTime = 0;
                charachter.isJumping = false;
            }
            else
                rb.AddForce(Vector2.up * holdForce);
        }
        else
        {
            charachter.isJumping = false;
        }
    }

    private void Falling()
    {
        if (!charachter.isJumping && rb.velocity.y < fallSpeed) 
        {
            rb.gravityScale = gravityMultipler;
        }
        if(rb.velocity.y < maxFallSpeed) 
        {
            rb.velocity = new Vector2(rb.velocity.x, maxFallSpeed);
        }
    }

    private void GroundCheck()
    {
        if(charachter.CollisionCheck(Vector2.down, distanceToCollider,collisionLayer) && !charachter.isJumping) 
        {
         charachter.isGrounded = true;
            numberOfJumpsLeft = maxJumps;
            rb.gravityScale = originalGravity;
        }
        else
        {
            charachter.isGrounded = false;
        }
    }
}
