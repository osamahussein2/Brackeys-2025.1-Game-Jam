using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trackMouse : MonoBehaviour
{
    Vector3 mousePos;

    [SerializeField] private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Camera.main.transform.position.x > -23.5f && Camera.main.transform.position.x < 23.5f &&
            Camera.main.transform.position.y > -15.9f && Camera.main.transform.position.y < 15.9f)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        else if (Camera.main.transform.position.x <= -23.5f || Camera.main.transform.position.x >= 23.5f ||
            Camera.main.transform.position.y <= -15.9f || Camera.main.transform.position.y >= 15.9f)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + (player.transform.position - Camera.main.transform.position);
        }

        mousePos.z = 0;
        transform.position = mousePos;
    }
        
}
