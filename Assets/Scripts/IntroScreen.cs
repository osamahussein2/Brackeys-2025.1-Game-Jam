using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroScreen : MonoBehaviour
{
    private Canvas introCanvas;
    [SerializeField] private Canvas loadingCanvas;

    private float timer;
    public float maxIntroTime;

    // Start is called before the first frame update
    void Start()
    {
        introCanvas = GetComponent<Canvas>();

        introCanvas.gameObject.SetActive(true);
        loadingCanvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= maxIntroTime)
        {
            loadingCanvas.gameObject.SetActive(true);

            Destroy(gameObject);
        }
    }
}
