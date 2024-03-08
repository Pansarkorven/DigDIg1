using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMeleeAttack : MonoBehaviour
{
    public int attackDamage = 1;
    public float attackRange = 2f;
    public LayerMask playerLayer;
    public float attackCooldown = 2f;
    public float chargeTime = 1.5f; // Adjust this value for the charging duration

    private bool canAttack = true;

    void Update()
    {
        if (canAttack)
        {
            StartCharge();
        }
    }

    void StartCharge()
    {
        
        // You might want to play a charging animation here

        // Invoke the actual attack after the charging duration
        Invoke("Attack", chargeTime);
        Attack();
        canAttack = false;
        Debug.Log("IStartSwing");
    }

    void Attack()
    {
        // Resume moving or perform the attack logic here

        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(transform.position, attackRange, playerLayer);

        foreach (Collider2D player in hitPlayers)
        {
            player.GetComponent<Health>().TakeDamage(attackDamage);
            // You might want to play an attack animation or perform other actions here
        }

        // Start the cooldown before the boss can attack again
        Invoke("ResetAttackCooldown", attackCooldown);
        Debug.Log("I Swung Sword");
    }

    void ResetAttackCooldown()
    {
        canAttack = true;
    }

    void OnDrawGizmosSelected()
    {
        // Visualize the attack range in the scene view
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}