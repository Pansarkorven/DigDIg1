using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] AudioSource audiosource;
    [SerializeField] AudioClip clip;
    [SerializeField] SpriteRenderer sprite; 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // har den inventory kopplat
            Inventory PlayerInventory = other.GetComponent<Inventory>();

            if (PlayerInventory != null)
            {
                PlayerInventory.AddKey();
                audiosource.PlayOneShot(clip);
                sprite.enabled = false;
                Destroy(gameObject, 1.14f);

            }
        }
    }
}
