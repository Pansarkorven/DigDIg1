using UnityEngine;
using System.Collections;

public class BossMeleeAttack : MonoBehaviour
{
    [SerializeField] int attackDamage = 1;
    [SerializeField] float attackRange = 2f;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] float attackCooldown = 2f;
    [SerializeField] float chargeTime = 1.5f;
    [SerializeField] Transform HitPointLeft;
    [SerializeField] Animator Anim;

    bool canAttack = true;

    BossFollowPlayer bossMove;

    void Start()
    {
        bossMove = GetComponent<BossFollowPlayer>();
        StartCoroutine(AttackRoutine());
    }

    IEnumerator AttackRoutine()
    {
        while (true)
        {
            if (canAttack)
            {
                yield return new WaitForSeconds(attackCooldown);
                StartCharge();
            }
            yield return null;
        }
    }

    void StartCharge()
    {
        bossMove.StopMoving();
        Anim.SetTrigger("BossAttacking");

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
        Collider2D[] hitPlayersRight = Physics2D.OverlapCircleAll(HitPointLeft.position, attackRange, playerLayer);

        foreach (Collider2D player in hitPlayersRight)
        {
            player.GetComponent<Health>().TakeDamage(attackDamage);
            Debug.Log(player + " is hit!");
            // You might want to play an attack animation or perform other actions here
        }

        // Start the cooldown before the boss can attack again
        StartCoroutine(ResetAttackCooldown());
        Debug.Log("Swing Sword");
    }

    IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    void OnDrawGizmosSelected()
    {
        // Visualize the attack range in the scene view
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(HitPointLeft.position, attackRange);
    }
}
