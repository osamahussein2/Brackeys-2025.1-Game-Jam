using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombieGen : MonoBehaviour
{
    float timer;
    public GameObject zombiePrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += 1f * Time.deltaTime;
        if (timer > 0.5f)
        {
            Instantiate(zombiePrefab);
            timer = 0f;
        }
    }
}
