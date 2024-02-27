using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Openable : Interaction
{
    public Sprite open;
    public Sprite closed;

    private SpriteRenderer baj;
    private bool isOpen;


    public override void Interact()
    {
        if (isOpen)
            baj.sprite = closed; // programmera vad som ska hända
        else
            baj.sprite = open; // programmera vad som ska hända

        isOpen = !isOpen;
    }
    private void Start()
    {
        baj = GetComponent<SpriteRenderer>();
        baj.sprite = closed;

    }
}
