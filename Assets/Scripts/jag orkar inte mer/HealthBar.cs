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

    public float spriteInterval = 1f;
    public float healthUpdateInterval = 1f; // varje sekund uppdatera hp

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
            currentIndex = (currentIndex + 1) % 4; // jag vet att detta är ett dåligt sätt att göra det jag bryr mig bara inte
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

    public void UpdateHP()
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
                Debug.LogError("du måste referänsa till healthen");
            }
        }
    }
}
