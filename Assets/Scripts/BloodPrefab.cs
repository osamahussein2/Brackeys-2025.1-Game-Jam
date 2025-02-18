using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodPrefab : MonoBehaviour
{
    [SerializeField] private List<Sprite> bloodSprites;

    private SpriteRenderer bloodSprite;

    private float bloodAlpha;

    public float alphaSpeed;

    private float timer;
    public float maxTime;

    private int bloodSpriteIndex;

    // Start is called before the first frame update
    void Start()
    {
        bloodSprite = GetComponent<SpriteRenderer>();

        bloodSpriteIndex = Random.Range(1, 3);

        bloodAlpha = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        ShowBlood();

        bloodSprite.color = new Color(1f, 1f, 1f, bloodAlpha);

        timer += Time.deltaTime;

        if (timer >= maxTime)
        {
            bloodAlpha -= alphaSpeed * Time.deltaTime;
        }

        if (bloodAlpha <= 0.0f)
        {
            Destroy(gameObject);
        }
    }

    private void ShowBlood()
    {
        switch (bloodSpriteIndex)
        {
            case 1:
                bloodSprite.sprite = bloodSprites[0];
                break;

            case 2:
                bloodSprite.sprite = bloodSprites[1];
                break;

            case 3:
                bloodSprite.sprite = bloodSprites[2];
                break;

            default:
                break;
        }
    }
}
