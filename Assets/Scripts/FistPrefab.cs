using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FistPrefab : MonoBehaviour
{
    float timer;
    private float lifeTime = 1.5f;
    private float bulletDamageAmount = 60f;
    private float bulletSpeed = 50f;
    private Vector3 moveDirection;

    private Vector3 currentPosition;
    private Vector3 lastPosition;
    private void Awake()
    {
        if(!gameObject.IsDestroyed()) { Destroy(gameObject, lifeTime); }
    }

    private void FixedUpdate()
    {
        timer += 1f * Time.deltaTime;
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
        if (timer > 0.01f)
        {
            Destroy(this.gameObject);
        }
    }
    public void SetFistMoveDirection(Vector3 direction)
    {
        moveDirection = direction;
    }
}
