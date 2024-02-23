using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public float stamina;
    float maxStamina;
    bool isCooldown = false;
    public float cooldownTime = 2f; // Cooldown time in seconds

    public Slider staminaBar;
    public float dValue;

    private Vector3 lastPosition; // Store the last position to check movement

    PlayerMovement playerMove;

    // Start is called before the first frame update
    void Start()
    {
        maxStamina = stamina;
        staminaBar.maxValue = maxStamina;
        lastPosition = transform.position;

        playerMove = FindObjectOfType<PlayerMovement>().GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log(playerMove.isRunning);


        if (Input.GetKey(KeyCode.LeftShift) && playerMove.isRunning) // Check if running is allowed and character is moving
        {
            isCooldown = false; // Reset cooldown when left shift is pressed
            DecreaseEnergy();
        }
        else if (stamina < maxStamina && !isCooldown) // Only increase stamina if not in cooldown
            IncreaseEnergy();

        staminaBar.value = stamina;

        // Check if LeftShift key is released
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            ActivateCooldown();
        } // Update last position
    }

    private void DecreaseEnergy()
    {
        if (stamina > 0)
            stamina -= dValue * Time.deltaTime;
        else
        {
            stamina = 0;
            playerMove.isRunning = false; // Disable running when stamina hits 0
            ActivateCooldown();
        }
    }

    private void IncreaseEnergy()
    {
        if (stamina < maxStamina) // Add condition to check if stamina is less than maxStamina
            stamina += dValue * Time.deltaTime;
        if (stamina > maxStamina) // Ensure stamina doesn't exceed maxStamina
            stamina = maxStamina;
        if (stamina > 0)
            playerMove.isRunning = true; // Enable running when stamina is restored
    }

    private void ActivateCooldown()
    {
        isCooldown = true;
        StartCoroutine(StaminaCooldown());
    }

    IEnumerator StaminaCooldown()
    {
        yield return new WaitForSeconds(cooldownTime);
        isCooldown = false; // Reset cooldown after the specified time
    }
}