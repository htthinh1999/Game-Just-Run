using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject obstacle;
    [SerializeField] int obstacleCount = 5;

    void Start()
    {
        SpawnObstacle(obstacleCount);
    }
    
    void Update()
    {
        
    }

    void SpawnObstacle(int count)
    {
        for(int i=0; i<count; i++)
        {
            Instantiate(obstacle);
        }
    }
}
