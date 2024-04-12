using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // Reference to the object the camera follows
    public float smoothSpeed = 0.125f; // Speed of camera movement
    public Vector3 offset; // Offset from the target object

    private Collider roomCollider; // Reference to the collider of the room

    private void Start()
    {
        // Find and store the collider of the room
        roomCollider = GameObject.Find("RoomCollider").GetComponent<Collider>();
    }

    private void LateUpdate()
    {
        if (target == null) // Check if target is null
            return;

        Vector3 desiredPosition = target.position + offset;

        // Clamp the desired position to stay within the room boundaries
        desiredPosition.x = Mathf.Clamp(desiredPosition.x, roomCollider.bounds.min.x, roomCollider.bounds.max.x);
        desiredPosition.y = Mathf.Clamp(desiredPosition.y, roomCollider.bounds.min.y, roomCollider.bounds.max.y);
        desiredPosition.z = Mathf.Clamp(desiredPosition.z, roomCollider.bounds.min.z, roomCollider.bounds.max.z);

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        transform.LookAt(target); // Ensure the camera looks at the target
    }
}