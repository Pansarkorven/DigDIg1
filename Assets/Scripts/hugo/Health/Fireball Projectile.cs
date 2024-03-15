using UnityEngine;

public class FireballProjectile : MonoBehaviour
{
    new private Rigidbody2D rigidbody;
    public float projectileSpeed = 5f;

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
}
