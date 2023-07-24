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

    public OnObstacleSpawned EVT_OnObstacleSpawned;
    public OnObstaclePooled EVT_OnObjectPooled;

    [SerializeField] protected float poolTimer;

    public List<Obstacle> currentSpawnedObjects { get; protected set; } = new List<Obstacle>();
    public List<Obstacle> pooledObjects { get; protected set; } = new List<Obstacle>();

    [SerializeField] protected Vector3 SpawnPosition;

    public int totalSpawnsCount { get; private set; } = 0;
    public int totalPooledCount { get; protected set; } = 0;


    public System.Action OnObstaclesSpawn; 
    [SerializeField] EquationChecker equationChecker;
    [SerializeField] Obstacle obstaclePrefab;
    [SerializeField] Vector2[] spawnPositions;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] int maxRange = 5;
    [SerializeField] float obstacleOffsetFromPlayer;
    public Text additiveText;
    private List<Obstacle> currentSpawningObstacles = new List<Obstacle>();
    private List<int> numbersToAssign = new List<int>();
    public int randomAdditive { get; private set; }
    public Vector3 nextPowerupPosition { get; private set; }
    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(SpawnObstacle());
        //StartCoroutine(DestroyOldObstacles());
    }
    private void CreateEquationObstacle()
    {
         randomAdditive = Random.Range(1, maxRange+1);
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

        nextPowerupPosition =  new Vector3(spawnPositions[1].x, spawnPositions[1].y,
                playerMovement.transform.position.z + obstacleOffsetFromPlayer);

        OnObstaclesSpawn.Invoke();


        //INITIALIZING VALUES FOR EACH SPAWNED OBJECTS
        int randomIndex = Random.Range(0, currentSpawningObstacles.Count);
        Debug.Log("THE RANDOM INDEX IS " + randomIndex);
        //Setting the correct answer
        currentSpawningObstacles[randomIndex].SetNumberValue(equationChecker.currentSum + randomAdditive);
        additiveText.text = (equationChecker.currentSum + "+" + randomAdditive.ToString());
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

        currentSpawningObstacles.Clear();
        numbersToAssign.Clear();

        StartCoroutine(DelayedPool());
    }

    IEnumerator SpawnObstacle()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(5,10));
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
    }

    //adding a spawncooldown so we don't get multiple obstacles spawned in one spot
    IEnumerator DelayedPool()
    {
        yield return new WaitForSeconds(20);

        for (int i = 0; i < 3; i++)
        {
            Pool(currentSpawnedObjects[i]);
        }
      //  isOnSpawnCooldown = false;
    }



}
