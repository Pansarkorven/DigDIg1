using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJump : MonoBehaviour
{
    public float jumpForce = 10f;
    public float wallJumpCooldown = 0.5f;
    public LayerMask wallLayer;

    private Rigidbody rb2d;
    private bool isTouchingWall;
    private bool canWallJump = true;

    private void Update()
    {
        isTouchingWall = Physics2D.Raycast(transform.position, Vector2.right, 0.1f, wallLayer) || Physics2D.Raycast(transform.position, Vector2.left, 0.1f, wallLayer);
        if (isTouchingWall && !IsGrounded())
        {
            if (Input.GetButtonDown("Jump") && canWallJump)
            {
                //WallJump();
            }
        }
    }
    private bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, 0.1f);

    }

    private void Walljump()
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
        if (Physics2D.Raycast(transform.position, Vector2.right, 0.1f, wallLayer))
        {
            rb2d.velocity = new Vector2(-jumpForce, jumpForce);
        }
        else if (Physics2D.Raycast(transform.position, Vector2.left, 0.1f, wallLayer))
        {
            rb2d.velocity = new Vector2(jumpForce, jumpForce);
        }
        canWallJump= false;
        StartCoroutine(WallJumpCooldown());
    }
    private System.Collections.IEnumerator WallJumpCooldown()
    {
        yield return new WaitForSeconds(wallJumpCooldown);
        canWallJump = true;
    }
}
