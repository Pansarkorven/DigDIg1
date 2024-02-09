using UnityEngine.UI;
using UnityEngine;

public class StaminaBar : MonoBehaviour
{
    public Slider slider;
    public float maxStamina = 100;
    public float currentStamina;

    public bool isRunning = false;

    public float staminaBurnRate = 20f;
    public float staminaRegenRate = 10f;

    private PlayerMovement playerMovement;
    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = maxStamina;
        currentStamina = maxStamina;
        slider.value = currentStamina;

        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMovement.isRunning && currentStamina > 0)
        {
            currentStamina -= staminaBurnRate * Time.deltaTime;
            currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
        }
        else if (!PlayerMovement.isRunning && currentStamina < maxStamina)
        {
            currentStamina += staminaRegenRate * Time.deltaTime;
            currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
        }

        slider.value = currentStamina;
    }
}
