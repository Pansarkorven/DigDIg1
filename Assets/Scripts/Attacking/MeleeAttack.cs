using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public float attackRange = 0.5f;
    public int attackDamage = 1;

    public LayerMask enemyLayer;

    public Transform attackPoint;
    public Transform attackPointUp;

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
        if (Input.GetKeyDown(KeyCode.W))
        {
            PreformAttackUp();
        }
    }

    void PerformAttack()
    {

        // Perform the melee attack based on the character's direction
        if (IsFacingRight())
        {
            AttackSide();
            // Trigger the attack animation
            animator.SetTrigger("Attack");
        }
        else
        {
            AttackSide(); // You can replace this with a different animation for attacking to the left
            animator.SetTrigger("Attack");
        }

        // Perform the melee attack upwards
        if (Input.GetKeyDown(KeyCode.W))
        {
            AttackUp();
            animator.SetTrigger("AttackUp");
        }
    }
    void PreformAttackUp()
    {
        AttackUp();
        animator.SetTrigger("AttackUp");
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
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointUp.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
        }
        Debug.Log("slår up");
    }

    bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        if (attackPointUp == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        Gizmos.DrawWireSphere(attackPointUp.position, attackRange);
    }
}
