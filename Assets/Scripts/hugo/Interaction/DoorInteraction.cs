using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class OpenableDoor : Interaction
{
    public GameObject doorObject; // refer�ns till d�rren
    public float openPositionY = 2.0f; // hur l�ngt d�rren ska r�ra sig
    public float moveSpeed = 0.2f; // fart

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D doorCollider;
    private bool canMove = true;
    private Animator anim;

    public override void Interact()
    {
        if (canMove)
        {
            StartCoroutine(OpenDoorCoroutine());
        }
    }

    private IEnumerator OpenDoorCoroutine()
    {
        if (anim == null)
            yield break;

        anim.SetTrigger("OpenDoor");

        // V�nta p� att animationen ska slutf�ras
        yield return new WaitForSeconds(0.85f);

        // �ppna d�rren
        if (doorObject != null)
        {
            Vector3 initialPosition = doorObject.transform.position; // startar position
            Vector3 targetPosition = new Vector3(initialPosition.x, initialPosition.y + openPositionY, initialPosition.z); // target positionen

            float elapsedTime = 0f;

            while (elapsedTime < 1f) // hur l�nge innan d�rren ska sluta r�ra sig
            {
                doorObject.transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime);
                elapsedTime += Time.deltaTime * moveSpeed;
                yield return null;
            }

            doorObject.transform.position = targetPosition;
        }
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        doorCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>(); // H�mta Animator-komponenten n�r spelet b�rjar
    }
}
