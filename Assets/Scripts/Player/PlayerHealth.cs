using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth :MonoBehaviour,  IDamagable
{
    private float playerHealth = 100f;
    //private float houseHealth = 100f;

    [SerializeField] private Slider playerHP;
    //[SerializeField] private Slider houseHP;

    public GameObject bloodPrefab;

    private AudioSource playerSoundEffect;

    float deathTimer = 0.0f;

    private float playerAlpha;

    public static PlayerHealth Instance {  get; private set; }

    public static bool playerDied;

    private void Start()
    {
        playerDied = false;

        playerAlpha = 1.0f;

        // Start the player health to its max health
        playerHealth = playerHP.maxValue;

        // Update the player health slider with it
        playerHP.value = playerHealth;
    }

    private void Awake()
    {
        Instance = this;

        playerSoundEffect = GetComponent<AudioSource>();
    }

    void Update()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, playerAlpha);

        // Update the player and house HP slider values based on player and house health, respectively
        playerHP.value = playerHealth;
        //houseHP.value = houseHealth;

        if (playerDied)
        {
            deathTimer += Time.deltaTime;

            playerAlpha -= 0.5f * Time.deltaTime;

            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }

        else
        {
            deathTimer = 0f;

            playerAlpha = 1.0f;

            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }

        // After the death sound is done playing, call the die function
        if (deathTimer >= 2f)
        {
            Die();
        }
    }

    public void Damage(float damageAmount)
    {
        playerHealth -= damageAmount;

        if (damageAmount > 0)
        {
            // Set the blood sprite to red color to indicate that the player is bleeding
            bloodPrefab.GetComponent<SpriteRenderer>().color = Color.red;

            GameObject blood = Instantiate(bloodPrefab, transform.position, Quaternion.identity);
            blood.transform.parent = GameObject.Find("game").GetComponent<Transform>();
        }

        if (playerHealth <= 0) 
        {
            playerDied = true;

            // Play the player death sound
            playerSoundEffect.clip = Resources.Load<AudioClip>("SFX/Player/player death");
            playerSoundEffect.Play();

            gameObject.GetComponent<Animator>().Play("PlayerDeath");
        }
    }

    private void Die()
    {
        //Game over logic
        SceneManager.LoadScene(2); // Load game over scene
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the player collided with a zombie and their health is greater than 0
        if (collision.gameObject.tag == "Player" && playerHealth > 0f)
        {
            // Set the clip to player taking damage
            playerSoundEffect.clip = Resources.Load<AudioClip>("SFX/Player/Player Takes Damage");
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && playerHealth > 0f)
        {
            // Damage the player
            Damage(1f * Time.deltaTime);

            if (!playerSoundEffect.isPlaying)
            {
                playerSoundEffect.Play();
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && playerHealth > 0f)
        {
            Damage(0f);
        }
    }
}
