using UnityEngine;

public class AIShooter : MonoBehaviour
{
    public Transform target; // The target to aim at (player, for example)
    public GameObject projectilePrefab;
    public float shootingRange = 5f;
    public float shootingInterval = 1f; // Time between shots
    public Transform shootPoint; // Point from where the projectile will be shot

    private float lastShootTime;

    void Start()
    {
        lastShootTime = Time.time;
    }

    void Update()
    {
        if (target != null && Vector3.Distance(transform.position, target.position) <= shootingRange)
        {
            // Aim at the target
            Vector2 direction = (target.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            // Check if it's time to shoot
            if (Time.time - lastShootTime >= shootingInterval)
            {
                Shoot();
                lastShootTime = Time.time;
            }
        }
    }

    void Shoot()
    {
        // Instantiate a projectile at the shoot point
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
        // Set the projectile's direction
       // projectile.GetComponent<Projectile>().SetDirection((target.position - shootPoint.position).normalized);
    }
}