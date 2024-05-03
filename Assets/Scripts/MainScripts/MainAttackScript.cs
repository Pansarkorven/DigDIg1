using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MainAttackScript : MonoBehaviour
{
    [SerializeField] float attackRange = 0.5f;
    [SerializeField] int attackDamage = 1;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] LayerMask WallLayer;
    [SerializeField] Transform attackPoint;
    [SerializeField] Transform attackPointUp;
    [SerializeField] Animator animator;
    [SerializeField] float WhereMouse;
    [SerializeField] float WhenAttackUp = 6;
    [SerializeField] Transform PlayerTransform;
    [SerializeField] float PlayerPosition;
    [SerializeField] MainCharacterController characterController;
    [SerializeField] AudioClip[] whooshSound;
    [SerializeField] AudioClip[] hitSound;
    [SerializeField] AudioSource audioSource;


    bool AttackUpCheck = false;
    [SerializeField] public bool isAttacking = false;
    [SerializeField] float attackCooldown = 0.5f;

    void Start()
    {
        PlayerTransform = transform;
        animator = GetComponent<Animator>();
       characterController = GetComponent<MainCharacterController>();

    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !isAttacking && AttackUpCheck == true)
        {
            StartCoroutine(PerformAttackUp());
        }
        if (Input.GetButtonDown("Fire1") && !isAttacking && AttackUpCheck == false)
        {
            StartCoroutine(PerformAttack());
        }
        Vector2 MouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 playerPosition = PlayerTransform.position;
        WhereMouse = MouseWorldPos.y - PlayerPosition;
        PlayerPosition = playerPosition.y;
        
        if (WhereMouse > WhenAttackUp) 
        {
            AttackUpCheck = true;
        }
        else 
        {
            AttackUpCheck = false;
        }
        if (isAttacking == true)
        {
            characterController.StopMove();
            
        }
        else
        {
            characterController.AllowsMove();
        }

    }

    IEnumerator PerformAttack()
    {
        isAttacking = true;
        SpelaSlumpWhooshLjud();

        if (IsFacingRight())
        {
            AttackSide();
            animator.SetTrigger("Attack");
        }
        else
        {
            AttackSide(); 
            animator.SetTrigger("Attack");
        }

        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;
    }

    IEnumerator PerformAttackUp()
    {
        isAttacking = true;
        SpelaSlumpWhooshLjud();

        AttackUp();
        animator.SetTrigger("AttackUp");
        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;
    }

    void AttackSide()
    {
        LayerMask DamageLayers = enemyLayer | WallLayer;
        Collider2D[] HitStuff = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, DamageLayers);

        foreach (Collider2D enemy in HitStuff)
        {
            if (enemy != null)
            {
                BossHealth bossHealth = enemy.GetComponent<BossHealth>();
                if (bossHealth != null)
                {
                    SpelaSlumpLjud();
                    bossHealth.TakeDamage(attackDamage);
                }

                BreakableWall breakableWall = enemy.GetComponent<BreakableWall>();
                if (breakableWall != null)
                {
                    SpelaSlumpLjud();
                    breakableWall.TakeDamage(attackDamage);
                }
            }
        }
    }

    void SpelaSlumpLjud()
    {
        if (audioSource != null && hitSound != null && hitSound.Length > 0)
        {
            if (!audioSource.isPlaying) 
            { 
                int randomIndex = Random.Range(0, hitSound.Length);
                audioSource.clip = hitSound[randomIndex];  
                audioSource.Play();
            }
        }
    }

    void SpelaSlumpWhooshLjud()
    {
        if (audioSource != null && whooshSound != null && whooshSound.Length > 0)
        {
            if (!audioSource.isPlaying)
            {
                int randomIndex = Random.Range(0, whooshSound.Length);
                audioSource.clip = whooshSound[randomIndex];
                audioSource.Play();
            }
        }
    }

    void AttackUp()
    {
        LayerMask DamageLayers = enemyLayer | WallLayer;
        Collider2D[] HitStuff = Physics2D.OverlapCircleAll(attackPointUp.position, attackRange, DamageLayers);

        foreach (Collider2D enemy in HitStuff)
        {
            if (enemy != null)
            {
                SpelaSlumpLjud();
                BossHealth bossHealth = enemy.GetComponent<BossHealth>();
                if (bossHealth != null)
                {
                    bossHealth.TakeDamage(attackDamage);
                }

                BreakableWall breakableWall = enemy.GetComponent<BreakableWall>();
                if (breakableWall != null)
                {
                    breakableWall.TakeDamage(attackDamage);
                }
            }
        }
        Debug.Log("slår up");
        
    }

    private IEnumerator StartTimer(float MoveAgain)
    {
        yield return new WaitForSeconds(MoveAgain);

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
