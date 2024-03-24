using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisonIgnor : MonoBehaviour
{
    [SerializeField] private string tagToIgnore = "Enemy"; // Tag of objects to ignore collisions with
    [SerializeField] private LayerMask layerToIgnore; // Layer of objects to ignore collisions with
    [SerializeField] private Collider2D thisCollider; // Collider of the current object

    private void Start()
    {
        // Get the collider of the current object
        if (thisCollider == null)
        {
            thisCollider = GetComponent<Collider2D>();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with an object with the specified tag
        if (collision.gameObject.CompareTag(tagToIgnore))
        {
            // Ignore collision with the object
            Physics2D.IgnoreCollision(thisCollider, collision.collider);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collider belongs to the specified layer
        if (((1 << other.gameObject.layer) & layerToIgnore.value) != 0)
        {
            // Ignore collision with the object
            Physics2D.IgnoreCollision(thisCollider, other);
        }
    }
}
