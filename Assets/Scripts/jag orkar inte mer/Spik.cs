using System.Collections;
using UnityEngine;

public class Spik : MonoBehaviour
{
    private int damageCount = 0;
    public int maxDamageCount = 3; // Max uses of the spike
    private bool isPlayerTouching = false;
    private Health healthComponent;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DamageCoroutine());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerTouching = true;
            healthComponent = collision.gameObject.GetComponent<Health>(); // Get the Health component
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerTouching = false;
        }
    }

    IEnumerator DamageCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.4f); // Wait for X amount of sekundre
            if (isPlayerTouching && healthComponent != null)
            {
                healthComponent.TakeDamage(1); // Apply damage if player is still touching
                damageCount++;
                if (damageCount >= maxDamageCount)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
