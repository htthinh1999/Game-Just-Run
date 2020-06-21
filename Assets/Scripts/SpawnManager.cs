using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;

    [SerializeField] GameObject[] obstacles;
    [SerializeField] Player player;
    [SerializeField] int obstacleCount = 50;
    [SerializeField] float spawnDelayMin = 1;
    [SerializeField] float spawnDelayMax = 3;

    List<GameObject> poolObjects = new List<GameObject>();
    float[] ySpawnPos;

    void Awake()
    {
        Instance = this;
        transform.position = player.transform.position;
        ySpawnPos = new float[] {transform.position.y + player.Distance,
                                transform.position.y,
                                transform.position.y - player.Distance};
    }

    void Start()
    {
        SpawnObstacle(obstacleCount);
        SpawnObstacleToScene();
    }
	
    void SpawnObstacle(int count)
    {
        for(int i=0; i<count; i++)
        {
            GameObject ob = Instantiate(obstacles[Random.Range(0, obstacles.Length)], transform);
            ob.SetActive(false);
            poolObjects.Add(ob);
        }
    }

    public void SpawnObstacleToScene()
    {
        StartCoroutine(_SpawnObstacleToScene());
    }

    IEnumerator _SpawnObstacleToScene()
    {
        while (!GameManager.Instance.GameOver)
        {
            yield return new WaitForSeconds(Random.Range(spawnDelayMin, spawnDelayMax));
            if (poolObjects.Count > 1)
            {
                bool[] ySpawned = new bool[3];
                for (int i=0; i<Random.Range(1, 3); i++) // Spawn 1 or 2 obstacle
                {
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
