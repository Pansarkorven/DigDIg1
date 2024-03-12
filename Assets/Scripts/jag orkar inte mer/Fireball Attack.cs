using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballAttack : MonoBehaviour
{
    public Transform firePosition;
    public GameObject projectile;
    public GameObject playerObject; // Referens till spelarens GameObject
    private Inventory inventory; // referens till lagringen
    private float lastAttackTime; // variabel för att lagra tiden för det senaste anfallet
    public float attackCooldown = 5f; // 5 sek cooldown

    void Start()
    {
        // Få spelarens lagring
        inventory = playerObject.GetComponent<Inventory>();
        if (inventory == null)
        {
            Debug.LogError("INventory hittades inte på spelaren");
        }
        // Sista tiden du attackerade
        lastAttackTime = -attackCooldown; // Starta med en nedkylning så spelaren kan attackera omedelbart vid spelets start
    }

    void Update()
    {
        
        if (Time.time - lastAttackTime >= attackCooldown && inventory != null && inventory.HasRanged() && Input.GetMouseButtonDown(0))
        {
            // Skapa projektilen
            GameObject newProjectile = Instantiate(projectile, firePosition.position, Quaternion.identity);

            // Kontrollera om spelaren är vänd åt högerr
            bool isFacingRight = (playerObject.transform.localScale.x > 0);

            // Om inte åt höger så vänder den eldbollens skala så den ser ut som att den siktar åt det hållet, detta systemet suger balle men kenneth gjorde movement systemet så där så ingenting vänder när spelaren vänder så jag måste göra så jävla många saker för att fixa det
            if (!isFacingRight)
            {
                Vector3 newScale = newProjectile.transform.localScale;
                newScale.x *= -1; // Vänd horisontellt
                newProjectile.transform.localScale = newScale;
            }

            // Uppdatera sista attack tiden
            lastAttackTime = Time.time;
        }
    }
}
