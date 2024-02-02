using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool hasKey = false;

    public void AddKey()
    {
        hasKey = true;
        Debug.Log("barn mongo 23");
    }

    public bool HasKey()
    {
        return hasKey;
    }

    public void RemoveKey()
    {
        hasKey = false;
    }
}
