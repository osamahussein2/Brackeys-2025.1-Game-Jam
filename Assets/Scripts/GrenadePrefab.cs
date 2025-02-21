using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrenadePrefab : MonoBehaviour
{
    float timer = 0f;
    private float lifeTime = 1.5f;
    private float grenadeDamageAmount = 80f;
    private float grenadeSpeed = 40f;
    private Vector3 moveDirection;

    private Vector3 currentPosition;
    private Vector3 lastPosition;

    private AudioSource grenadeSound;

    private void Start()
    {
        grenadeSound = GetComponent<AudioSource>();

        PlayGrenadeSound();
    }

    private void Awake()
    {
        if (!gameObject.IsDestroyed()) { Destroy(gameObject, lifeTime); }
    }

    private void FixedUpdate()
    {
        currentPosition = transform.position;
        Vector3 difference = currentPosition - lastPosition;
        RaycastHit2D hit = Physics2D.Raycast(currentPosition, lastPosition, difference.magnitude);
        if (hit && hit.collider.gameObject.name != "Player")
        {
            if (hit.collider.gameObject.TryGetComponent<IDamagable>(out IDamagable damagableObject))
            {
                damagableObject.Damage(grenadeDamageAmount);

                moveDirection = Vector3.zero;

                grenadeSound.clip = Resources.Load<AudioClip>($"SFX/Weapons/grenade boom");
                grenadeSound.Play();

                timer += Time.deltaTime;
            }

            if (timer >= 1.1f) Destroy(gameObject);
        }

        transform.position += moveDirection * grenadeSpeed * Time.deltaTime;
        lastPosition = transform.position;
    }
    public void SetGrenadeMoveDirection(Vector3 direction)
    {
        moveDirection = direction;
    }

    private void PlayGrenadeSound()
    {
        switch (swap_items.curr_item)
        {
            case 6:
                // grenade sound only
                grenadeSound.clip = Resources.Load<AudioClip>($"SFX/Weapons/grenade throw");
                grenadeSound.Play();

                break;

            default:
                break;
        }
    }
}
