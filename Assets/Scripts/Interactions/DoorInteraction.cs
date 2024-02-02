using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))] // Assuming you're using a 2D collider for the trigger
public class OpenableDoor : Interaction
{
    public GameObject doorObject;
    public float openPositionY = 2.0f;
    public float moveSpeed = 0.2f; // Adjust the speed as needed
    private bool isOpen;

    private BoxCollider2D doorCollider; // Use the appropriate collider type based on your door setup

    public override void Interact()
    {
        if (!isOpen)
        {
            StartCoroutine(MoveDoor());
        }
    }

    private IEnumerator MoveDoor()
    {
        if (doorObject != null)
        {
            Vector3 initialPosition = doorObject.transform.position;
            Vector3 targetPosition = new Vector3(initialPosition.x, initialPosition.y + openPositionY, initialPosition.z);

            float elapsedTime = 0f;

            while (elapsedTime < 1f)
            {
                doorObject.transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime);
                elapsedTime += Time.deltaTime * moveSpeed;
                yield return null;
            }

            doorObject.transform.position = targetPosition;
            isOpen = true;
        }
        else
        {
            Debug.LogError("Door GameObject reference is missing!");
        }
    }

    private void Start()
    {
        doorCollider = GetComponent<BoxCollider2D>(); // Adjust if using a different collider type
        isOpen = false;
    }
}
