using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballAttack : MonoBehaviour
{
    [SerializeField] Transform firePosition;
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject playerObject;
    [SerializeField] Inventory inventory;
    public BossHealth bossHealth;
    [SerializeField] float lastAttackTime;
    [SerializeField] float attackCooldown = 5f;

    void Start()
    {
        bossHealth = GetComponent<BossHealth>();
        inventory = playerObject.GetComponent<Inventory>();
        lastAttackTime = -attackCooldown;

    }

    void Update()
    {

        if (Time.time - lastAttackTime >= attackCooldown && inventory != null && inventory.HasRanged() && Input.GetMouseButtonDown(0))
        {
            GameObject newProjectile = Instantiate(projectile, firePosition.position, Quaternion.identity);
            bool isFacingRight = (playerObject.transform.localScale.x > 0);
            // If not facing right, flip the projectile's scale to aim in the opposite direction
            if (!isFacingRight)
            {
                Vector3 newScale = newProjectile.transform.localScale;
                newScale.x *= -1;
                newProjectile.transform.localScale = newScale;
            }
            lastAttackTime = Time.time;
        }
    }
}