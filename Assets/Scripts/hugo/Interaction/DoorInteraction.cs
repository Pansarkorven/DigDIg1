using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class OpenableDoor : Interaction
{
    [SerializeField] GameObject doorObject; // referäns till dörren
    [SerializeField] float openPositionY = 2.0f; // hur långt dörren ska röra sig
    [SerializeField] float moveSpeed = 0.2f; // fart

    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] BoxCollider2D doorCollider;
    [SerializeField] bool canMove = true;
    [SerializeField] Animator anim;
    [SerializeField] AudioSource Ljud = null;
    [SerializeField] AudioClip Dorr = null;

    public override void Interact()
    {
        if (canMove)
        {
            StartCoroutine(OpenDoorCoroutine());
            canMove = false;
        }
    }

    private IEnumerator OpenDoorCoroutine()
    {
        if (Ljud != null && Dorr != null)
        {
            Ljud.PlayOneShot(Dorr);
            yield return new WaitForSeconds(0.6f);
        }
        if (anim == null)
            yield break;

        anim.SetTrigger("OpenDoor");
        yield return new WaitForSeconds(1.0625f); // vänta på animationen

        if (doorObject != null)
        {
            Vector3 initialPosition = doorObject.transform.position; // startar position
            Vector3 targetPosition = new Vector3(initialPosition.x, initialPosition.y + openPositionY, initialPosition.z); // target positionen

            float elapsedTime = 0f;

            while (elapsedTime < 1f) // hur länge innan dörren ska sluta röra sig
            {
                doorObject.transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime);
                elapsedTime += Time.deltaTime * moveSpeed;
                yield return null;
            } // någonting

            doorObject.transform.position = targetPosition;
        }
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        doorCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }
}
