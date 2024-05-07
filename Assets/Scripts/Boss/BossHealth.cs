using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;
    [SerializeField] int currentHealth;
    public BossMeleeAttack BossMeleeAttack;
    [SerializeField] private GameObject DoorPrefab;
    [SerializeField] private GameObject DashPrefab;
    [SerializeField] private GameObject ExitPrefab;
    [SerializeField] private GameObject BossHealthBar;
    [SerializeField] float rageThresholdPercentage = 0.3f;
    [SerializeField] Animator AnimBoss;

    public bool isRageMode = false;

    void Start()
    {
        BossMeleeAttack = GetComponent<BossMeleeAttack>();
        currentHealth = maxHealth;
        UpdateHealthUI();
        BossHealthBar.SetActive(true);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        currentHealth = Mathf.Max(currentHealth, 0);

        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
        else if (!isRageMode && (float)currentHealth / maxHealth <= rageThresholdPercentage)
        {
            EnterRageMode();
        }
    }

    void Die()
    {

        Destroy(gameObject);

        DoorPrefab.SetActive(false);
        ExitPrefab.SetActive(true);
        BossHealthBar.SetActive(false);

        if (DashPrefab != null)
        {
            DashPrefab.SetActive(true);
        }

        if (ExitPrefab != null)
        {
            ExitPrefab.SetActive(false);
        }


    }

    void EnterRageMode()
    {
        isRageMode = true;
        BossMeleeAttack.attackCooldown = 0.9f;
        BossMeleeAttack.chargeTime = 1.5f;
    }

    void UpdateHealthUI()
    {
        
        //just nu inget
    }

    public void TakeDamageFromExternalSource(int damage)
    {
        TakeDamage(damage);
    }
}
