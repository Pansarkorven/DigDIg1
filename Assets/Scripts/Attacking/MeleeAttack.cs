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

    private bool isAttacking = false;
    public float attackCooldown = 0.5f; // Adjust as needed

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !isAttacking)
        {
            StartCoroutine(PerformAttack());
        }
        if (Input.GetKeyDown(KeyCode.W) && !isAttacking)
        {
            StartCoroutine(PerformAttackUp());
        }
    }

    IEnumerator PerformAttack()
    {
        isAttacking = true;

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

        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;
    }

    IEnumerator PerformAttackUp()
    {
        isAttacking = true;
        AttackUp();
        animator.SetTrigger("AttackUp");
        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;
    }

    void AttackSide()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<BossHealth>().TakeDamage(attackDamage);
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
        if (attackPoint == null || attackPointUp == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        Gizmos.DrawWireSphere(attackPointUp.position, attackRange);
    }
}
