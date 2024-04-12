using UnityEngine;

public class FireballProjectile : MonoBehaviour
{
    new private Rigidbody2D rigidbody;
    public float projectileSpeed = 5f;

    [SerializeField] int attackDamage = 1;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        Vector3 fireballScale = transform.localScale;
        Vector2 fireballDirection = Vector2.right;

        if (fireballScale.x < 0)
        {
            fireballDirection = Vector2.left;
        }

        rigidbody.velocity = fireballDirection * projectileSpeed;

        Destroy(gameObject, 60f);
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            BossHealth bossHealth = collision.gameObject.GetComponent<BossHealth>();
            if (bossHealth != null)
            {
                bossHealth.TakeDamage(attackDamage);

            }
            Destroy(gameObject);
        }
    }
}
