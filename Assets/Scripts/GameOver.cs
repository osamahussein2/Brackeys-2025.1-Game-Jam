using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private AudioSource gameMenuMusic;

    private void Start()
    {
        Cursor.visible = true; // Show the mouse cursor when the game over screen loads

        gameMenuMusic = GetComponent<AudioSource>();

        gameMenuMusic.clip = Resources.Load<AudioClip>("Music/Game menu theme");
    }

    private void Update()
    {
        if (!gameMenuMusic.isPlaying)
        {
            gameMenuMusic.Play();
        }
    }

    public void RestartGame()
    {
        // Restart the game scene
        SceneManager.LoadScene(1);
    }
}
