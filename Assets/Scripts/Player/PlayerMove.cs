using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public static bool playerMovingLeft, playerMovingRight, playerMovingUp, playerMovingDown;

    [SerializeField] private AudioSource footstepSounds;

    private int concreteSoundIndex;

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

        // Otherwise, play the walking animation
        if (Input.GetKey(KeyCode.W))
        {
            playerMovingUp = true;

            playerSprite.flipY = false; // Don't flip the sprite vertically
        }

        else if (!Input.GetKey(KeyCode.W))
        {
            playerMovingUp = false;
        }

        if (Input.GetKey(KeyCode.A))
        {
            playerMovingLeft = true;

            playerSprite.flipX = true; // Flip the sprite horizontally
        }

        else if (!Input.GetKey(KeyCode.A))
        {
            playerMovingLeft = false;
        }

        if (Input.GetKey(KeyCode.S))
        {
            playerMovingDown = true;

            playerSprite.flipY = false; // Don't flip the sprite vertically
        }

        else if (!Input.GetKey(KeyCode.S))
        {
            playerMovingDown = false;
        }

        if (Input.GetKey(KeyCode.D))
        {
            playerMovingRight = true;

            playerSprite.flipX = false; // Don't flip the sprite horizontally
        }

        else if (!Input.GetKey(KeyCode.D))
        {
            playerMovingRight = false;
        }

        playerAnimator.SetBool("IsWalkingUp", playerMovingUp);
        playerAnimator.SetBool("IsWalkingDown", playerMovingDown);
        playerAnimator.SetBool("IsWalkingLeft", playerMovingLeft);
        playerAnimator.SetBool("IsWalkingRight", playerMovingRight);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Concrete")
        {
            concreteSoundIndex = Random.Range(1, 4);

            if (concreteSoundIndex == 1)
            {
                footstepSounds.clip = Resources.Load<AudioClip>("SFX/Footsteps/concrete footsteps 1");
                footstepSounds.Play();
            }

            if (concreteSoundIndex == 2)
            {
                footstepSounds.clip = Resources.Load<AudioClip>("SFX/Footsteps/concrete footsteps 2");
                footstepSounds.Play();
            }

            if (concreteSoundIndex == 3)
            {
                footstepSounds.clip = Resources.Load<AudioClip>("SFX/Footsteps/concrete footsteps 3");
                footstepSounds.Play();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Concrete")
        {
            if (!footstepSounds.isPlaying)
            {
                footstepSounds.Play();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        footstepSounds.clip = null;
    }
}
