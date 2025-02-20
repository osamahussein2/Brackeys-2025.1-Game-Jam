using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletPrefab : MonoBehaviour
{
    private float lifeTime = 1.5f;
    private float bulletDamageAmount = 60f;
    private float bulletSpeed = 50f;
    private Vector3 moveDirection;

    private Vector3 currentPosition;
    private Vector3 lastPosition;

    private AudioSource bulletSound;

    private int smgSoundIndex;
    private int pistolSoundIndex;

    private void Start()
    {
        smgSoundIndex = Random.Range(1, 5);
        pistolSoundIndex = Random.Range(1, 3);

        bulletSound = GetComponent<AudioSource>();

        BulletSounds();
    }

    private void Awake()
    {
        if (!gameObject.IsDestroyed()) { Destroy(gameObject, lifeTime); }
    }

    private void Update()
    {
        currentPosition = transform.position;
        Vector3 difference = currentPosition - lastPosition;
        RaycastHit2D hit = Physics2D.Raycast(currentPosition, lastPosition, difference.magnitude);
        if (hit)
        {
            if (hit.collider.gameObject.TryGetComponent<IDamagable>(out IDamagable damagableObject))
            {
                damagableObject.Damage(bulletDamageAmount);
            }
            Destroy(gameObject);
        }
        transform.position += moveDirection * bulletSpeed * Time.deltaTime;
        lastPosition = transform.position;
    }
    public void SetBulletMoveDirection(Vector3 direction)
    {
        moveDirection = direction;
    }

    private void BulletSounds()
    {
        switch (swap_items.curr_item)
        {
            case 2:
                // smg
                bulletSound.clip = Resources.Load<AudioClip>($"SFX/Weapons/smg {smgSoundIndex}");
                bulletSound.Play();

                break;

            case 3:
                // pistol
                bulletSound.clip = Resources.Load<AudioClip>($"SFX/Weapons/pistol {pistolSoundIndex}");
                bulletSound.Play();

                break;

            case 4:
                // mg
                //bulletSound.clip = Resources.Load<AudioClip>("SFX/Weapons/missing machine gun sound");
                //bulletSound.Play();

                break;

            case 5:
                // shotgun
                bulletSound.clip = Resources.Load<AudioClip>("SFX/Weapons/shotgun");
                bulletSound.Play();

                break;

            case 6:
                // grenade
                bulletSound.clip = Resources.Load<AudioClip>("SFX/Weapons/grenade throw");
                bulletSound.Play();

                break;

            case 7:
                // sniper
                bulletSound.clip = Resources.Load<AudioClip>("SFX/Weapons/sniper");
                bulletSound.Play();

                break;

            default:
                // none
                break;
        }
    }
}
