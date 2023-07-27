using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[System.Serializable]
public class OnObstaclePooled : UnityEvent<Obstacle> { };

[System.Serializable]
public class OnObstacleSpawned : UnityEvent<Obstacle> { };
public class ObstacleSpawner : MonoBehaviour
{
    public System.Action OnObstaclesSpawn;
    public OnObstacleSpawned EVT_OnObstacleSpawned;
    public OnObstaclePooled EVT_OnObstaclePooled;

    [SerializeField] EquationChecker equationChecker;
    [SerializeField] Obstacle obstaclePrefab;
    [SerializeField] Vector2[] spawnPositions;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] float minSpawnTime, maxSpawnTime;
    [SerializeField] int minAdditiveRange = 1;
    [SerializeField] int maxAdditiveRange = 5;
    [SerializeField] float obstacleOffsetFromPlayer;
    [SerializeField] float spawnTimeDecrement;
    [SerializeField] float distanceTravelledBeforeDecreasingSpawnTime;
    public List<Obstacle> currentSpawnedObjects { get; protected set; } = new List<Obstacle>();
    public List<Obstacle> pooledObjects { get; protected set; } = new List<Obstacle>();

    protected Vector3 SpawnPosition;

    public int totalSpawnsCount { get; private set; } = 0;
    public int totalPooledCount { get; protected set; } = 0;
    public int currentAdditive { get; private set; }
    public Text additiveText;
    private List<Obstacle> currentSpawningObstacles = new List<Obstacle>();

    private List<int> numbersToAssign = new List<int>();
    public int randomAdditive { get; private set; }

    private bool hasDecreasedSpawnTime = false;
    float poolTimer = 20;
    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(SpawnObstacle());
        equationChecker.OnCurrentSumDeducted += DisableCurrentObstacles;
        equationChecker.OnCurrentSumDeducted += RefreshObstacleValues;
        equationChecker.OnCurrentSumAdded += RefreshObstacleValues;
        //StartCoroutine(DestroyOldObstacles());
    }

    private void Update()
    {
        IncreaseSpeedOnDistanceTravelled();
    }
    private void IncreaseSpeedOnDistanceTravelled()
    {
        if (hasDecreasedSpawnTime) return;
        if (playerMovement.distanceTravelled <= this.distanceTravelledBeforeDecreasingSpawnTime) return;
        if ((Mathf.RoundToInt(playerMovement.distanceTravelled)) % distanceTravelledBeforeDecreasingSpawnTime == 0)
            maxSpawnTime -= spawnTimeDecrement;
        if (maxSpawnTime <= minSpawnTime) maxSpawnTime = minSpawnTime;
        hasDecreasedSpawnTime = true;
        StartCoroutine(DecreaseSpawnTimeCooldown());
    }

    IEnumerator DecreaseSpawnTimeCooldown()
    {
        yield return new WaitForSeconds(0.1f);
        hasDecreasedSpawnTime = false;
    }
    private void CreateEquationObstacle()
    {
        currentSpawningObstacles.Clear();
        randomAdditive = Random.Range(minAdditiveRange, maxAdditiveRange+1);
        //fill the list w/ 10 choices of additives to assign
        equationChecker.currentAdditive = randomAdditive;
        //Generating all 3 obstacles
        for (int i = 0; i < 3; i++)
        {
            Obstacle obj;
            if (pooledObjects.Count > 0)
            {
                // get the last pooled object
                obj = pooledObjects[0];
                pooledObjects.RemoveAt(0);
                obj.gameObject.SetActive(true);
                currentSpawnedObjects.Add(obj);

            }
            else
            {
                obj = Instantiate(obstaclePrefab);
                currentSpawnedObjects.Add(obj);
              //  SetPoolingInitializations(obj);
            }
            obj.additive = randomAdditive;
            currentSpawningObstacles.Add(obj);
            totalSpawnsCount += 1;
            //Set Spawn Position
            SpawnPosition = new Vector3(spawnPositions[i].x, spawnPositions[i].y,
                playerMovement.transform.position.z + obstacleOffsetFromPlayer);
            obj.transform.position = SpawnPosition;

            EVT_OnObstacleSpawned.Invoke(obj);
        }

        OnObstaclesSpawn?.Invoke();

        //INITIALIZING VALUES FOR EACH SPAWNED OBJECTS
        int randomIndex = Random.Range(0, currentSpawningObstacles.Count);
        Debug.Log("THE RANDOM INDEX IS " + randomIndex);
        //Setting the correct answer
        currentSpawningObstacles[randomIndex].SetNumberValue(equationChecker.currentSum + randomAdditive);
        //additiveText.text = (equationChecker.currentSum + "+" + randomAdditive.ToString());
        //additiveText.transform.position = new Vector3(0, 1, playerMovement.transform.position.z + obstacleOffsetFromPlayer);
        //Setting wrong answers for the rest of the obstacles
        randomAdditive -= 5;
        for (int i = 0; i < 10; i++)
        {
            randomAdditive += 1;
            numbersToAssign.Add(equationChecker.currentSum + randomAdditive);
        }
        //Remove correct answer from list
        numbersToAssign.Remove(currentSpawningObstacles[randomIndex].numberValue);
        for (int i = 0; i < currentSpawningObstacles.Count; i++)
        {
            if (i == randomIndex) continue;
            else
            {
                int numberToAssignIndex = Random.Range(0, numbersToAssign.Count);
                currentSpawningObstacles[i].SetNumberValue(numbersToAssign[numberToAssignIndex]);
                numbersToAssign.RemoveAt(numberToAssignIndex);
            }
        }

        numbersToAssign.Clear();

        StartCoroutine(DelayedPool());
    }

    private void RefreshObstacleValues(int additive)
    {
        //Refreses all obstacle values whenever currentSum is changed
        for (int i = 0; i < currentSpawnedObjects.Count; i++)
        {
            currentSpawnedObjects[i].SetNumberValue(currentSpawnedObjects[i].numberValue + additive);
        }
    }
    IEnumerator SpawnObstacle()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnTime,maxSpawnTime));
                    CreateEquationObstacle();
                    Debug.Log("Spawn Obstacles");
                   // StartCoroutine(SpawnCoolDown());
                
            
        }
    }
    protected virtual void Pool(Obstacle obstacle)
    {
        obstacle.gameObject.SetActive(false);
        pooledObjects.Add(obstacle);
        currentSpawnedObjects.Remove(obstacle);
        EVT_OnObstaclePooled?.Invoke(obstacle);
    }

    IEnumerator DelayedPool()
    {
        yield return new WaitForSeconds(poolTimer);

        for (int i = 0; i < 3; i++)
        {
            Pool(currentSpawnedObjects[i]);
        }
    }

    public void DisableCurrentObstacles(int additive)
    {
        for (int i = 0; i < currentSpawningObstacles.Count; i++)
        {
            currentSpawningObstacles[i].gameObject.SetActive(false);
        }
        
    }



}
