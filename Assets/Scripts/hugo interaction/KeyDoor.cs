    using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class KeyDoor : Interaction
{
    public GameObject doorObject;
    public float openPositionY = 2.0f; // hur långt
    public float moveSpeed = 0.2f; // fart
    public Sprite openSprite;
    public Sprite closedSprite;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D doorCollider; // använd collidern beroende på
    private bool canMove = true;

    public Inventory playerInventory; // refräng inveotory

    public override void Interact()
    {
        if (canMove)
        {
            if (playerInventory != null && playerInventory.HasKey())
            {
                StartCoroutine(MoveDoor());
                playerInventory.RemoveKey(); // ta bort nyckeln
            }
            else
            {
                Debug.Log("hämta nyckel din lilla jävla orangutang");
            }
        }
    }

    private IEnumerator MoveDoor()
    {
        if (doorObject != null)
        {
            canMove = false; // stäng av movement

            Vector3 initialPosition = doorObject.transform.position;
            Vector3 targetPosition = new Vector3(initialPosition.x, initialPosition.y + openPositionY, initialPosition.z);

            float elapsedTime = 0f;

            while (elapsedTime < 1f)
            {
                doorObject.transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime);
                spriteRenderer.sprite = openSprite; // byt sprite
                elapsedTime += Time.deltaTime * moveSpeed;
                yield return null;
            }

            doorObject.transform.position = targetPosition;
        }
        else
        {
            Debug.LogError("DÖRREN ÄR BORTA");
        }
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        doorCollider = GetComponent<BoxCollider2D>(); // får componenten
        spriteRenderer.sprite = closedSprite;
    }
}
