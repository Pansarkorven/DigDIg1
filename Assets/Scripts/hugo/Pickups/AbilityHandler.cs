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

    public void UseDash()
    {
        if (inventory.HasDash())
        {
            // dash kod g� h�r
        }
    }
}
