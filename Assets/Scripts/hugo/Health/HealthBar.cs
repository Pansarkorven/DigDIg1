using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image FillImage;
    public Sprite NormalFillSprite;
    public Sprite SpecialFillSprite;
    public Health reference;
    public Image healthBarImage;
    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite sprite3;
    public Sprite sprite4;
    public Sprite specialSprite1;
    public Sprite specialSprite2;
    public Sprite specialSprite3;
    public Sprite specialSprite4;
    public float spriteInterval = 1f;
    public float healthUpdateInterval = 1f;
    public bool useSpecialHealthBar = false;
    private float spriteTimer = 0f;
    private float healthTimer = 0f;
    private int currentIndex = 0;
    public float bombaclat = 6f;

    void Update()
    {
        UpdateSprite();
        UpdateHP();
    }

    void UpdateSprite()
    {
        spriteTimer += Time.deltaTime;
        if (spriteTimer >= spriteInterval)
        {
            spriteTimer -= spriteInterval;
            currentIndex = (currentIndex + 1) % 4;
            if (useSpecialHealthBar)
            {
                switch (currentIndex)
                {
                    case 0:
                        healthBarImage.sprite = specialSprite1;
                        break;
                    case 1:
                        healthBarImage.sprite = specialSprite2;
                        break;
                    case 2:
                        healthBarImage.sprite = specialSprite3;
                        break;
                    case 3:
                        healthBarImage.sprite = specialSprite4;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (currentIndex)
                {
                    case 0:
                        healthBarImage.sprite = sprite1;
                        break;
                    case 1:
                        healthBarImage.sprite = sprite2;
                        break;
                    case 2:
                        healthBarImage.sprite = sprite3;
                        break;
                    case 3:
                        healthBarImage.sprite = sprite4;
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public void UpdateHP()
    {
        healthTimer += Time.deltaTime;
        if (healthTimer >= healthUpdateInterval)
        {
            healthTimer -= healthUpdateInterval;
            if (reference != null)
            {
                int currentHealth = reference.CurrentHealth;
                float fillAmount = (float)currentHealth / (useSpecialHealthBar ? 9f : bombaclat); 
                if (useSpecialHealthBar)
                {
                    FillImage.sprite = SpecialFillSprite;
                }
                else
                {
                    FillImage.sprite = NormalFillSprite;
                }
                FillImage.fillAmount = fillAmount;
            }
        }
    }
}
