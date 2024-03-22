using UnityEngine;

public class CameraRoom : MonoBehaviour
{
    public Transform target; // The target to follow (usually the player)
    public float roomPadding = 1f; // Additional padding around the room
    public float minCameraSize = 5f; // Minimum orthographic size of the camera

    private Camera mainCamera;
    private Bounds roomBounds;

    void Start()
    {
        mainCamera = GetComponent<Camera>();

        // Find the bounds of the room
        FindRoomBounds();
    }

    void Update()
    {
        if (target != null)
        {
            // Calculate target position
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

            // Clamp the target position within the room bounds
            targetPosition.x = Mathf.Clamp(targetPosition.x, roomBounds.min.x, roomBounds.max.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, roomBounds.min.y, roomBounds.max.y);

            // Update camera position
            transform.position = targetPosition;

            // Adjust camera size based on room dimensions
            float cameraSizeX = (roomBounds.size.x + roomPadding * 2) / mainCamera.aspect / 2;
            float cameraSizeY = (roomBounds.size.y + roomPadding * 2) / 2;
            mainCamera.orthographicSize = Mathf.Max(Mathf.Max(cameraSizeX, cameraSizeY), minCameraSize);
        }
    }

    void FindRoomBounds()
    {
        GameObject[] roomBoundsObjects = GameObject.FindGameObjectsWithTag("RoomBound");

        if (roomBoundsObjects.Length > 0)
        {
            // Combine room bounds into a single bounding box
            roomBounds = new Bounds(roomBoundsObjects[0].transform.position, Vector3.zero);
            foreach (GameObject roomBoundsObject in roomBoundsObjects)
            {
                roomBounds.Encapsulate(roomBoundsObject.transform.position);
            }

            // Expand bounds by padding
            roomBounds.Expand(roomPadding);
        }
        else
        {
            Debug.LogError("No room bounds found. Make sure to tag your room boundaries with 'RoomBound'.");
        }
    }
}