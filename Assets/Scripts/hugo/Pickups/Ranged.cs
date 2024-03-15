using UnityEngine;

public class Ranged : MonoBehaviour
{
    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite sprite3;
    public Sprite sprite4;

    private SpriteRenderer spriteRenderer;
    private Sprite[] sprites;
    private int currentIndex = 0;
    private int direction = 1;
    public float cycleTime = 0.15f;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        sprites = new Sprite[] { sprite1, sprite2, sprite3, sprite4 };

        if (sprites.Length > 0)
        {
            spriteRenderer.sprite = sprites[currentIndex];
            InvokeRepeating("CycleSprites", cycleTime, cycleTime);
        }
    }

    private void CycleSprites()
    {
        currentIndex += direction;

        // Om den har n�tt slutet i "cyclen" s� kommer den "cycla" tillbaka s� det g�r som 1, 2, 3, 4, 3, 2, 1 ist�llet f�r 1, 2, 3, 4, 1, 2, 3, 4
        if (currentIndex >= sprites.Length)
        {
            currentIndex = sprites.Length - 2;
            direction = -1;
        }
        else if (currentIndex < 0)
        {
            currentIndex = 1;
            direction = 1;
        }

        spriteRenderer.sprite = sprites[currentIndex];
    }

    // sj�lv v�lja sprites 
    public void AssignSprites(Sprite[] newSprites)
    {
        sprites = newSprites;
        if (sprites.Length > 0)
        {
            spriteRenderer.sprite = sprites[currentIndex];
            CancelInvoke("CycleSprites");
            InvokeRepeating("CycleSprites", cycleTime, cycleTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // har den inventory kopplat
            Inventory PlayerInventory = other.GetComponent<Inventory>();

            if (PlayerInventory != null)
            {
                PlayerInventory.AddRanged(); // l�gg skyddet i inventory (value)
                Destroy(gameObject); // spr�ng objektet
            }
        }
    }




}
