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
    [SerializeField] private GameObject HealthSlider;
    [SerializeField] private GameObject HealthText;

    [SerializeField] float rageThresholdPercentage = 0.3f; // Rage mode triggers when health drops below this percentage

    public Slider healthSlider; // Reference to the Slider UI component
    public TextMeshProUGUI healthText; // Reference to the TextMeshPro text component

    public bool isRageMode = false; // Flag to track if the boss is in rage mode

    void Start()
    {
        BossMeleeAttack = GetComponent<BossMeleeAttack>();
        currentHealth = maxHealth;
        UpdateHealthUI();
        HealthSlider.SetActive(true);
        HealthText.SetActive(true);
    }

    public void TakeDamage(int damage)
    {
        // Reduce the boss's health by the amount of damage taken
        currentHealth -= damage;

        // Ensure the health doesn't go below zero
        currentHealth = Mathf.Max(currentHealth, 0);

        // Invoke the event when the boss's health changes
        UpdateHealthUI();

        // Check if the boss has been defeated
        if (currentHealth <= 0)
        {
            Die();
        }
        else if (!isRageMode && (float)currentHealth / maxHealth <= rageThresholdPercentage)
        {
            EnterRageMode(); // Trigger rage mode when health drops below the threshold
        }
    }

    void Die()
    {
        // Optional: Implement death behavior here (e.g., play death animation, trigger level completion, etc.)
        Destroy(gameObject);

        DoorPrefab.SetActive(false);
        HealthSlider.SetActive(false);
        HealthText.SetActive(false);

        if (DashPrefab != null)
        {
            DashPrefab.SetActive(true);
        }
    }

    void EnterRageMode()
    {
        // Optional: Implement rage mode behavior here (e.g., increase attack power, change behavior, etc.)
        isRageMode = true;
        BossMeleeAttack.attackCooldown = 0.9f;
        BossMeleeAttack.chargeTime = 0.9f;

        


    }

    void UpdateHealthUI()
    {
        // Update the health bar UI
        healthSlider.value = currentHealth;
        healthText.text = "Health: " + currentHealth.ToString();
    }

    // Function to take damage from external sources
    public void TakeDamageFromExternalSource(int damage)
    {
        TakeDamage(damage);
    }
}
