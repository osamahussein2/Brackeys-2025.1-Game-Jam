using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class gameLoader : MonoBehaviour
{
    public GameObject zombieGen;
    public GameObject menu;
    public GameObject game;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menu.SetActive(false);
            game.SetActive(true);
            foreach (GameObject zombie in zombieGen.GetComponent<zombieGen>().zombies)
            {
                zombie.SetActive(true);
            }
        }
    }
}
