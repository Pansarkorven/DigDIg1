using UnityEngine;

public class Key : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // har den inventory kopplat
            Inventory PlayerInventory = other.GetComponent<Inventory>();

            if (PlayerInventory != null)
            {
                PlayerInventory.AddKey(); // l�gg nyckeln i inventory (value)
                Destroy(gameObject); // spr�ng objektet
            }
        }
    }
}
