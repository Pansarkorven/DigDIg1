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

    public bool attacking = false;
    void Update()
    {
        MoveTowardsPlayer();
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
                // Perform the jump or any other action
                Jump();
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
}
