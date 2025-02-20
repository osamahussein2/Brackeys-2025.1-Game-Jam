using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FistPrefab : MonoBehaviour
{
    float timer;
    private float lifeTime = 1.5f;
    private float fistDamageAmount = 15f;
    private float fistSpeed = 50f;
    private Vector3 moveDirection;

    private Vector3 currentPosition;
    private Vector3 lastPosition;

    private AudioSource fistSound;

    private int fistSoundIndex;

    private void Start()
    {
        fistSoundIndex = Random.Range(1, 4);

        fistSound = GetComponent<AudioSource>();

        PlayFistSound();
    }

    private void Awake()
    {
        if(!gameObject.IsDestroyed()) { Destroy(gameObject, lifeTime); }
    }

    private void FixedUpdate()
    {
        timer += 1f * Time.deltaTime;
        currentPosition = transform.position;
        /*Vector3 difference = currentPosition - lastPosition;
        RaycastHit2D hit = Physics2D.Raycast(currentPosition, lastPosition, difference.magnitude);
        if (hit)
        {
            if (hit.collider.gameObject.TryGetComponent<IDamagable>(out IDamagable damagableObject))
            {
                damagableObject.Damage(fistDamageAmount);
            }
            Destroy(gameObject);
        }*/
        transform.position += moveDirection * fistSpeed * Time.deltaTime;
        lastPosition = transform.position;
        if (timer > 0.01f)
        {
            Destroy(this.gameObject);
        }
    }
    public void SetFistMoveDirection(Vector3 direction)
    {
        moveDirection = direction;
    }

    private void PlayFistSound()
    {
        switch (swap_items.curr_item)
        {
            case 0:
                // fist sound only
                fistSound.clip = Resources.Load<AudioClip>($"SFX/Weapons/fists {fistSoundIndex}");
                fistSound.Play();

                break;

            default:
                break;
        }
    }
}
