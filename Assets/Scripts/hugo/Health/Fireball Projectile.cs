using UnityEngine;

public class FireballProjectile : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer; 
    public float projectileSpeed = 5f;
    [SerializeField] int attackDamage = 1;
    [SerializeField] AudioClip impactSound;
    [SerializeField] AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        Vector2 fireballDirection = transform.right;
        rb.velocity = fireballDirection * projectileSpeed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            PlayImpactSound();
            if (spriteRenderer != null)
            {
                spriteRenderer.enabled = false;
            }

            rb.velocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Static;
            Destroy(gameObject, 0.1655f);
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            BossHealth bossHealth = collision.gameObject.GetComponent<BossHealth>();
            if (bossHealth != null)
            {
                bossHealth.TakeDamage(attackDamage);
            }
            Destroy(gameObject);
        }
    }

    void PlayImpactSound()
    {
        if (audioSource != null && impactSound != null)
        {
            audioSource.PlayOneShot(impactSound);
        }
    }
}
