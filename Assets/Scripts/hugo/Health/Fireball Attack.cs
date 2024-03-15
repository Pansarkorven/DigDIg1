using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballAttack : MonoBehaviour
{
    public Transform firePosition;
    public GameObject projectile;
    public GameObject playerObject;
    private Inventory inventory;
    private float lastAttackTime;
    public float attackCooldown = 5f;

    void Start()
    {
        inventory = playerObject.GetComponent<Inventory>();
        lastAttackTime = -attackCooldown; 
    }

    void Update()
    {
        
        if (Time.time - lastAttackTime >= attackCooldown && inventory != null && inventory.HasRanged() && Input.GetMouseButtonDown(0))
        {
            GameObject newProjectile = Instantiate(projectile, firePosition.position, Quaternion.identity);
            bool isFacingRight = (playerObject.transform.localScale.x > 0);
            // Om inte �t h�ger s� v�nder den eldbollens skala s� den ser ut som att den siktar �t det h�llet, detta systemet suger balle men kenneth gjorde movement systemet s� d�r s� ingenting v�nder n�r spelaren v�nder s� jag m�ste g�ra s� j�vla m�nga saker f�r att fixa det
            if (!isFacingRight)
            {
                Vector3 newScale = newProjectile.transform.localScale;
                newScale.x *= -1;
                newProjectile.transform.localScale = newScale;
            }
            lastAttackTime = Time.time;
        }
    }
}
