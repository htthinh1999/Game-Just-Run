using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;


public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    void Move()
    {
        PCInput2();
    }
    void PCInput2()
    {
        int y = (Input.GetAxis("Vertical")  > 0 )?1: (Input.GetAxis("Vertical") < 0)?-1:0;
        transform.position = new Vector2(transform.position.x, y);
    }
    void PCInput()
    {
        if (transform.position.y == 0)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                transform.position = new Vector2(transform.position.x, 1);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                transform.position = new Vector2(transform.position.x, -1);
            }
        }
        if (transform.position.y == 1)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                transform.position = new Vector2(transform.position.x, 0);
            }
        }
        if (transform.position.y == -1)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                transform.position = new Vector2(transform.position.x, 0);
            }
        }
    }
}
