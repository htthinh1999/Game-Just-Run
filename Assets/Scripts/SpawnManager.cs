using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject obstacle;
    [SerializeField] Player player;
    float[] randomSpawnPos;
    int count = 0;

    void Start()
    {
        
        transform.position = player.transform.position;
        randomSpawnPos = new float[] { transform.position.y - player.Distance,
                                       transform.position.y,
                                       transform.position.y + player.Distance};
    }
    
    void Update()
    {
        count++;
        if(count%120==0 && !GameManager.Instance.GameOver)
        {
            GameObject ob = Instantiate(obstacle, transform);
            ob.transform.position = new Vector2(9, randomSpawnPos[Random.Range(0, randomSpawnPos.Length)]);
        }
    }

}
