using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private List<GameObject> levels;

    private float levelTimer;
    public static int currentLevel;

    // Start is called before the first frame update
    void Start()
    {
        SwitchLevels();
    }

    // Update is called once per frame
    void Update()
    {
        levelTimer += Time.deltaTime;

        // After the level timer exceeded 30 seconds, switch levels and reset the level timer
        if (levelTimer >= 30)
        {
            currentLevel += 1;

            levelTimer = 0f;
        }

        SwitchLevels();
    }

    private void SwitchLevels()
    {
        // If the current level isn't initialized, less than 1 or more than 4, set it to 1 to indicate that the first level needs to load first
        if (currentLevel <= 1 || currentLevel >= 4)
        {
            currentLevel = 1;
        }

        // Change current level here
        switch (currentLevel)
        {
            case 1:
                levels[0].SetActive(true); // Level 1
                levels[1].SetActive(false); // Level 2
                levels[2].SetActive(false); // Level 2.2

                break;

            case 2:
                levels[0].SetActive(false); // Level 1
                levels[1].SetActive(true); // Level 2
                levels[2].SetActive(false); // Level 2.2

                break;

            case 3:
                levels[0].SetActive(false); // Level 1
                levels[1].SetActive(false); // Level 2
                levels[2].SetActive(true); // Level 2.2

                break;

            default:
                break;
        }
    }
}
