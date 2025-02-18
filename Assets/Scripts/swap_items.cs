using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class swap_items : MonoBehaviour
{
    public int curr_item;

    [SerializeField] private Image weaponImage;

    public List<Sprite> weapons;

    // Start is called before the first frame update
    void Start()
    {
        curr_item = 0;
    }

    // Update is called once per frame
    void Update()
    {
        weaponImage.sprite = weapons[curr_item];

        if (Input.GetKey("1"))
        {
            curr_item = 0;
        }
        if (Input.GetKey("2"))
        {
            curr_item = 1;
        }
        if (Input.GetKey("3"))
        {
            curr_item = 2;
        }
        if (Input.GetKey("4"))
        {
            curr_item = 3;
        }
        if (Input.GetKey("5"))
        {
            curr_item = 4;
        }
        if (Input.GetKey("6"))
        {
            curr_item = 5;
        }
        if (Input.GetKey("7"))
        {
            curr_item = 6;
        }
        if (Input.GetKey("8"))
        {
            curr_item = 7;
        }
    }
}
