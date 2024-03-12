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

    BossFollowPlayer bossMove;

    private void Start()
    {
        bossMove = GetComponent<BossFollowPlayer>();
    }

    private void Update() // Called every single frame
    {
        
    }

    void FixedUpdate() // Called every physics update, every 0.02 second
    {
        if (canAttack)
        {
            StartCharge();
        }
    }

    void StartCharge()
    {
        bossMove.StopMoving();

        // You might want to play a charging animation here

        // Invoke the actual attack after the charging duration
        Invoke(nameof(Attack), chargeTime);

        // Set canAttack to false only after invoking the Attack method
        canAttack = false;
        Debug.Log("Start Swinging");
    }

    void Attack()
    {
        // Resume boss movement when the attack is executed
        bossMove.StartMoving();

        // Perform the attack logic
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(transform.position, attackRange, playerLayer);

        foreach (Collider2D player in hitPlayers)
        {
            player.GetComponent<Health>().TakeDamage(attackDamage);
            Debug.Log(player + " is hit!");
            // You might want to play an attack animation or perform other actions here
        }

        // Start the cooldown before the boss can attack again
        Invoke("ResetAttackCooldown", attackCooldown);
        Debug.Log("Swing Sword");
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