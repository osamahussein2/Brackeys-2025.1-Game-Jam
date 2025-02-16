using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rb;
    private float playerRadius = 0.5f;
    public static PlayerMove Instance {  get; private set; }
    private float speed = 12f;
    private float dashDistance = 3.8f;
    private float dashSpeed = 50f;
    private bool isDashing = false;
    private Vector2 dashDirection = Vector2.zero;
    private Vector3 dashStartPos = Vector3.zero;
    private float dashCooldownTimer = 1f;
    private float dashCooldownTimerMax = 0.7f;
    private void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (isDashing)
        {
            RaycastHit2D hit = Physics2D.CircleCast(gameObject.transform.position, playerRadius, dashDirection, dashDistance * Time.deltaTime, playerCollisionLayers);
            if (hit)
            {
                isDashing = false;
            }
            else
            {
                rb.MovePosition(transform.position + (new Vector3(dashDirection.x, dashDirection.y, 0) * dashDistance * dashSpeed * Time.deltaTime));
                if ((transform.position - dashStartPos).magnitude >= dashDistance)
                {
                    isDashing = false;
                    dashCooldownTimer = 0f;
                }
            }
        }
        else
        {
            if (dashCooldownTimer <= 1f) { dashCooldownTimer += Time.deltaTime; }
        }
    }

    public void ProccessMove(Vector2 direction, bool isSprinting)
    {
        if (!isDashing)
        {
            rb.velocity = direction * speed;
        }
    }

    public LayerMask playerCollisionLayers;
    public void Dash(Vector2 direction)
    {
        if (dashCooldownTimer < dashCooldownTimerMax) { return; }
        isDashing = true;
        dashDirection = direction;
        dashStartPos = transform.position;
    }
}
