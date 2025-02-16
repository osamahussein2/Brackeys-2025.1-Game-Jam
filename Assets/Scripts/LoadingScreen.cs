using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    private float timer;
    public float maxLoadingTime;

    [SerializeField] private List<GameObject> circles;

    private int incrementLoadingCircles;

    // Start is called before the first frame update
    void Start()
    {
        incrementLoadingCircles = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        // Load next scene after reaching the maximum loading time
        if (timer >= maxLoadingTime)
        {
            SceneManager.LoadScene(1);
        }

        LoadingCircles();
    }

    private void LoadingCircles()
    {
        // Increment loading circles
        incrementLoadingCircles++;

        // Switch between loading circles to render/hide
        switch (incrementLoadingCircles)
        {
            case 0:
                circles[0].SetActive(false);
                circles[1].SetActive(false);
                circles[2].SetActive(false);

                break;

            case 1:
                circles[0].SetActive(true);
                circles[1].SetActive(false);
                circles[2].SetActive(false);

                break;

            case 2:
                circles[0].SetActive(true);
                circles[1].SetActive(true);
                circles[2].SetActive(false);

                break;

            case 3:
                circles[0].SetActive(true);
                circles[1].SetActive(true);
                circles[2].SetActive(true);

                break;

            case 4:
                incrementLoadingCircles = 0; // Go back to the beginning

                break;

            default:
                break;
        }
    }
}
