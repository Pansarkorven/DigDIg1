using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // Add this line to import the SceneManager

public class Health : MonoBehaviour
{
    public Animator Animation;
    public int MaxHealth = 10;
    public int CurrentHealth;
    public GameObject Player = null;

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
            Player.GetComponent<MainCharacterController>().enabled = false;
            StartCoroutine(LoadDeathScreenAfterDelay(2f)); // Start the coroutine to delay scene loading
        }
    }

    IEnumerator LoadDeathScreenAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("DeathScreen"); // Load the DeathScreen scene after the delay
    }

    public void Heal(int amount)
    {
        CurrentHealth += amount;

        if (CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
    }
}
