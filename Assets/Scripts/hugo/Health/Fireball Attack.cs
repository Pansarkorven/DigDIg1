using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballAttack : MonoBehaviour
{
    [SerializeField] Transform firePosition;
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject playerObject;
    [SerializeField] Inventory inventory;
    public BossHealth bossHealth;
    [SerializeField] float lastAttackTime;
    [SerializeField] float attackCooldown = 5f;
    [SerializeField] AudioClip attackSound;
    [SerializeField] AudioSource audioSource;

    void Start()
    {
        bossHealth = GetComponent<BossHealth>();
        inventory = playerObject.GetComponent<Inventory>();
        lastAttackTime = -attackCooldown;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Time.time - lastAttackTime >= attackCooldown && inventory != null && inventory.HasRanged() && Input.GetMouseButtonDown(0))
        {

            PlayAttackSound();

            GameObject newProjectile = Instantiate(projectile, firePosition.position, Quaternion.identity);
            bool isFacingRight = (playerObject.transform.localScale.x > 0);
            if (!isFacingRight)
            {
                Vector3 newScale = newProjectile.transform.localScale;
                newScale.x *= -1;
                newProjectile.transform.localScale = newScale;
            }
            lastAttackTime = Time.time;
        }
    }

    void PlayAttackSound()
    {
        if (audioSource != null && attackSound != null)
        {
            audioSource.PlayOneShot(attackSound);
        }
    }
}
