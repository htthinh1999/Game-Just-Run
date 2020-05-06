using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool GameOver { get; private set; } = false;
    public float Speed = 4f;

    void Awake()
    {
        Instance = this; 
    }
    
    void Start()
    {
        
    }

    void Update()
    {
           
    }

    public void Stop()
    {
        Speed = 0;
        GameOver = true;
    }
}
