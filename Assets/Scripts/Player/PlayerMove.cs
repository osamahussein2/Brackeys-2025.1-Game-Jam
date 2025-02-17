using Cinemachine;
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

    private SpriteRenderer playerSprite;

    private void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();

        playerAnimator = GetComponent<Animator>();

        playerSprite = GetComponent<SpriteRenderer>();
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

        PlayerBoundaries();
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
            playerAnimator.SetBool("IsWalkingUp", false);
            playerAnimator.SetBool("IsWalkingDown", false);
            playerAnimator.SetBool("IsWalkingRight", false);
            playerAnimator.SetBool("IsWalkingLeft", false);

            playerAnimator.Play("PlayerIdle");
        }

        // Otherwise, play the walking animation
        else if (Input.GetKey(KeyCode.W))
        {
            playerAnimator.SetBool("IsWalkingUp", true);
            playerAnimator.Play("PlayerMovingUp");

            playerSprite.flipY = false; // Don't flip the sprite vertically
        }

        else if (Input.GetKey(KeyCode.A))
        {
            playerAnimator.SetBool("IsWalkingLeft", true);
            playerAnimator.Play("PlayerMovingLeft");

            playerSprite.flipX = true; // Flip the sprite horizontally
        }

        else if (Input.GetKey(KeyCode.S))
        {
            playerAnimator.SetBool("IsWalkingDown", true);
            playerAnimator.Play("PlayerMovingDown");

            playerSprite.flipY = false; // Don't flip the sprite vertically
        }

        else if (Input.GetKey(KeyCode.D))
        {
            playerAnimator.SetBool("IsWalkingRight", true);
            playerAnimator.Play("PlayerMovingRight");

            playerSprite.flipX = false; // Don't flip the sprite horizontally
        }
    }

    public LayerMask playerCollisionLayers;
    public void Dash(Vector2 direction)
    {
        if (dashCooldownTimer < dashCooldownTimerMax) { return; }

        // Only make the player dash when they're actually walking
        if (playerAnimator.GetBool("IsWalkingUp") == true || playerAnimator.GetBool("IsWalkingDown") == true ||
            playerAnimator.GetBool("IsWalkingRight") == true || playerAnimator.GetBool("IsWalkingLeft") == true)
        {
            isDashing = true;
            dashDirection = direction;
            dashStartPos = transform.position;
        }
    }

    private void PlayerBoundaries()
    {
        // Player boundaries only
        if (transform.position.x >= 19.9f)
        {
            transform.position = new Vector2(19.9f, transform.position.y);
        }

        else if (transform.position.x <= -19.9f)
        {
            transform.position = new Vector2(-19.9f, transform.position.y);
        }

        if (transform.position.y >= 15.9f)
        {
            transform.position = new Vector2(transform.position.x, 15.9f);
        }

        else if (transform.position.y <= -15.9f)
        {
            transform.position = new Vector2(transform.position.x, -15.9f);
        }
    }
}
