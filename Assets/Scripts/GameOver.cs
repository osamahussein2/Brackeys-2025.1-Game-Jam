using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private void Start()
    {
        Cursor.visible = true; // Show the mouse cursor when the game over screen loads
    }

    public void RestartGame()
    {
        // Restart the game scene
        SceneManager.LoadScene(1);
    }
}
