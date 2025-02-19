using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private List<GameObject> levels;

    // Start is called before the first frame update
    void Start()
    {
        // Make sure the first level is loaded
        levels[0].SetActive(true);
        levels[1].SetActive(false);
        levels[2].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
