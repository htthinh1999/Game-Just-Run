using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Player : MonoBehaviour
{
    public float Distance = 1.5f;
    float ySpawnPos;
    int check = 0;
    bool dead = false;

    void Start()
    {
        ySpawnPos = transform.position.y;
    }

    void Update()
    {
        if(!dead)
        {
            Move();
        }
    }

    void Move()
    {       
        PCInput2();
    }

    void PCInput2()
    {
        float direction = Input.GetAxis("Vertical");  //return [-1; 1] when push W or S or up arrow or down arrow
        if (Input.GetButtonDown("Vertical"))            //return true when push W or S or up arrow or down arrow
        {
            if (direction < 0 && transform.position.y > ( ySpawnPos - Distance))
            {
                transform.position = new Vector2(transform.position.x, transform.position.y - Distance);
            }
            else if (direction > 0 && transform.position.y < (ySpawnPos + Distance))
            {
                transform.position = new Vector2(transform.position.x, transform.position.y + Distance);
            }
        }
    }

    void PCInput()
    {
        if (transform.position.y == 0)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                transform.position = new Vector2(transform.position.x, 1);
            }
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                transform.position = new Vector2(transform.position.x, -1);
            }
        }
        if (transform.position.y == 1)
        {
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                transform.position = new Vector2(transform.position.x, 0);
            }
        }
        if (transform.position.y == -1)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                transform.position = new Vector2(transform.position.x, 0);
            }
        }
    }
    public bool CheckDeath()
    {
        if (check == 1)
            return true;
        else
            return false;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Va cham");
        check = 1;
        dead = true;
        GameManager.Instance.StopGame();
    }
}
