using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public Animator Anim;
    [SerializeField] Animator AnimHealth;
    public int MaxHealth = 6;
    public int CurrentHealth;
    public GameObject Player = null;
    

    private void Start()
    {
        CurrentHealth = MaxHealth;
    }

    public void HpAnimation()
    {
        if (CurrentHealth < 6)
        {
            AnimHealth.SetTrigger("Health1");
        }
        if (CurrentHealth <5)
        {
            AnimHealth.SetTrigger("Health2");
        }
        if (CurrentHealth < 4)
        {
            AnimHealth.SetTrigger("Health3");
        }
        if (CurrentHealth < 3)
        {
            AnimHealth.SetTrigger("Health4");
        }
        if (CurrentHealth < 2)
        {
            AnimHealth.SetTrigger("Health5");
        }
        if (CurrentHealth < 1)
        {
            AnimHealth.SetTrigger("Health6");
        }
    }

    public void TakeDamage(int amount)
    {
        CurrentHealth -= amount;
        HpAnimation();
        if (CurrentHealth <= 0)
        {
            Debug.Log(" du dog noob ");
            Anim.SetBool("IsDead", true);
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
        HpAnimation();

        if (CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
    }
}
