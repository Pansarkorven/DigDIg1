using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class OpenableDoor : Interaction
{
    public GameObject doorObject; // refer�ns till d�rren
    public float openPositionY = 2.0f; // hur l�ngt d�rren ska r�ra sig
    public float moveSpeed = 0.2f; // fart
    public Sprite openSprite; // sprite f�r �ppem
    public Sprite closedSprite; // sprite f�r st�ngd

    private SpriteRenderer spriteRenderer; 
    private BoxCollider2D doorCollider; 
    private bool canMove = true;

    public override void Interact() //
    {
        if (canMove)
        {
            StartCoroutine(MoveDoor()); // vid interact �ppna d�rren
        }
    }

    private IEnumerator MoveDoor() // det h�r �r vad �ppna d�rren inneb�r
    {
        if (doorObject != null)
        {
            canMove = false;

            Vector3 initialPosition = doorObject.transform.position; // startar position
            Vector3 targetPosition = new Vector3(initialPosition.x, initialPosition.y + openPositionY, initialPosition.z); // target positionen

            float elapsedTime = 0f; // tid

            while (elapsedTime < 1f) // hur l�nge innan d�rren ska sluta r�ra sig
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
            Debug.LogError("d�rren �r borta");
        }
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        doorCollider = GetComponent<BoxCollider2D>(); 
        spriteRenderer.sprite = closedSprite;
    }
}