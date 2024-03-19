using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRoom : MonoBehaviour
{
    public Transform camTarget;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") Camera.main.transform.position = transform.position + new Vector3(0, 0, -10);
    }
}
