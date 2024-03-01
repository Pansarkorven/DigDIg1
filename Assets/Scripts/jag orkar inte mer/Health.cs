using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    public Animator Animation;
    public int MaxHealth = 10;
    public int CurrentHealth;
    public GameObject Player = null;
    public RectTransform uiElement; // Reference to the UI element you want to animate

    private void Start()
    {
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(int amount)
    {
        CurrentHealth -= amount;

        if (CurrentHealth <= 0)
        {
            Debug.Log(" du dog noob ");
            Animation.SetBool("IsDead", true);
            Player.GetComponent<PlayerMovement>().enabled = false;
            StartCoroutine(AnimateUIElement());
        }
    }

    public void Heal(int amount)
    {
        CurrentHealth += amount;

        if (CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
    }

    IEnumerator AnimateUIElement()
    {
        yield return new WaitForSeconds(Animation.GetCurrentAnimatorStateInfo(0).length);
        // Wait for the length of the death animation

        // Calculate the final position of the UI element (up from under the screen)
        Vector3 targetPosition = uiElement.position;
        // Assuming the UI element starts from outside the canvas (e.g., below the screen)
        Vector3 startPosition = new Vector3(targetPosition.x, -uiElement.rect.height, targetPosition.z);
        uiElement.position = startPosition;

        // Animate the UI element moving up into the canvas
        float animationDuration = 1f; // Adjust this value as needed
        float elapsedTime = 0f;

        while (elapsedTime < animationDuration)
        {
            elapsedTime += Time.deltaTime;
            uiElement.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / animationDuration);
            yield return null;
        }

        // Ensure the UI element reaches its final position
        uiElement.position = targetPosition;
    }
}
