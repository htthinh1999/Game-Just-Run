using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;

    [Header("Obstacle")]
    [SerializeField] GameObject[] obstacles;
    [SerializeField] int obstacleCount = 60;
    [SerializeField] float spawnObstaclesDelayMin = 10;
    [SerializeField] float spawnObstaclesDelayMax = 30;
    [Header("Shield")]
    [SerializeField] GameObject shield;
    [SerializeField] int shieldCount = 10;
    [SerializeField] float spawnShieldDelayMin = 40;
    [SerializeField] float spawnShieldDelayMax = 50;
    [Header("Other")]
    [SerializeField] Player player;

    Dictionary<string ,List<GameObject>> poolObjects = new Dictionary<string, List<GameObject>>();
    string strObstacles = "obstacles", strShield = "shield";
    float[] ySpawnPos;
    bool[] ySpawned = new bool[3];

    void Awake()
    {
        Instance = this;
        transform.position = player.transform.position;
        ySpawnPos = new float[] {transform.position.y + player.Distance,
                                transform.position.y,
                                transform.position.y - player.Distance};
        List<GameObject> list = new List<GameObject>();
        poolObjects.Add(strObstacles, list);
        list = new List<GameObject>();
        poolObjects.Add(strShield, list);
    }

    void Start()
    {
        SpawnObjects();
    }

    void SpawnObjects()
    {
        StartCoroutine(_SpawnObjects());
    }

    IEnumerator _SpawnObjects()
    {
        SpawnObstacle(obstacleCount);
        SpawnShield(shieldCount);
        SpawnObstacleToScene();
        yield return new WaitForSeconds(10);
        SpawnShieldToScene();
    }
	
    void SpawnObstacle(int count)
    {
        for(int i=0; i<count; i++)
        {
            GameObject ob = Instantiate(obstacles[Random.Range(0, obstacles.Length)], transform);
            ob.SetActive(false);
            poolObjects[strObstacles].Add(ob);
        }
    }

    void SpawnShield(int count)
    {
        for(int i=0; i<count; i++)
        {
            GameObject sh = Instantiate(shield, transform);
            sh.SetActive(false);
            poolObjects[strShield].Add(sh);
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
            yield return new WaitForSeconds(Random.Range(spawnObstaclesDelayMin, spawnObstaclesDelayMax) / GameManager.Instance.Speed);
            if (poolObjects[strObstacles].Count > 1)
            {
                ySpawned = new bool[3];
                for (int i=0; i<Random.Range(1, 3); i++) // Spawn 1 or 2 obstacle
                {
                    GameObject ob = poolObjects[strObstacles][0];
                    
                    // Spawn random position
                    int index;
                    do
                    {
                        index = Random.Range(0, 3);
                    } while (ySpawned[index] == true);
                    ySpawned[index] = true;

                    ob.transform.position = new Vector2(ScreenBounds.Right, ySpawnPos[index]);
                    ob.SetActive(true);
                    poolObjects[strObstacles].Remove(ob);
                }
            }
        }
    }

    public void SpawnShieldToScene()
    {
        StartCoroutine(_SpawnShieldToScene());
    }

    IEnumerator _SpawnShieldToScene()
    {
        while (!GameManager.Instance.GameOver)
        {
            yield return new WaitForSeconds(Random.Range(spawnShieldDelayMin, spawnShieldDelayMax) / GameManager.Instance.Speed);
            if (poolObjects[strShield].Count > 1)
            {
                GameObject sh = poolObjects[strShield][0];

                int index;
                do
                {
                    index = Random.Range(0, 3);
                } while (ySpawned[index] == true);
                ySpawned[index] = true;

                // Spawn random position
                sh.transform.position = new Vector2(ScreenBounds.Right, ySpawnPos[index]);
                sh.SetActive(true);
                poolObjects[strShield].Remove(sh);
            }
        }
    }

    public void ReturnToPool(string poolName, GameObject obj)
    {
        if (!poolObjects[poolName].Contains(obj))
        {
            poolObjects[poolName].Add(obj);
            obj.SetActive(false);
        }
    }

}
