using UnityEngine;

public class AIBossRagePhase : MonoBehaviour
{
    public float maxHealth = 100f; // Max health of the boss
    public float rageThreshold = 30f; // Health threshold to enter rage phase
    public float rageSpeedMultiplier = 1.5f; // Speed multiplier during rage phase

    private float currentHealth;
    private bool isInRagePhase = false;
    private float originalMoveSpeed;
    private Rigidbody2D rb; // Assuming the boss has a Rigidbody2D for movement

    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();

        // Assuming the boss has a script controlling its movement speed
        originalMoveSpeed = GetComponent<EnemyPatrol>().moveSpeed;
    }

    void Update()
    {
        if (currentHealth <= rageThreshold && !isInRagePhase)
        {
            EnterRagePhase();
        }
    }

    void EnterRagePhase()
    {
        isInRagePhase = true;
        // Increase speed
        GetComponent<EnemyPatrol>().moveSpeed *= rageSpeedMultiplier;

        // Any other actions you want to take during rage phase
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Your death logic here
        Destroy(gameObject);
    }
}