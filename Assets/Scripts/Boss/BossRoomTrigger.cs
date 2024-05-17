using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomTrigger : MonoBehaviour
{
    [SerializeField] private GameObject bossPrefab; 
    [SerializeField] private GameObject DoorPrefab;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (bossPrefab != false)
            {
                bossPrefab.SetActive(true);
            }

            if (DoorPrefab != false)
            { 
                DoorPrefab.SetActive(true);
            }

            gameObject.SetActive(false);
        }
    }
}
