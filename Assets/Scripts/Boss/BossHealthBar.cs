using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossHealthBar : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private RectTransform healthBarFill;

    // Function to update the boss's health value and adjust the health bar fill amount
    public void UpdateHealth(float currentHealth, float maxHealth)
    {
        float fillAmount = currentHealth / maxHealth;
        healthText.text = $"{currentHealth} / {maxHealth}"; // Update the health text
        healthBarFill.localScale = new Vector3(fillAmount, 1f, 1f); // Adjust the health bar fill amount
    }

    // Function to show the boss health bar
    public void ShowHealthBar()
    {
        gameObject.SetActive(true);
    }

    // Function to hide the boss health bar
    public void HideHealthBar()
    {
        gameObject.SetActive(false);
    }
}
