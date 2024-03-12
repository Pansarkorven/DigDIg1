using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballAttack : MonoBehaviour
{
    public Transform firePosition;
    public GameObject projectile;
    public GameObject playerObject; // Referens till spelarens GameObject
    private Inventory inventory; // referens till lagringen
    private float lastAttackTime; // variabel f�r att lagra tiden f�r det senaste anfallet
    public float attackCooldown = 5f; // 5 sek cooldown

    void Start()
    {
        // F� spelarens lagring
        inventory = playerObject.GetComponent<Inventory>();
        if (inventory == null)
        {
            Debug.LogError("INventory hittades inte p� spelaren");
        }
        // Sista tiden du attackerade
        lastAttackTime = -attackCooldown; // Starta med en nedkylning s� spelaren kan attackera omedelbart vid spelets start
    }

    void Update()
    {
        
        if (Time.time - lastAttackTime >= attackCooldown && inventory != null && inventory.HasRanged() && Input.GetMouseButtonDown(0))
        {
            // Skapa projektilen
            GameObject newProjectile = Instantiate(projectile, firePosition.position, Quaternion.identity);

            // Kontrollera om spelaren �r v�nd �t h�gerr
            bool isFacingRight = (playerObject.transform.localScale.x > 0);

            // Om inte �t h�ger s� v�nder den eldbollens skala s� den ser ut som att den siktar �t det h�llet, detta systemet suger balle men kenneth gjorde movement systemet s� d�r s� ingenting v�nder n�r spelaren v�nder s� jag m�ste g�ra s� j�vla m�nga saker f�r att fixa det
            if (!isFacingRight)
            {
                Vector3 newScale = newProjectile.transform.localScale;
                newScale.x *= -1; // V�nd horisontellt
                newProjectile.transform.localScale = newScale;
            }

            // Uppdatera sista attack tiden
            lastAttackTime = Time.time;
        }
    }
}
