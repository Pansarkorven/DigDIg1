using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnlyOneWayPlatfroms : MonoBehaviour
{
    private GameObject currentOneWayPlatform;
    private BoxCollider2D playerCollider;

    private void Start()
    {
        playerCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (currentOneWayPlatform != null)
            {
                StartCoroutine(DisableCollision());
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OneWayPlatform"))
        {
            currentOneWayPlatform = collision.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OneWayPlatform"))
        {
            currentOneWayPlatform = null;
        }
    }

    private IEnumerator DisableCollision()
    {
        BoxCollider2D platformCollider = currentOneWayPlatform.GetComponent<BoxCollider2D>();

        // Ignore collisions with the platform
        Physics2D.IgnoreCollision(playerCollider, platformCollider);

        // Ignore collisions with enemies
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            BoxCollider2D enemyCollider = enemy.GetComponent<BoxCollider2D>();
            if (enemyCollider != null)
            {
                Physics2D.IgnoreCollision(playerCollider, enemyCollider);
            }
        }

        // Wait for a short duration
        yield return new WaitForSeconds(0.25f);

        // Re-enable collisions with the platform
        Physics2D.IgnoreCollision(playerCollider, platformCollider, false);

        // Re-enable collisions with enemies
        foreach (GameObject enemy in enemies)
        {
            BoxCollider2D enemyCollider = enemy.GetComponent<BoxCollider2D>();
            if (enemyCollider != null)
            {
                Physics2D.IgnoreCollision(playerCollider, enemyCollider, false);
            }
        }
    }
}
