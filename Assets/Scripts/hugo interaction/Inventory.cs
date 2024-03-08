using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool hasKey = false;
    public bool hasRanged = false;
    public bool hasArmor = false;
    public bool hasDash = false;

    public void AddKey()
    {
        hasKey = true;
    }

    public bool HasKey()
    {
        return hasKey;
    }

    public void RemoveKey()
    {
        hasKey = false;
    }

    public void AddRanged()
    {
        hasRanged = true;
    }

    public bool HasRanged()
    {
        return hasRanged;
    }

    public void AddArmor()
    {
        hasArmor = true;
    }

    public bool HasArmor()
    {
        return hasArmor;
    }

    public void AddDash()
    {
        hasDash = true;
    }

    public bool HasDash()
    {
        return hasDash;
    }
}
