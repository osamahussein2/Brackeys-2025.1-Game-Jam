using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombie : MonoBehaviour, IDamagable
{
    int enemyLayerMask;
    int layerMask;
    public zombieGen zombieGen;
    float timer = 0f;
    public float rayLen = 1f;
    public float sightRange = 4f;
    bool seesPlayer = false;
    float coneSize = 30f;
    public GameObject player;
    Vector3 randPos;
    public float speed = 0.75f;
    // Start is called before the first frame update
    void Awake()
    {
        enemyLayerMask = LayerMask.GetMask("Enemy");
        layerMask = ~enemyLayerMask;
        zombieGen = GameObject.Find("zombieGenerator").GetComponent<zombieGen>();
        zombieGen.count += 1;
        randPos = transform.position;
        player = GameObject.Find("Player");
    }
    private void OnDestroy() {
        zombieGen.count -= 1;
    }
    // Update is called once per frame
    void moveRandomly()
    {
        if (randPos == transform.position)
            timer += 1f * Time.deltaTime;
        if (timer > 0.25f)
        {
            randPos = new Vector3(transform.position.x + Random.Range(1f, -1f), transform.position.y + Random.Range(1f, -1f), transform.position.z);
            timer = 0f;
        }
        Vector3 direction = randPos - transform.position;
        transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);
        transform.position = Vector3.MoveTowards(transform.position, randPos, speed);
    }
    float dist(Vector3 a, Vector3 b)
    {
        return Mathf.Sqrt((a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y));
    }
    void FixedUpdate()
    {
        if (dist(player.transform.position, transform.position) > sightRange && seesPlayer == true)
        {
            seesPlayer = false;
        }
        if (seesPlayer)
        {
            Vector3 direction = player.transform.position - transform.position;
            transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed);
        }
        Vector3 right = Quaternion.Euler(0, 0, -coneSize) * transform.up;
        Vector3 left = Quaternion.Euler(0, 0, coneSize) * transform.up;
        RaycastHit2D hitCenter = Physics2D.Raycast(transform.position, transform.up, rayLen, layerMask);
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, left, rayLen, layerMask);
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, right, rayLen, layerMask);
        if (hitCenter || hitLeft || hitRight)
        {
            seesPlayer = true;
        }
        else
        {
            moveRandomly();
        }
    }

    private float zombieHealth =100f;

    public void Damage(float damageAmount)
    {
        zombieHealth -= damageAmount;
        if (zombieHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
