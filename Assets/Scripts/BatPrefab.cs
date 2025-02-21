using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BatPrefab : MonoBehaviour
{
    float timer;
    private float lifeTime = 1.5f;
    private float batDamageAmount = 20f;
    private float batSpeed = 15f;
    private Vector3 moveDirection;

    private Vector3 currentPosition;
    private Vector3 lastPosition;

    private AudioSource batSound;

    private int batSoundIndex;

    private void Start()
    {
        batSoundIndex = Random.Range(1, 4);

        batSound = GetComponent<AudioSource>();

        PlayBatSound();
    }

    private void Awake()
    {
        if (!gameObject.IsDestroyed()) { Destroy(gameObject, lifeTime); }
    }

    private void FixedUpdate()
    {
        timer += 1f * Time.deltaTime;
        currentPosition = transform.position;
        Vector3 difference = currentPosition - lastPosition;
        RaycastHit2D hit = Physics2D.Raycast(currentPosition, lastPosition, difference.magnitude);
        if (hit && hit.collider.gameObject.name != "Player")
        {
            if (hit.collider.gameObject.TryGetComponent<IDamagable>(out IDamagable damagableObject))
            {
                damagableObject.Damage(batDamageAmount);
            }
            Destroy(gameObject);
        }
        transform.position += moveDirection * batSpeed * Time.deltaTime;
        lastPosition = transform.position;
        if (timer > 0.05f)
        {
            Destroy(this.gameObject);
        }
    }
    public void SetBatMoveDirection(Vector3 direction)
    {
        moveDirection = direction;
    }

    private void PlayBatSound()
    {
        switch (swap_items.curr_item)
        {
            case 1:
                // bat sound only
                batSound.clip = Resources.Load<AudioClip>($"SFX/Weapons/bat {batSoundIndex}");
                batSound.Play();

                break;

            default:
                break;
        }
    }
}
