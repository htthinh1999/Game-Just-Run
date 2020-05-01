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
        if(count%120==0)
        {
            GameObject ob = Instantiate(obstacle, transform);
            ob.transform.position = new Vector2(9, randomSpawnPos[RandomIndex()]);
        }
    }

    int RandomIndex()
    {
        float randomNumber = Random.Range(0f, 1f);
        return (randomNumber < 0.33f) ? 0 : (randomNumber < 0.66f) ? 1 : 2;
    }
}
