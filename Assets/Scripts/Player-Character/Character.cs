using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public CharacterState stateCharacter;
    public string nameCharacter;

    public float currentLife;
    public float maxLife;

    public float currentStamina;
    public float maxStamina;
    public float timeRegenStamina;
    public float staminaCost;

    private Coroutine regen;

    public void TakeDamage(int amountDamege)
    {
        currentLife -= amountDamege;

        if(currentLife <= 0)
        {            
            stateCharacter = CharacterState.DEAD;                        
        }
    }
    public void AddLife(float amountLife)
    {
        currentLife += amountLife;
        currentLife = Mathf.Min(currentLife, maxLife);
    }
    public void UseStamina(float usedStamina)
    {
        currentStamina -= usedStamina;        
        if(currentStamina >= 0)
        {           
            if(regen != null)
            {
                StopCoroutine(regen);
            }
            regen = StartCoroutine(RegenStamina());
        } 
    }
    public void AddStamina(float amountStamina)
    {
        currentStamina += amountStamina;
        currentStamina = Mathf.Min(currentStamina, maxStamina);
    }
    
    public IEnumerator RegenStamina()
    {
        yield return new WaitForSeconds(timeRegenStamina);

        while(currentStamina < maxStamina)
        {
            currentStamina += maxStamina / 100;  
            yield return new WaitForSeconds(0.1f);
        }
        regen = null;
    }
}
