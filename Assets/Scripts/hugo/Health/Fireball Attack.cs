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
            // Om inte åt höger så vänder den eldbollens skala så den ser ut som att den siktar åt det hållet, detta systemet suger balle men kenneth gjorde movement systemet så där så ingenting vänder när spelaren vänder så jag måste göra så jävla många saker för att fixa det
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
