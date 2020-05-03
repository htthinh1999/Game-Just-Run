using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;

    [SerializeField] float spawnDistance = 1.5f;
    [SerializeField] GameObject obstacle;
    [SerializeField] int obstacleCount = 50;
    [SerializeField] float spawnDelayMin = 1;
    [SerializeField] float spawnDelayMax = 3;

    List<GameObject> poolObjects = new List<GameObject>();
    float[] ySpawnPos;

    void Awake()
    {
        Instance = this;
        ySpawnPos = new float[] {transform.position.y + spawnDistance,
                                transform.position.y,
                                transform.position.y - spawnDistance};
    }

    void Start()
    {
        SpawnObstacle(obstacleCount);
        SpawnObstacleToScene();
    }
    
    void Update()
    {
        
    }

    void SpawnObstacle(int count)
    {
        for(int i=0; i<count; i++)
        {
            GameObject ob = Instantiate(obstacle, transform);
            ob.SetActive(false);
            poolObjects.Add(ob);
        }
    }

    void SpawnObstacleToScene()
    {
        StartCoroutine(_SpawnObstacleToScene());
    }

    IEnumerator _SpawnObstacleToScene()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(spawnDelayMin, spawnDelayMax));
            if (poolObjects.Count > 1)
            {
                for(int i=0; i<Random.Range(1, 3); i++) // Spawn 1 or 2 obstacle
                {
                    bool[] ySpawned = new bool[3];
                    GameObject ob = poolObjects[0];

                    // Spawn random position
                    int index;
                    do
                    {
                        index = Random.Range(0, 3);
                    } while (ySpawned[index] == true);
                    ySpawned[index] = true;

                    ob.transform.position = new Vector2(ScreenBounds.Right, ySpawnPos[index]);
                    ob.SetActive(true);
                    poolObjects.Remove(ob);
                }
            }
        }
    }

    public void ReturnToPool(GameObject obj)
    {
        if (!poolObjects.Contains(obj))
        {
            poolObjects.Add(obj);
            obj.SetActive(false);
        }
    }



}
