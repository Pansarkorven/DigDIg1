using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class OpenableDoor : Interaction
{
    public GameObject doorObject; // referäns till dörren
    public float openPositionY = 2.0f; // hur långt dörren ska röra sig
    public float moveSpeed = 0.2f; // fart
    public Sprite openSprite; // sprite för öppem
    public Sprite closedSprite; // sprite för stängd

    private SpriteRenderer spriteRenderer; 
    private BoxCollider2D doorCollider; 
    private bool canMove = true;

    public override void Interact()
    {
        if (canMove)
        {
            StartCoroutine(MoveDoor());
        }
    }

    private IEnumerator MoveDoor()
    {
        if (doorObject != null)
        {
            canMove = false;

            Vector3 initialPosition = doorObject.transform.position;
            Vector3 targetPosition = new Vector3(initialPosition.x, initialPosition.y + openPositionY, initialPosition.z);

            float elapsedTime = 0f;

            while (elapsedTime < 1f)
            {
                doorObject.transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime);
                spriteRenderer.sprite = openSprite; // byt sprite vid movement
                elapsedTime += Time.deltaTime * moveSpeed;
                yield return null;
            }

            doorObject.transform.position = targetPosition;
        }
        else
        {
            Debug.LogError("dörren är borta");
        }
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        doorCollider = GetComponent<BoxCollider2D>(); 
        spriteRenderer.sprite = closedSprite;
    }
}