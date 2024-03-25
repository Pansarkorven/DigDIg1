using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        // Reduce the boss's health by the amount of damage taken
        currentHealth -= damage;

        // Ensure the health doesn't go below zero
        currentHealth = Mathf.Max(currentHealth, 0);

     

        // Check if the boss has been defeated
        if (currentHealth <= 0)
        {
            Die();
        }
      
    }

    void Die()
    {
        // Perform death actions here
        Destroy(gameObject);
        Debug.Log("jag dog why");
    }
}
