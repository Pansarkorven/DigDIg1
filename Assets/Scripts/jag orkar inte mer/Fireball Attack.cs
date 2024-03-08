using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballAttack : MonoBehaviour
{
    public Transform firePosition;
    public GameObject projectile;
    public GameObject playerObject; // Reference to the player's GameObject

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Instantiate projectile
            GameObject newProjectile = Instantiate(projectile, firePosition.position, Quaternion.identity);

            // Check if player is facing right
            bool isFacingRight = (playerObject.transform.localScale.x > 0);

            // If not facing right, flip the fireball's scale
            if (!isFacingRight)
            {
                Vector3 newScale = newProjectile.transform.localScale;
                newScale.x *= -1; // Flip horizontally
                newProjectile.transform.localScale = newScale;
            }
        }
    }
}
