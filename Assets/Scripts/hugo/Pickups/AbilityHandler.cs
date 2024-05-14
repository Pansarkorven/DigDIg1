using UnityEngine;

public class AbilityHandler : MonoBehaviour
{
    private Inventory inventory;

    private void Start()
    {
        inventory = GetComponent<Inventory>();
    }

    public void UseKey()
    {
        if (inventory.HasKey())
        {
            Debug.Log("Anv�nder nyckeln gggggggggggggg");
            inventory.RemoveKey();
        }
        else
        {
            Debug.Log("Du har inte nycklen");
        }
    }

}
