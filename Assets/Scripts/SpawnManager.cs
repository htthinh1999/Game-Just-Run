using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject Obstacle;
    int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        count++;
        if(count%120==0)
        {
            GameObject ob = Instantiate(Obstacle, transform);
            ob.transform.position = new Vector2(transform.position.x, Random.Range(-1, 2));
        }
    }
}
