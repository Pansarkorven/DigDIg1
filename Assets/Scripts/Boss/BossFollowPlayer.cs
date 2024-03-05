using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFollowPlayer : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 5f;

    void Update()
    {
        MoveTowardsPlayer();
    }

    void MoveTowardsPlayer()
    {
        if (player != null)
        {
            // Calculate the direction towards the player
            Vector3 direction = (player.position - transform.position).normalized;

            // Only move along the X-axis
            direction.y = 0;

            // Update the boss's position
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }
    }
}
