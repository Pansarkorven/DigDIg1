using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFollowPlayer : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float jumpHeightThreshold = 2f; // Adjust this value based on the desired distance
    public float jumpCooldown = 2f; // Adjust this value for the cooldown duration
    public LayerMask groundLayer;

    private float lastJumpTime;
    private float lastPlayerAboveTime; // New variable to track the time when the player was last detected above the boss
    public float playerAboveCooldown = 2f; // New variable to set the cooldown duration between detecting player above and jumping

    public bool attacking = false;

    private bool canMove = true;

    void Update()
    {
        if (canMove)
        {
            MoveTowardsPlayer();
        }
    }

    void MoveTowardsPlayer()
    {
        if (player != null || attacking != false)
        {
            // Calculate the direction towards the player
            Vector3 direction = (player.position - transform.position).normalized;

            // Only move along the X-axis
            direction.y = 0;

            // Update the boss's position
            transform.Translate(direction * moveSpeed * Time.deltaTime);

            // Check if the player is significantly above the boss and the cooldown has passed
            float verticalDistance = player.position.y - transform.position.y;
            if (verticalDistance > 3.85 && verticalDistance < jumpHeightThreshold && Time.time - lastJumpTime > jumpCooldown)
            {
                // Check if the cooldown between detecting player above has passed
                if (Time.time - lastPlayerAboveTime > playerAboveCooldown)
                {
                    // Perform the jump or any other action
                    Jump();
                    // Record the time when the player was last detected above the boss
                    lastPlayerAboveTime = Time.time;
                }
            }
        }
    }

    void Jump()
    {
        // Implement jumping logic here
        // For example, applying a force to the boss's Rigidbody2D
        GetComponent<Rigidbody2D>().velocity = new Vector2(0f, jumpForce);

        // Record the time of the jump for cooldown calculation
        lastJumpTime = Time.time;
    }

    // Function to stop the boss movement
    public void StopMoving()
    {
        canMove = false;
    }

    // Function to start the boss movement
    public void StartMoving()
    {
        canMove = true;
    }
}