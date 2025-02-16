using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;

    public PlayerShoot Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void Shoot()
    { 
        Vector3 direction = (new Vector3(Input.mousePosition.x - Screen.width/2, Input.mousePosition.y - Screen.height/2) - transform.position).normalized;

        BulletPrefab bullet = Instantiate(bulletPrefab, transform.position + direction, 
            Quaternion.FromToRotation(new Vector3(1,0,0), direction)).GetComponent<BulletPrefab>();

        bullet.SetBulletMoveDirection(direction);
    }
}
