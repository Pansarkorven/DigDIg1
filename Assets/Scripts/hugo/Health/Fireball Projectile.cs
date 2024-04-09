using UnityEngine;

public class FireballProjectile : MonoBehaviour
{
    new private Rigidbody2D rigidbody;
    public float projectileSpeed = 5f;
  //  public enemy enemy = null;
    [SerializeField] int attackDamage = 1;

    void Start()
    {
  //      enemy = GetComponent<EnemyHealth>();
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


 //   public void OnCollisionEnter2D(Collision2D collision)
  //  {
    //    if (collision.gameObject.CompareTag("Enemy"))
     //   {
           // enemy.GetComponent<BossHealth>().TakeDamage(attackDamage);
       // }
   // }
}
