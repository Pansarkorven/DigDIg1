using UnityEngine;

public class RoomBasedCameraAdjustment : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        mainCamera = GetComponent<Camera>();
        AdjustCameraSize();
    }

    void AdjustCameraSize()
    {
        // Replace this with your room detection logic
        // For demonstration purposes, let's assume we have a variable "roomSize" representing the size of the room
        float roomSize = 10f;

        // Adjust camera size based on room size
        mainCamera.orthographicSize = roomSize / 2f;
    }
}