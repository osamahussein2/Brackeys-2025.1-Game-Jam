using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    //[SerializeField] swap_items swapper;
    // for the grappling hook
    // float speed = 0.1f;
    // public GameObject hookLinePrefab;
    [SerializeField] private GameObject fistPrefab;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject mainCamera;

    [SerializeField] private GameObject crosshair;

    public PlayerShoot Instance { get; private set; }

    private Vector3 direction;
    private FistPrefab fist;
    private BulletPrefab bullet;

    private void Awake()
    {
        Instance = this;
    }

    public void Shoot()
    {
        if (!PlayerHealth.playerDied && !menuLoader.gamePaused && Instance != null)
        {
            // If player is moving and the cross hair is pointing where the player is currently facing
            // And if the player is in an idle state as well
            if (PlayerMove.playerMovingUp && crosshair.transform.position.y > transform.position.y ||
                PlayerMove.playerMovingDown && crosshair.transform.position.y < transform.position.y ||
                PlayerMove.playerMovingRight && crosshair.transform.position.x > transform.position.x ||
                PlayerMove.playerMovingLeft && crosshair.transform.position.x < transform.position.x ||
                !PlayerMove.playerMovingUp && !PlayerMove.playerMovingDown && !PlayerMove.playerMovingRight &&
                !PlayerMove.playerMovingLeft)
            {
                switch (swap_items.curr_item)
                {
                    case 0:
                        direction = (new Vector3(Input.mousePosition.x - (Screen.width / 2 + transform.position.x - mainCamera.transform.position.x),
                            Input.mousePosition.y - (Screen.height / 2 + transform.position.y - mainCamera.transform.position.y))).normalized;

                        fist = Instantiate(fistPrefab, transform.position + direction,
                            Quaternion.FromToRotation(new Vector3(1, 0, 0), direction)).GetComponent<FistPrefab>();

                        fist.transform.parent = GameObject.Find("game").GetComponent<Transform>();

                        fist.SetFistMoveDirection(direction);

                        break;
                    case 1:
                        // bat
                        break;
                    case 2:
                        // smg
                        direction = (new Vector3(Input.mousePosition.x - (Screen.width / 2 + transform.position.x - mainCamera.transform.position.x),
                            Input.mousePosition.y - (Screen.height / 2 + transform.position.y - mainCamera.transform.position.y))).normalized;

                        bullet = Instantiate(bulletPrefab, transform.position + direction,
                            Quaternion.FromToRotation(new Vector3(1, 0, 0), direction)).GetComponent<BulletPrefab>();

                        bullet.transform.parent = GameObject.Find("game").GetComponent<Transform>();

                        bullet.SetBulletMoveDirection(direction);

                        break;
                    case 3:
                        // pistol
                        direction = (new Vector3(Input.mousePosition.x - (Screen.width / 2 + transform.position.x - mainCamera.transform.position.x),
                            Input.mousePosition.y - (Screen.height / 2 + transform.position.y - mainCamera.transform.position.y))).normalized;

                        bullet = Instantiate(bulletPrefab, transform.position + direction,
                            Quaternion.FromToRotation(new Vector3(1, 0, 0), direction)).GetComponent<BulletPrefab>();

                        bullet.transform.parent = GameObject.Find("game").GetComponent<Transform>();

                        bullet.SetBulletMoveDirection(direction);

                        break;
                    case 4:
                        // mg
                        direction = (new Vector3(Input.mousePosition.x - (Screen.width / 2 + transform.position.x - mainCamera.transform.position.x),
                            Input.mousePosition.y - (Screen.height / 2 + transform.position.y - mainCamera.transform.position.y))).normalized;

                        bullet = Instantiate(bulletPrefab, transform.position + direction,
                            Quaternion.FromToRotation(new Vector3(1, 0, 0), direction)).GetComponent<BulletPrefab>();

                        bullet.transform.parent = GameObject.Find("game").GetComponent<Transform>();

                        bullet.SetBulletMoveDirection(direction);

                        break;
                    case 5:
                        // shotgun
                        direction = (new Vector3(Input.mousePosition.x - (Screen.width / 2 + transform.position.x - mainCamera.transform.position.x),
                            Input.mousePosition.y - (Screen.height / 2 + transform.position.y - mainCamera.transform.position.y))).normalized;

                        bullet = Instantiate(bulletPrefab, transform.position + direction,
                            Quaternion.FromToRotation(new Vector3(1, 0, 0), direction)).GetComponent<BulletPrefab>();

                        bullet.transform.parent = GameObject.Find("game").GetComponent<Transform>();

                        bullet.SetBulletMoveDirection(direction);

                        break;
                    case 6:
                        // grenade
                        break;
                    case 7:
                        // sniper
                        direction = (new Vector3(Input.mousePosition.x - (Screen.width / 2 + transform.position.x - mainCamera.transform.position.x),
                            Input.mousePosition.y - (Screen.height / 2 + transform.position.y - mainCamera.transform.position.y))).normalized;

                        bullet = Instantiate(bulletPrefab, transform.position + direction,
                            Quaternion.FromToRotation(new Vector3(1, 0, 0), direction)).GetComponent<BulletPrefab>();

                        bullet.transform.parent = GameObject.Find("game").GetComponent<Transform>();

                        bullet.SetBulletMoveDirection(direction);

                        break;
                    default:
                        // none
                        break;


                }
            }
        }

        // Else if player has died
        else if (PlayerHealth.playerDied && !menuLoader.gamePaused)
        {
            direction = Vector3.zero;

            if (!fist.IsDestroyed()) Destroy(fist);
            if (!bullet.IsDestroyed()) Destroy(bullet);
        }
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
