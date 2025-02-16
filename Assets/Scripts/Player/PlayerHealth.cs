using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth :MonoBehaviour,  IDamagable
{
    private float playerHealth = 100f;
    
    public static PlayerHealth Instance {  get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void Damage(float damageAmount)
    {
        playerHealth -= damageAmount;
        if (playerHealth <= 0 ) { Die(); }
    }

    private void Die()
    { 
        //Game over logic
    }
}
