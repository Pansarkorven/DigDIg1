using UnityEngine;
using System.Collections;
public class BossMeleeAttack : MonoBehaviour
{
    [SerializeField] int attackDamage = 1;
    [SerializeField] float attackRange = 2f;
    [SerializeField] LayerMask playerLayer;
    public float attackCooldown = 2f;
    public float chargeTime = 1.5f;
    [SerializeField] Collider2D normalAttackTrigger; // Reference to the pre-existing trigger collider for normal attack
    [SerializeField] Collider2D upAttackTrigger; // Reference to the pre-existing trigger collider for up attack
    [SerializeField] Animator Anim;

    bool canAttack = true;
    bool isCharging = false;

    BossFollowPlayer bossMove;
    Transform player;

    void Awake()
    {
        bossMove = GetComponent<BossFollowPlayer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Start()
    {
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
        // If the player is within range for a normal attack, start charging
        if (normalAttackTrigger.IsTouchingLayers(playerLayer))
        {
            StartCharge(normalAttackTrigger.transform);
        }
        // If the player is within range for an attack up, start charging
        else if (upAttackTrigger.IsTouchingLayers(playerLayer))
        {
            StartCharge(upAttackTrigger.transform);
        }
    }

    void StartCharge(Transform attackPoint)
    {
        bossMove.StopMoving();
        Anim.SetTrigger("BossAttacking");
        isCharging = true;

        StartCoroutine(Attack());

        canAttack = false;
    }

    IEnumerator Attack()
    {
        Anim.SetTrigger("BossAttacking");

       
        yield return new WaitForSeconds(1);

  
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(normalAttackTrigger.transform.position, attackRange, playerLayer);

       
        if (hitPlayers.Length > 0)
        {
            foreach (Collider2D playerCollider in hitPlayers)
            {
                if (playerCollider.IsTouching(normalAttackTrigger))
                {
                    playerCollider.GetComponent<Health>().TakeDamage(attackDamage);
                    Debug.Log(playerCollider + " is hit!");
                }
            }
        }
        yield return new WaitForSeconds(1);

        // Resume boss movement
        bossMove.StartMoving();

        // Reset attack cooldown
        StartCoroutine(ResetAttackCooldown());
        isCharging = false;
    }

    IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}