using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballProjectile : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    public float projectileSpeed = 5f;
    private GameObject playerObject;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            bool isPlayerFacingRight = (playerObject.transform.localScale.x > 0); // jag hatar det här systemet
            bool isFireballFacingRight = (transform.localScale.x > 0);

            if (isPlayerFacingRight == isFireballFacingRight)
            {
                rigidbody.velocity = transform.right * projectileSpeed;
            }
            else
            {
                rigidbody.velocity = -transform.right * projectileSpeed;
            }
        }
        else
        {
            Debug.LogError("Spelaren hittas inte");
            Destroy(gameObject, 60f);
        }
    }
}
