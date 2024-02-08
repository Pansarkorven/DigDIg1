using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public Transform attackPoint;
    public LayerMask enemyLayer;
    public float attackRange = 0.5f;
    public int attackDamage = 1;

    public Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            PerformAttack();
        }
    }

    void PerformAttack()
    {
        // Trigger the attack animation
        animator.SetTrigger("Attack");

        // Perform the melee attack based on the character's direction
        if (IsFacingRight())
        {
            AttackSide();
        }
        else
        {
            AttackSide(); // You can replace this with a different animation for attacking to the left
        }

        // Perform the melee attack upwards
        AttackUp();
    }

    void AttackSide()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
        }
    }

    void AttackUp()
    {
        // Implement attacking upwards logic here
        // You can use OverlapCircle or OverlapBox to detect enemies above the character
    }

    bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
