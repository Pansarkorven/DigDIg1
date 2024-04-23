using UnityEngine;
using System.Collections;
public class BossMeleeAttack : MonoBehaviour
{
    [SerializeField] int attackDamage = 1;
    [SerializeField] float attackRange = 2f;
    [SerializeField] LayerMask playerLayer;
    public float attackCooldown = 2f;
    public float chargeTime = 1.5f;
    [SerializeField] Collider2D normalAttackTrigger;
    [SerializeField] Collider2D BossHitbox; 
    [SerializeField] Collider2D upAttackTrigger; 
    [SerializeField] Animator Anim;
    [SerializeField] float DashStartDelay;
    [SerializeField] float DashDuration;

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
        StartCoroutine(RepeatedDashCoroutine());
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
    IEnumerator RepeatedDashCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(30f); // Wait for 30 seconds
            StartDash();
        }
    }

    void CheckAndStartCharge()
    {
        if (normalAttackTrigger.IsTouchingLayers(playerLayer))
        {
            StartCharge(normalAttackTrigger.transform);
        }
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
    void StartDash()
    {
        Anim.SetTrigger("DashAttack");

        bossMove.StopMoving();

        canAttack = false;

        BossHitbox.isTrigger = true;

        StartCoroutine(DashCollisionCheck());
        Debug.Log("StartDashing");
    }

    IEnumerator DashCollisionCheck()
    {
        yield return new WaitForSeconds(DashStartDelay);
        Debug.Log("Now look if can hit");

        Collider2D[] HitPlayers = Physics2D.OverlapCircleAll(BossHitbox.transform.position, playerLayer);

        if (HitPlayers.Length > 0)
        {
            Debug.Log("CheckCLose");
            foreach (Collider2D playerCollider in HitPlayers)
            {
                if (playerCollider.IsTouching(BossHitbox))
                {
                    Debug.Log("I tutch");
                    Health playerHealth = playerCollider.GetComponent<Health>();
                    if (playerHealth != null)
                    {
                        playerHealth.TakeDamage(attackDamage);
                        Debug.Log(playerCollider + " is hit by dash");
                    }
                }
            }
        }
        yield return new WaitForSeconds(DashDuration);

        bossMove.StartMoving();

        canAttack = true;

        BossHitbox.isTrigger = false;
        Debug.Log("Now Stop Dash");
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

        bossMove.StartMoving();

        StartCoroutine(ResetAttackCooldown());
        isCharging = false;
    }

    IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}