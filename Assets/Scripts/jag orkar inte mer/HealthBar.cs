using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image FillImage;
    public Health reference;

    public Image healthBarImage;

    public Sprite sprite1; // eld animationen
    public Sprite sprite2; // eld animationen
    public Sprite sprite3; // eld animationen
    public Sprite sprite4; // eld animationen

    public Sprite specialSprite1; // animation f�r armor hp
    public Sprite specialSprite2; // animation f�r armor hp
    public Sprite specialSprite3; // animation f�r armor hp
    public Sprite specialSprite4; // animation f�r armor hp

    public float spriteInterval = 1f;
    public float healthUpdateInterval = 1f; // varje sekund uppdatera hp

    public bool useSpecialHealthBar = false; // s�tta p� eller av special hp
    private float spriteTimer = 0f;
    private float healthTimer = 0f;
    private int currentIndex = 0;

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
            currentIndex = (currentIndex + 1) % 4; // jag vet att detta �r ett d�ligt s�tt att g�ra det jag bryr mig bara inte
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

    public void UpdateHP() // uppdatera hp, s� n�r du skadas uppdaterar fillen
    {
        healthTimer += Time.deltaTime;
        if (healthTimer >= healthUpdateInterval)
        {
            healthTimer -= healthUpdateInterval;
            if (reference != null)
            {
                int currentHealth = reference.CurrentHealth;
                float fillAmount = (float)currentHealth / 6f;
                FillImage.fillAmount = fillAmount;
            }
            else
            {
                Debug.LogError("du m�ste refer�nsa till hp");
            }
        }
    }
}
