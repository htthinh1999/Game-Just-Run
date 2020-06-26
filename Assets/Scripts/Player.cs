using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Player : MonoBehaviour
{
    public float Distance = 1.5f;
    [SerializeField] GameObject mobileInput;
    [SerializeField] GameObject shield;
    float ySpawnPos;
    bool dead = false, hasShield = false;

    void Awake()
    {
        #if UNITY_EDITOR || UNITY_STANDALONE_WIN
            mobileInput.SetActive(false);
        #endif
    }

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
        PCInputTest();
    }

    void PCInputTest()
    {
        if(Input.GetButtonDown("UpArrow"))
        {
            transform.position = new Vector2(transform.position.x, Mathf.Min(transform.position.y + Distance,ySpawnPos + Distance));
        }
        if (Input.GetButtonDown("DownArrow"))
        {
            transform.position = new Vector2(transform.position.x, Mathf.Max(transform.position.y - Distance, ySpawnPos - Distance));
        }
    }

    /*void PCInput()
    {
        if (transform.position.y == ySpawnPos)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                transform.position = new Vector2(transform.position.x, transform.position.y + Distance);
            }
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                transform.position = new Vector2(transform.position.x, transform.position.y - Distance);
            }
        }
        if (transform.position.y == ySpawnPos + Distance)
        {
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                transform.position = new Vector2(transform.position.x, ySpawnPos);
            }
        }
        if (transform.position.y == ySpawnPos - Distance)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                transform.position = new Vector2(transform.position.x, ySpawnPos);
            }
        }
    }*/
    
    public void MobileInput(string direction)
    {
        if (direction.Equals("UpArrow"))
        {
            transform.position = new Vector2(transform.position.x, Mathf.Min(transform.position.y + Distance, ySpawnPos + Distance));
        }
        if (direction.Equals("DownArrow"))
        {
            transform.position = new Vector2(transform.position.x, Mathf.Max(transform.position.y - Distance, ySpawnPos - Distance));
        }
    }

    public bool CheckDeath()
    {
        return dead;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Shield"))
        {
            AudioManager.Instance.PlaySound("shield");
            if (!hasShield)
            {
                hasShield = true;
                shield.SetActive(true);
            }
            else
            {
                GameManager.Instance.Plus1000(transform.position);
            }
            SpawnManager.Instance.ReturnToPool("shield", col.gameObject);
        }
        else if (col.gameObject.CompareTag("Obstacle"))
        {
            if (!hasShield)
            {
                dead = true;
                AudioManager.Instance.PlaySound("ugh");
                GameManager.Instance.StopGame();
            }
            else
            {
                AudioManager.Instance.PlaySound("collision");
                hasShield = false;
                shield.SetActive(false);
            }
        }
    }
}
