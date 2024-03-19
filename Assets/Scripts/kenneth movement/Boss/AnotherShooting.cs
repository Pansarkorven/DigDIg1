using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AnotherShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public int numBullets = 5;
    public float spreadAngle = 5f; // in degrees
    public float shootInterval = 4f; // Interval between shots in seconds

    private bool canShoot = true;
    void Start()
    {
        StartCoroutine(ShootRoutine());
    }

    IEnumerator ShootRoutine()
    {
        while (true)
        {
            if (canShoot)
            {
                Shoot();
                canShoot = false;
                yield return new WaitForSeconds(shootInterval);
                canShoot = true;
            }
            yield return null;
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }



    Vector2 GetShootDirection()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return (mousePosition - transform.position).normalized;
    }

    void Shoot()
    {
        Vector2 shootDirection = GetShootDirection();

        for (int i = 0; i < numBullets; i++)
        {
            Quaternion rotation = Quaternion.AngleAxis(-spreadAngle / 2f + i * (spreadAngle / (numBullets - 1)), Vector3.forward);
            Vector2 bulletDirection = rotation * shootDirection;
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = bulletDirection * bulletSpeed;
            Destroy(bullet, 5f); // Destroy bullet after 5 seconds (adjust as needed)
        }
    }
}
