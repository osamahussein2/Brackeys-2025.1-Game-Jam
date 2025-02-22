using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class menuLoader : MonoBehaviour
{
    public GameObject menu;
    public GameObject game;

    public static bool gamePaused;

    private AudioSource menuMusic;

    // Start is called before the first frame update
    void Start()
    {
        game.SetActive(true);
        menu.SetActive(false);

        gamePaused = false;

        menuMusic = GetComponent<AudioSource>();

        menuMusic.clip = Resources.Load<AudioClip>("Music/Game menu theme");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && !gamePaused)
        {
            game.SetActive(false);
            menu.SetActive(true);

            gamePaused = true;
        }

        else if (Input.GetKeyDown(KeyCode.Return) && gamePaused)
        {
            menu.SetActive(false);
            game.SetActive(true);

            gamePaused = false;
        }

        PlayMusicOnPauseMenu();
    }

    private void PlayMusicOnPauseMenu()
    {
        // If the game isn't paused and menu music is playing, pause the music
        if (!gamePaused && menuMusic.isPlaying)
        {
            menuMusic.Pause();
        }

        // Else the game is paused and menu music is not playing, play the music
        else if (gamePaused && !menuMusic.isPlaying)
        {
            menuMusic.Play();
        }
    }
}
