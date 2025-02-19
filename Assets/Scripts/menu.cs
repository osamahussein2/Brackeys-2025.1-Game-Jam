using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class menuLoader : MonoBehaviour
{
    public GameObject menu;
    public GameObject game;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && game.activeInHierarchy && !menu.activeInHierarchy)
        {
            game.SetActive(false);
            menu.SetActive(true);
        }

        else if (Input.GetKeyDown(KeyCode.Escape) && !game.activeInHierarchy && menu.activeInHierarchy)
        {
            game.SetActive(true);
            menu.SetActive(false);
        }
    }
}
