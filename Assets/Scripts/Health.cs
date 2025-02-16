using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private Slider playerHP;
    [SerializeField] private Slider houseHP;

    public static float playerHealth;
    public static float houseHealth;

    // Start is called before the first frame update
    void Start()
    {
        // Start the player and house health to their max health
        playerHealth = playerHP.maxValue;
        houseHealth = houseHP.maxValue;

        // Set the player and house HP slider values equal to their initialized player and house health, respectively
        playerHP.value = playerHealth;
        houseHP.value = houseHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // Update the player and house HP slider values based on player and house health, respectively
        playerHP.value = playerHealth;
        houseHP.value = houseHealth;
    }
}
