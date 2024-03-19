using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public Animator Animation;
    public int MaxHealth = 9;
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
            StartCoroutine(LoadDeathScreenAfterDelay(2f));
        }
    }

    IEnumerator LoadDeathScreenAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("DeathScreen");
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
