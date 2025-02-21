using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    private float timer;
    private float loadingTime;

    [SerializeField] private List<GameObject> circles;

    private float incrementLoadingCircles;

    public float loadingSpeed;

    [SerializeField] private AudioSource menuMusic;

    // Start is called before the first frame update
    void Start()
    {
        incrementLoadingCircles = 0;

        // Randomize the loading times
        loadingTime = Random.Range(3f, 6f);

        if (menuMusic.clip == null)
        {
            menuMusic.clip = Resources.Load<AudioClip>("Music/Game menu theme");
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (!menuMusic.isPlaying)
        {
            menuMusic.Play();
        }

        // Load next scene after reaching the loading time amount
        if (timer >= loadingTime)
        {
            SceneManager.LoadScene(1);
        }

        LoadingCircles();
    }

    private void LoadingCircles()
    {
        // Increment loading circles
        incrementLoadingCircles += loadingSpeed * Time.deltaTime;

        // Switch the value to show/hide loading circles
        if (incrementLoadingCircles >= 0f && incrementLoadingCircles < 1f)
        {
            circles[0].SetActive(false);
            circles[1].SetActive(false);
            circles[2].SetActive(false);
        }

        else if (incrementLoadingCircles >= 1f && incrementLoadingCircles < 2f)
        {
            circles[0].SetActive(true);
            circles[1].SetActive(false);
            circles[2].SetActive(false);
        }

        else if (incrementLoadingCircles >= 2f && incrementLoadingCircles < 3f)
        {
            circles[0].SetActive(true);
            circles[1].SetActive(true);
            circles[2].SetActive(false);
        }

        else if (incrementLoadingCircles >= 3f && incrementLoadingCircles < 4f)
        {
            circles[0].SetActive(true);
            circles[1].SetActive(true);
            circles[2].SetActive(true);
        }

        // If the value exceed 4 or go below 0, load circles from the beginning
        else if (incrementLoadingCircles >= 4f || incrementLoadingCircles < 0f)
        {
            incrementLoadingCircles = 0;
        }
    }
}
