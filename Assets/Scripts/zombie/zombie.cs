using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombie : MonoBehaviour, IDamagable
{
    int enemyLayerMask;
    int layerMask;
    public zombieGen zombieGen;
    float timer = 0f;
    public float rayLen = 1f;
    public float sightRange = 4f;
    bool seesPlayer = false;
    float coneSize = 30f;
    public GameObject player;
    Vector3 randPos;
    public float speed = 0.75f;

    private Animator zombieAnimator;
    private SpriteRenderer zombieSprite;

    public GameObject bloodPrefab;

    private AudioSource zombieHealthSounds;
    [SerializeField] private AudioSource zombieSounds;

    private int zombieSoundIndex;

    private bool zombieDied;

    private float zombieAlpha;

    private void Start()
    {
        zombieDied = false;

        zombieAlpha = 1.0f;
    }

    // Start is called before the first frame update
    void Awake()
    {
        enemyLayerMask = LayerMask.GetMask("Enemy");
        layerMask = ~enemyLayerMask;
        zombieGen = GameObject.Find("zombieGenerator").GetComponent<zombieGen>();
        zombieGen.count += 1;
        randPos = transform.position;
        player = GameObject.Find("Player");

        zombieAnimator = GetComponent<Animator>();
        zombieSprite = GetComponent<SpriteRenderer>();

        zombieHealthSounds = GetComponent<AudioSource>();

        zombieSoundIndex = Random.Range(1, 4);
    }

    private void Update()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, zombieAlpha);

        if (zombieDied)
        {
            zombieAlpha -= 0.5f * Time.deltaTime;

            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }

        else
        {
            zombieAlpha = 1.0f;

            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }

        if (zombieAlpha <= 0.0f)
        {
            Destroy(gameObject);
        }

        if (zombieHealth > 0)
        {
            // Have different zombies play different sounds
            switch (zombieSoundIndex)
            {
                case 1:

                    zombieSounds.clip = Resources.Load<AudioClip>("SFX/Zombies/zombie sound 1");

                    break;

                case 2:

                    zombieSounds.clip = Resources.Load<AudioClip>("SFX/Zombies/zombie sound 2");

                    break;

                case 3:

                    zombieSounds.clip = Resources.Load<AudioClip>("SFX/Zombies/zombie sound 3");

                    break;

                default: // Only if the number isn't 1-3

                    zombieSounds.clip = Resources.Load<AudioClip>("SFX/Zombies/zombie sound 3");

                    break;
            }

            if (!zombieSounds.isPlaying)
            {
                zombieSounds.Play();
            }
        }
    }

    private void OnDestroy()
    {
        zombieGen.count -= 1;
    }
    // Update is called once per frame
    void moveRandomly()
    {
        if (randPos == transform.position)
            timer += 1f * Time.deltaTime;
        if (timer > 0.25f)
        {
            randPos = new Vector3(transform.position.x + Random.Range(1f, -1f), transform.position.y + Random.Range(1f, -1f), transform.position.z);
            timer = 0f;
        }
        Vector3 direction = randPos - transform.position;
        //transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);
        transform.position = Vector3.MoveTowards(transform.position, randPos, speed);

        if (direction.x <= -0.01f)
        {
            zombieAnimator.SetBool("IsMovingLeft", true);
            zombieAnimator.SetBool("IsMovingRight", false);

            zombieSprite.flipX = true;
        }

        else if (direction.x >= 0.01f)
        {
            zombieAnimator.SetBool("IsMovingRight", true);
            zombieAnimator.SetBool("IsMovingLeft", false);

            zombieSprite.flipX = false;
        }

        if (direction.y <= -0.01f)
        {
            zombieAnimator.SetBool("IsMovingDown", true);
            zombieAnimator.SetBool("IsMovingUp", false);

            zombieSprite.flipY = false;
        }

        else if (direction.y >= 0.01f)
        {
            zombieAnimator.SetBool("IsMovingUp", true);
            zombieAnimator.SetBool("IsMovingDown", false);

            zombieSprite.flipY = false;
        }

        if (direction == Vector3.zero)
        {
            zombieAnimator.SetBool("IsMovingLeft", false);
            zombieAnimator.SetBool("IsMovingRight", false);
            zombieAnimator.SetBool("IsMovingUp", false);
            zombieAnimator.SetBool("IsMovingDown", false);
        }
    }
    float dist(Vector3 a, Vector3 b)
    {
        return Mathf.Sqrt((a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y));
    }
    void FixedUpdate()
    {
        if (dist(player.transform.position, transform.position) > sightRange && seesPlayer == true)
        {
            seesPlayer = false;
        }
        if (seesPlayer)
        {
            Vector3 direction = player.transform.position - transform.position;
            //transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed);

            if (direction.x <= -0.01f)
            {
                zombieAnimator.SetBool("IsMovingLeft", true);
                zombieAnimator.SetBool("IsMovingRight", false);

                zombieSprite.flipX = true;
            }

            else if (direction.x >= 0.01f)
            {
                zombieAnimator.SetBool("IsMovingRight", true);
                zombieAnimator.SetBool("IsMovingLeft", false);

                zombieSprite.flipX = false;
            }

            if (direction.y <= -0.01f)
            {
                zombieAnimator.SetBool("IsMovingDown", true);
                zombieAnimator.SetBool("IsMovingUp", false);

                zombieSprite.flipY = false;
            }

            else if (direction.y >= 0.01f)
            {
                zombieAnimator.SetBool("IsMovingUp", true);
                zombieAnimator.SetBool("IsMovingDown", false);

                zombieSprite.flipY = false;
            }

            if (direction == Vector3.zero)
            {
                zombieAnimator.SetBool("IsMovingLeft", false);
                zombieAnimator.SetBool("IsMovingRight", false);
                zombieAnimator.SetBool("IsMovingUp", false);
                zombieAnimator.SetBool("IsMovingDown", false);
            }
        }
        Vector3 right = Quaternion.Euler(0, 0, -coneSize) * transform.up;
        Vector3 left = Quaternion.Euler(0, 0, coneSize) * transform.up;
        RaycastHit2D hitCenter = Physics2D.Raycast(transform.position, transform.up, rayLen, layerMask);
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, left, rayLen, layerMask);
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, right, rayLen, layerMask);
        if (hitCenter || hitLeft || hitRight)
        {
            seesPlayer = true;
        }
        if (!seesPlayer)
        {
            moveRandomly();
        }
    }

    private float zombieHealth = 100f;

    public void Damage(float damageAmount)
    {
        zombieHealth -= damageAmount;

        if (damageAmount > 0 && zombieHealth > 0)
        {
            // Play the zombie damaged from bullet sound for now
            zombieHealthSounds.clip = Resources.Load<AudioClip>("SFX/Zombies/zombie takes bullet");
            zombieHealthSounds.Play();
        }

        // Player the different zombie damage sound depending on weapon

        /* 
        
        zombieSoundEffect.clip = Resources.Load<AudioClip>("SFX/Zombies/zombie takes fist or bat");
        zombieSoundEffect.Play();

        zombieSoundEffect.clip = Resources.Load<AudioClip>("SFX/Zombies/zombie takes fist or bat 2");
        zombieSoundEffect.Play();

        */

        // Set the blood sprite to its default color
        bloodPrefab.GetComponent<SpriteRenderer>().color = Color.white;

        GameObject blood = Instantiate(bloodPrefab, transform.position, Quaternion.identity);
        blood.transform.parent = GameObject.Find("game").GetComponent<Transform>();

        if (zombieHealth <= 0)
        {
            zombieDied = true;

            // Play the zombie death sound
            zombieHealthSounds.clip = Resources.Load<AudioClip>("SFX/Zombies/zombie death");
            zombieHealthSounds.Play();

            gameObject.GetComponent<Animator>().Play("ZombieDeath");
        }
    }
}
