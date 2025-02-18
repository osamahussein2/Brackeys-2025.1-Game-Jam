using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth :MonoBehaviour,  IDamagable
{
    private float playerHealth = 100f;
    private float houseHealth = 100f;

    [SerializeField] private Slider playerHP;
    [SerializeField] private Slider houseHP;

    public GameObject bloodPrefab;

    private AudioSource playerSoundEffect;

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

        playerSoundEffect = GetComponent<AudioSource>();
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

        // Set the blood sprite to red color to indicate that the player is bleeding
        bloodPrefab.GetComponent<SpriteRenderer>().color = Color.red;

        Instantiate(bloodPrefab, transform.position, Quaternion.identity);

        if (playerHealth <= 0 ) 
        {
            float timer = 0.0f;

            timer += Time.deltaTime;

            // Play the player death sound
            playerSoundEffect.clip = Resources.Load<AudioClip>("SFX/Player/player death");
            playerSoundEffect.Play();

            // After the death sound is done playing, call the die function
            if (timer >= 2f)
            {
                Die();
            }
        }
    }

    private void Die()
    { 
        //Game over logic
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the player collided with a zombie and their health is greater than 0
        if (collision.gameObject.tag == "Player" && playerHealth > 0)
        {
            // Set the clip to player taking damage
            playerSoundEffect.clip = Resources.Load<AudioClip>("SFX/Player/Player Takes Damage");

            // If the sound isn't playing, play it
            if (!playerSoundEffect.isPlaying)
            {
                playerSoundEffect.Play();
            }
        }
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
