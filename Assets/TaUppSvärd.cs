using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaUppSvärd : MonoBehaviour
{
    [SerializeField] AudioSource audiosource;
    [SerializeField] AudioClip clip;
    [SerializeField] SpriteRenderer sprite;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // dfofkodo
            MainAttackScript AttackScript = other.GetComponent<MainAttackScript>();

            AttackScript.enabled = true;
            audiosource.PlayOneShot(clip);
            sprite.enabled = false;
            Destroy(gameObject, 0.5F);
        }
    }
}
