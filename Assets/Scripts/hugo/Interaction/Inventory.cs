using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool hasKey = false;
    public bool hasRanged = false;
    //public bool hasArmor = false;
    public bool hasDash = false;
    public Health playerHealth;
    public HealthBar playerHealthBar;
    public MainCharacterController controller;

    public void AddKey()
    {
        hasKey = true;
    }

    public void RemoveKey()
    {
        hasKey = false;
    }

    public bool HasKey()
    {
        return hasKey;
    }

    public void AddRanged()
    {
        hasRanged = true;
    }

    public bool HasRanged()
    {
        return hasRanged;
    }

    //public void AddArmor()
    //{
    //    hasArmor = true;
    //    if (playerHealth != null && playerHealthBar != null)
    //    {
    //        playerHealth.MaxHealth = 9;
    //        playerHealth.CurrentHealth = 9;
    //        playerHealthBar.useSpecialHealthBar = true;

    //    } nytt hp system detta fungerar inte l�ngre (viktor varf�r)
    //}

    public void AddDash()
    {
        hasDash = true;
        if (controller != null)
        {
            controller.canDash = true;
        }
    }
}
