using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class menuLoader : MonoBehaviour
{
    public GameObject menu;
    public GameObject game;

    public static bool gamePaused;

    // Start is called before the first frame update
    void Start()
    {
        game.SetActive(true);
        menu.SetActive(false);

        gamePaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !gamePaused)
        {
            game.SetActive(false);
            menu.SetActive(true);

            gamePaused = true;
        }

        else if (Input.GetKeyDown(KeyCode.Escape) && gamePaused)
        {
            menu.SetActive(false);
            game.SetActive(true);

            gamePaused = false;
        }
    }
}
