using UnityEngine;

public class RoomCamera : MonoBehaviour
{
    public Transform roomCenter; // The center of the room
    public Vector2 roomSize; // The size of the room (width, height)
    public float cameraPadding = 1.0f; // Padding around the room boundary
    public float cameraSmoothness = 5.0f; // How smoothly the camera follows

    private Camera mainCamera;
    private Vector3 targetPosition;

    void Start()
    {
        mainCamera = Camera.main;
        if (roomCenter == null)
        {
            Debug.LogError("Room center is not assigned!");
            return;
        }
    }

    void LateUpdate()
    {
        if (roomCenter == null)
            return;

        // Calculate the target position for the camera
        float targetX = Mathf.Clamp(roomCenter.position.x, roomCenter.position.x - roomSize.x / 2 + cameraPadding, roomCenter.position.x + roomSize.x / 2 - cameraPadding);
        float targetY = Mathf.Clamp(roomCenter.position.y, roomCenter.position.y - roomSize.y / 2 + cameraPadding, roomCenter.position.y + roomSize.y / 2 - cameraPadding);
        targetPosition = new Vector3(targetX, targetY, transform.position.z);

        // Smoothly move the camera towards the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, cameraSmoothness * Time.deltaTime);
    }

    // Draw the room boundary gizmos in the Scene view
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Vector3 bottomLeft = new Vector3(roomCenter.position.x - roomSize.x / 2, roomCenter.position.y - roomSize.y / 2, roomCenter.position.z);
        Vector3 bottomRight = new Vector3(roomCenter.position.x + roomSize.x / 2, roomCenter.position.y - roomSize.y / 2, roomCenter.position.z);
        Vector3 topLeft = new Vector3(roomCenter.position.x - roomSize.x / 2, roomCenter.position.y + roomSize.y / 2, roomCenter.position.z);
        Vector3 topRight = new Vector3(roomCenter.position.x + roomSize.x / 2, roomCenter.position.y + roomSize.y / 2, roomCenter.position.z);

        Gizmos.DrawLine(bottomLeft, bottomRight);
        Gizmos.DrawLine(bottomRight, topRight);
        Gizmos.DrawLine(topRight, topLeft);
        Gizmos.DrawLine(topLeft, bottomLeft);
    }
}