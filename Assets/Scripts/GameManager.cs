using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
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

    public void GameOver()
    {
        Speed = 0;
    }
}
