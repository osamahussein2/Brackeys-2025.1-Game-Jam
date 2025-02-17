using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    // for the grappling hook
    // float speed = 0.1f;
    // public GameObject hookLinePrefab;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject mainCamera;

    public PlayerShoot Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void Shoot()
    { 
        Vector3 direction = (new Vector3(Input.mousePosition.x - (Screen.width/2 + transform.position.x - mainCamera.transform.position.x ), 
            Input.mousePosition.y - (Screen.height / 2 + transform.position.y - mainCamera.transform.position.y))).normalized;

        BulletPrefab bullet = Instantiate(bulletPrefab, transform.position + direction, 
            Quaternion.FromToRotation(new Vector3(1,0,0), direction)).GetComponent<BulletPrefab>();

        bullet.SetBulletMoveDirection(direction);
    }
    /*
    // helper function
    float dist(Vector2 a, Vector2 b)
    {
        return Mathf.Sqrt((a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y));
    }
    public void Grapple()
    {
        if (true) // add a clause for score or something
        {
            Vector3 direction = (new Vector3(Input.mousePosition.x - Screen.width/2, Input.mousePosition.y - Screen.height/2) - transform.position).normalized;
            BulletPrefab bullet = Instantiate(bulletPrefab, transform.position + direction, 
                Quaternion.FromToRotation(new Vector3(1,0,0), direction)).GetComponent<BulletPrefab>();
            bullet.SetBulletMoveDirection(direction);
            Vector3 end1 = transform.position;
            Vector3 end2 = bullet.transform.position;
            float scale = Mathf.Sqrt(Mathf.Pow(end1.x - end2.x, 2) + Mathf.Pow(end1.y - end2.y, 2) + Mathf.Pow(end1.z - end2.z, 2));
            GameObject hookLine = Instantiate(hookLinePrefab);
            hookLine.transform.localScale = new Vector3(transform.localScale.x, scale, transform.localScale.z);

            hookLine.transform.position = (end1 + end2) / 2;
            hookLine.transform.rotation = Quaternion.LookRotation(end1 - end2);
            hookLine.transform.Rotate(new Vector3(0, 90, 0));
            // transform.position = Vector3.MoveTowards(transform.position, bullet.transform.position, speed);
        }
    }
    */
}
