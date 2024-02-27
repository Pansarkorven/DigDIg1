using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public Animator Animation;
    public int MaxHealth = 10;
    public int CurrentHealth;

    private void Start()
    {
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(int amount)
    {
        CurrentHealth -= amount;

        if(CurrentHealth <= 0)  
        {
            Debug.Log(" du dog noob ");
          Animation.SetBool("IsDead", true);
        }
    }

    public void Heal(int amount)
    {   
        CurrentHealth += amount;

        if (CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
            //  Animation.SetBool("IsDead", true);
        }
    }


}
