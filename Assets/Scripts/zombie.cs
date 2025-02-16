using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombie : MonoBehaviour
{
    public GameObject player;
    Vector3 randPos;
    public float speed = 0.75f;
    // Start is called before the first frame update
    void Awake()
    {
        randPos = transform.position;
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void moveRandomly()
    {
        if (randPos == transform.position)
            randPos = new Vector3(transform.position.x + Random.Range(0.5f, -0.5f), transform.position.y + Random.Range(0.5f, -0.5f), transform.position.z);
        Vector3 direction = randPos - transform.position;
        transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);
        transform.position = Vector3.MoveTowards(transform.position, randPos, speed);
    }
    void FixedUpdate()
    {
        Debug.DrawRay(transform.position, transform.right);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 5);
        if (hit)
        {
            Debug.Log('a');
            if (hit.collider.gameObject.tag == "Player")
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed);
                
            }
        }
        else
        {
            moveRandomly();
        }
    }
}
