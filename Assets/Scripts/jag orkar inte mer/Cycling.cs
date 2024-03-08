using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cycling : MonoBehaviour
{
    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite sprite3;
    public Sprite sprite4;
    public Sprite sprite5;

    private SpriteRenderer spriteRenderer;
    private Sprite[] sprites;
    private int currentIndex = 0;
    public float cycleTime = 0.15f;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        sprites = new Sprite[] { sprite1, sprite2, sprite3, sprite4, sprite5 }; 

        if (sprites.Length > 0)
        {
            spriteRenderer.sprite = sprites[currentIndex];
            InvokeRepeating("CycleSprites", cycleTime, cycleTime);
        }
        else
        {
            Debug.LogError("No sprites assigned to SpriteCycler.");
        }
    }

    private void CycleSprites()
    {
        currentIndex = (currentIndex + 1) % sprites.Length; // Increment index and wrap around

        spriteRenderer.sprite = sprites[currentIndex];
    }

    // Function to manually assign sprites from Unity Editor
    public void AssignSprites(Sprite[] newSprites)
    {
        sprites = newSprites;
        if (sprites.Length > 0)
        {
            spriteRenderer.sprite = sprites[currentIndex];
            CancelInvoke("CycleSprites");
            InvokeRepeating("CycleSprites", cycleTime, cycleTime);
        }
        else
        {
            Debug.LogError("No sprites assigned to SpriteCycler.");
        }
    }
}
