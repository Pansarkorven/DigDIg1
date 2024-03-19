using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public float rageThresholdPercentage = 0.3f; // Rage mode triggers when health drops below this percentage

    // Optional: You can define events here to trigger when the boss's health changes or reaches certain thresholds
    public delegate void BossHealthChanged(int currentHealth, int maxHealth);
    public event BossHealthChanged OnBossHealthChanged;

    private bool isRageMode = false; // Flag to track if the boss is in rage mode

    void Start()
    {
        currentHealth = maxHealth;
        // Invoke the event when the boss's health changes
        InvokeBossHealthChanged();
    }

    public void TakeDamage(int damage)
    {
        // Reduce the boss's health by the amount of damage taken
        currentHealth -= damage;

        // Ensure the health doesn't go below zero
        currentHealth = Mathf.Max(currentHealth, 0);

        // Invoke the event when the boss's health changes
        InvokeBossHealthChanged();

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
    }

    void EnterRageMode()
    {
        // Optional: Implement rage mode behavior here (e.g., increase attack power, change behavior, etc.)
        isRageMode = true;
        Debug.Log("Entering Rage Mode!");
    }

    // Method to invoke the OnBossHealthChanged event
    void InvokeBossHealthChanged()
    {
        OnBossHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    // Function to take damage from external sources
    public void TakeDamageFromExternalSource(int damage)
    {
        TakeDamage(damage);
    }
}
