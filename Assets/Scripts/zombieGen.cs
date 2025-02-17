using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombieGen : MonoBehaviour
{
    public int count;
    float timer;
    public GameObject zombiePrefab;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    float dist(Vector3 a, Vector3 b)
    {
        return Mathf.Sqrt((a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y));
    }
    void FixedUpdate()
    {
        timer += 1f * Time.deltaTime;
        if (timer > 0.5f && count < 200)
        {
            Vector3 pos = player.transform.position;
            while (dist(pos, player.transform.position) < 2f)
                pos = player.transform.position + new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0);
            Instantiate(zombiePrefab, pos, Quaternion.identity);
            timer = 0f;
        }
    }
}
