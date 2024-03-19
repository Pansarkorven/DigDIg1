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
    [SerializeField] Transform HitPointUp;
    [SerializeField] Animator Anim;
    [SerializeField] float normalAttackTriggerDistance = 2f; // Distance threshold for triggering normal attack
    [SerializeField] float upAttackTriggerDistance = 3f; // Distance threshold for triggering attack up

    bool canAttack = true;
    bool isCharging = false;

    BossFollowPlayer bossMove;
    Transform player;

    void Start()
    {
        bossMove = GetComponent<BossFollowPlayer>();
        player = GameObject.FindGameObjectWithTag("Player").transform; // Assuming player is tagged as "Player"
        StartCoroutine(AttackRoutine());
    }

    IEnumerator AttackRoutine()
    {
        while (true)
        {
            if (canAttack && !isCharging)
            {
                yield return new WaitForSeconds(attackCooldown);
                CheckAndStartCharge();
            }
            yield return null;
        }
    }

    void CheckAndStartCharge()
    {
        // Calculate the distance between the boss's HitPointLeft and the player
        float distanceToPlayerLeft = Vector2.Distance(HitPointLeft.position, player.position);

        // Calculate the distance between the boss's HitPointUp and the player
        float distanceToPlayerUp = Vector2.Distance(HitPointUp.position, player.position);

        // If the player is within range for a normal attack, start charging
        if (distanceToPlayerLeft < normalAttackTriggerDistance)
        {
            StartCharge(HitPointLeft);
        }
        // If the player is within range for an attack up, start charging
        else if (distanceToPlayerUp < upAttackTriggerDistance)
        {
            StartCharge(HitPointUp);
            Debug.Log("attackingUp");
        }
    }

    void StartCharge(Transform attackPoint)
    {
        bossMove.StopMoving();
        Anim.SetTrigger("BossAttacking");
        isCharging = true;

        // Invoke the actual attack after the charging duration
        StartCoroutine(Attack(attackPoint));

        // Set canAttack to false only after invoking the Attack method
        canAttack = false;
        Debug.Log("Start Swinging");
    }

    IEnumerator Attack(Transform attackPoint)
    {
        yield return new WaitForSeconds(chargeTime);

        // Resume boss movement when the attack is executed
        bossMove.StartMoving();

        // Perform the attack logic
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

        foreach (Collider2D player in hitPlayers)
        {
            player.GetComponent<Health>().TakeDamage(attackDamage);
            Debug.Log(player + " is hit!");
            // You might want to play an attack animation or perform other actions here
        }

        // Start the cooldown before the boss can attack again
        StartCoroutine(ResetAttackCooldown());
        Debug.Log("Swing Sword");
        isCharging = false;
    }

    IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    void OnDrawGizmosSelected()
    {
        // Visualize the attack range in the scene view for HitPointLeft
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(HitPointLeft.position, attackRange);

        // Visualize the attack range in the scene view for HitPointUp
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(HitPointUp.position, attackRange);

        //// Visualize the normal attack trigger distance for HitPointLeft
        //Gizmos.color = Color.green;
        //Gizmos.DrawWireSphere(transform.position, normalAttackTriggerDistance);

        //// Visualize the attack up trigger distance for HitPointUp
        //Gizmos.color = Color.yellow;
        //Gizmos.DrawWireSphere(transform.position, upAttackTriggerDistance);
    }
}
