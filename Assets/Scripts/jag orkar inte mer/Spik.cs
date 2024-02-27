using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spik : MonoBehaviour
{
    private int damageCount = 0;
    public int maxDamageCount = 3; // max användingar av spiken

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // om objektet som nuddar har player taggen
        {
            var HealthComponent = collision.gameObject.GetComponent<Health>();
            if (HealthComponent != null)
            {
                HealthComponent.TakeDamage(1);
                damageCount++; // i = i + 1
                if (damageCount >= maxDamageCount)
                {
                    Destroy(gameObject); // lägg vad du vill ska hända
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
