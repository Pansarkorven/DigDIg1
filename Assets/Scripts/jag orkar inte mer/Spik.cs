using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spik : MonoBehaviour
{
    private int damageCount = 0;
    public int maxDamageCount = 3; // Set the maximum number of allowed damages
    public float damageDelay = 0.2f; // Set the delay between each damage

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(DamageWithDelay(collision.gameObject.GetComponent<Health>()));
        }
    }

    IEnumerator DamageWithDelay(Health healthComponent)
    {
        while (damageCount < maxDamageCount)
        {
            healthComponent.TakeDamage(1);
            damageCount++; // Increment damage count
            yield return new WaitForSeconds(damageDelay); // Wait for the specified delay
        }

        // If the maximum damage count is reached, destroy the Spik object
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
