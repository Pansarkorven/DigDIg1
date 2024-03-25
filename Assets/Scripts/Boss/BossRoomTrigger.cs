using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomTrigger : MonoBehaviour
{
    [SerializeField] private GameObject bossPrefab; // Reference to the boss prefab
    [SerializeField] private GameObject DoorPrefab;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Activate the boss prefab when the player enters the trigger
            if (bossPrefab != false)
            {
                bossPrefab.SetActive(true);
            }

            if (DoorPrefab != false)
            { 
                DoorPrefab.SetActive(true);
            }

            // Perform any additional actions here, such as playing a cutscene, showing UI, etc.

            // Disable the trigger after activating the boss (optional)
            gameObject.SetActive(false);
        }
    }
}
