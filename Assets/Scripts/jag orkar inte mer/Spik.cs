using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spik : MonoBehaviour
{
    private int damageCount = 0;
    public int maxDamageCount = 3; // Set the maximum number of allowed damages

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var HealthComponent = collision.gameObject.GetComponent<Health>();
            if (HealthComponent != null)
            {
                HealthComponent.TakeDamage(1);
                damageCount++; // Increment damage count
                if (damageCount >= maxDamageCount)
                {
                    Destroy(gameObject); // Destroy the Spik object if damage count reaches the limit
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
