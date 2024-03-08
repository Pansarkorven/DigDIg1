using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballProjectile : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    public float projectilespeed;
    private GameObject playerObject;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            bool isFacingRight = (playerObject.transform.localScale.x > 0);

            if (isFacingRight)
            {
                rigidbody.velocity = transform.right * projectilespeed;
            }
            else
            {
                rigidbody.velocity = -transform.right * projectilespeed;
            }
        }
        else
        {
            Debug.LogError("Spelarobjektet kunde inte hittas.");
        }
    }
}