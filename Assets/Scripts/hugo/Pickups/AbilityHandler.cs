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
            inventory.RemoveKey();
        }
    }

}
