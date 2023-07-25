using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPowerUpSpawner : MonoBehaviour
{

    public OnObjectSpawned EVT_OnObjectSpawned;
    public OnObjectPooled EVT_OnObjectPooled;

    [SerializeField] private int SpawnCount = 1;

    [SerializeField] protected float poolTimer;

    public List<GameObject> currentSpawnedObjects { get; protected set; } = new List<GameObject>();
    public List<GameObject> pooledObjects { get; protected set; } = new List<GameObject>();

    [SerializeField] protected Vector3 SpawnPosition;

    public int totalSpawnsCount { get; private set; } = 0;
    public int totalPooledCount { get; protected set; } = 0;

    [SerializeField] float minTimeSpawnInterval = 10;
    [SerializeField] float maxTimeSpawnInterval = 20;
    [SerializeField] GameObject powerUpPrefab;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] float minPowerUpOffset = 10,maxPowerUpOffset=25;
    [SerializeField] ObstacleSpawner obstacleSpawner;
    private float currentSpawnIntervalTime=10;
    private bool isOnSpawnCooldown = false;
    private bool isSpawningPowerUp = false;

    private int currentSum = 0;
    // Start is called before the first frame update
    private void Start()
    {
        obstacleSpawner.OnObstaclesSpawn +=SpawnPowerUpOnObstacleSpawn;
    }
    
    public void SpawnPowerUpOnObstacleSpawn()
    {
        int randomNumber = Random.Range(0, 2);
        if (randomNumber == 0) isSpawningPowerUp = true;
        else isSpawningPowerUp = false;

        if (isSpawningPowerUp)
        {
            Debug.Log("Spawning powerup");
            CreatePowerup();
            isSpawningPowerUp = false;
            return;
        }
        else Debug.Log("Not spawning powerup");
    }
    public void CreatePowerup()
    {
        float powerupOffset = Random.Range(minPowerUpOffset, maxPowerUpOffset);
        for (int i = 0; i < SpawnCount; i++)
        {

            // Spawn the object. If we have an object in the pool, use that instead. Else, instantiate.
            GameObject obj;
            if (pooledObjects.Count > 0)
            {
                // get the last pooled object
                obj = pooledObjects[0];
                pooledObjects.RemoveAt(0);
                obj.SetActive(true);
                currentSpawnedObjects.Add(obj);

            }
            else
            {
                obj = Instantiate(powerUpPrefab);
                currentSpawnedObjects.Add(obj);
            }

            totalSpawnsCount += 1;
            //Set Spawn Position
            SpawnPosition = new Vector3(Random.Range(-2,2+1),0.25f ,(playerMovement.transform.position.z + powerupOffset));
            obj.transform.position = SpawnPosition;

            EVT_OnObjectSpawned.Invoke(obj);
        }

        currentSpawnIntervalTime = Random.Range(minTimeSpawnInterval, maxTimeSpawnInterval + 1);
    }
    //IEnumerator SpawnObstacle()
    //{
    //        Debug.Log(Mathf.RoundToInt(playerMovement.distanceTravelled));
    //        CreatePowerup();

    //}

}
