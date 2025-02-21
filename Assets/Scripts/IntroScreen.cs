using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroScreen : MonoBehaviour
{
    private Canvas introCanvas;
    [SerializeField] private Canvas loadingCanvas;

    private float timer;
    public float maxIntroTime;

    [SerializeField] private AudioSource menuMusic;

    // Start is called before the first frame update
    void Start()
    {
        introCanvas = GetComponent<Canvas>();

        introCanvas.gameObject.SetActive(true);
        loadingCanvas.gameObject.SetActive(false);

        menuMusic.clip = Resources.Load<AudioClip>("Music/Game menu theme");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (!menuMusic.isPlaying)
        {
            menuMusic.Play();
        }

        // Destroy the intro screen either when the max intro time has been reached
        // Or the player presses the SPACE key
        if (timer >= maxIntroTime || Input.GetKeyDown(KeyCode.Space))
        {
            loadingCanvas.gameObject.SetActive(true);

            Destroy(gameObject);
        }
    }
}
