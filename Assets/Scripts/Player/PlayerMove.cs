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

    private Animator playerAnimator;

    private void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();

        playerAnimator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (isDashing)
        {
            RaycastHit2D hit = Physics2D.CircleCast(gameObject.transform.position, playerRadius, dashDirection, dashDistance * Time.deltaTime, playerCollisionLayers);
            if (hit)
            {
                isDashing = false;

                playerAnimator.SetBool("IsDashing", false);
            }
            else
            {
                rb.MovePosition(transform.position + (new Vector3(dashDirection.x, dashDirection.y, 0) * dashDistance * dashSpeed * Time.deltaTime));
                if ((transform.position - dashStartPos).magnitude >= dashDistance)
                {
                    isDashing = false;
                    dashCooldownTimer = 0f;

                    playerAnimator.SetBool("IsDashing", false);
                }
            }
        }
        else
        {
            if (dashCooldownTimer <= 1f) { dashCooldownTimer += Time.deltaTime; }
        }

        // If the player isn't walking, play the idle animation
        if (playerAnimator.GetBool("IsWalking") == false)
        {
            playerAnimator.Play("PlayerIdle");
        }
    }

    public void ProccessMove(Vector2 direction, bool isSprinting)
    {
        if (!isDashing)
        {
            if (!isSprinting) { direction *= 0.6f; }
            rb.velocity = direction * speed;
        }

        // If the player doesn't hold any of the WASD keys, play the idle animation
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) &&
            !Input.GetKey(KeyCode.D))
        {
            playerAnimator.SetBool("IsWalking", false);
            playerAnimator.Play("PlayerIdle");
        }

        // Otherwise, play the walking animation
        else
        {
            playerAnimator.SetBool("IsWalking", true);
            playerAnimator.Play("PlayerWalk");
        }
    }

    public LayerMask playerCollisionLayers;
    public void Dash(Vector2 direction)
    {
        if (dashCooldownTimer < dashCooldownTimerMax) { return; }

        // Only make the player dash when they're actually walking
        if (playerAnimator.GetBool("IsWalking") == true)
        {
            isDashing = true;
            dashDirection = direction;
            dashStartPos = transform.position;

            playerAnimator.SetBool("IsDashing", true);
            playerAnimator.Play("PlayerDash");
        }
    }
}
