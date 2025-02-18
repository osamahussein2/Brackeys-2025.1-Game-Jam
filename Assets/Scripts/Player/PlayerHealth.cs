using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth :MonoBehaviour,  IDamagable
{
    private float playerHealth = 100f;
    private float houseHealth = 100f;

    [SerializeField] private Slider playerHP;
    [SerializeField] private Slider houseHP;

    public static PlayerHealth Instance {  get; private set; }

    private void Awake()
    {
        Instance = this;

        // Start the player and house health to their max health
        playerHealth = playerHP.maxValue;
        houseHealth = houseHP.maxValue;

        // Set the player and house HP slider values equal to their initialized player and house health, respectively
        playerHP.value = playerHealth;
        houseHP.value = houseHealth;
    }

    void Update()
    {
        // Update the player and house HP slider values based on player and house health, respectively
        playerHP.value = playerHealth;
        houseHP.value = houseHealth;
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

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Damage the player
            Damage(1f * Time.deltaTime);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Damage(0f);
        }
    }
}
