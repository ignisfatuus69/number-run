using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] float spawnTimeInterval=3;
    [SerializeField] Obstacle obstaclePrefab;
    [SerializeField] Vector2[] spawnPositions;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] Text additiveText;
    [SerializeField] int maxRange = 5;
    [SerializeField] float obstacleOffsetFromPlayer;
    private bool isOnSpawnCooldown = false;
    private List<Obstacle> currentSpawningObstacles = new List<Obstacle>();
    private List<Obstacle> allSpawnedObstacles = new List<Obstacle>();
    private int currentSum = 0;
    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(SpawnObstacle());
        //StartCoroutine(DestroyOldObstacles());
    }
    private void CreateEquationObstacle()
    {
        int randomAdditive = Random.Range(1, maxRange+1);
        //Generating all 3 obstacles
        for (int i = 0; i < 3; i++)
        {
            Obstacle newObstacle = Instantiate(obstaclePrefab, new Vector3(spawnPositions[i].x, spawnPositions[i].y,
                playerMovement.transform.position.z + obstacleOffsetFromPlayer),Quaternion.identity);
            currentSpawningObstacles.Add(newObstacle);
            allSpawnedObstacles.Add(newObstacle);
        }
        //deleting old obstacles
        if (allSpawnedObstacles.Count >= 9)
        {
            for (int x = 0; x < 3; x++)
            {
                Destroy(allSpawnedObstacles[0].gameObject);
                allSpawnedObstacles.Remove(allSpawnedObstacles[0]);
            }
        }
        int randomIndex = Random.Range(0, currentSpawningObstacles.Count);
        Debug.Log("THE RANDOM INDEX IS " + randomIndex);
        //Setting the correct answer
        currentSpawningObstacles[randomIndex].SetNumberValue(randomAdditive);
        //Setting wrong answers for the rest of the obstacles
        for (int i = 0; i < currentSpawningObstacles.Count; i++)
        {
            if (currentSpawningObstacles[i].numberValue <= 0 && i % 2 == 0) currentSpawningObstacles[i].SetNumberValue(randomAdditive - i);
            if (currentSpawningObstacles[i].numberValue <= 0 && i % 2 != 0) currentSpawningObstacles[i].SetNumberValue(randomAdditive + i);
        }
        currentSpawningObstacles.Clear();
        additiveText.text = ("+" + randomAdditive.ToString());
        additiveText.transform.position = new Vector3(0, 1, playerMovement.transform.position.z + obstacleOffsetFromPlayer);
    }

    IEnumerator SpawnObstacle()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            Debug.Log(Mathf.RoundToInt(playerMovement.distanceTravelled));
            if (!playerMovement.isMoving || isOnSpawnCooldown) yield return null;
            else
            {
                if (Mathf.RoundToInt(playerMovement.distanceTravelled) % 10 == 0)
                {
                    CreateEquationObstacle();
                    Debug.Log("Spawn Obstacles");
                    isOnSpawnCooldown = true;
                    StartCoroutine(SpawnCoolDown());
                }
            }
        }
    }

    //adding a spawncooldown so we don't get multiple obstacles spawned in one spot
    IEnumerator SpawnCoolDown()
    {
        yield return new WaitForSeconds(0.5f);
        isOnSpawnCooldown = false;
    }

    //IEnumerator DestroyOldObstacles()
    //{
    //    while (true)
    //    {
    //        if (!playerMovement.isMoving) yield return null;
    //        else
    //        {
    //            Debug.Log("hello");
    //            yield return new WaitForSeconds(10f);
    //            //for (int i = 0; i < allSpawnedObstacles.Count; i++)
    //            //{
    //            //    Destroy(allSpawnedObstacles[i].gameObject);
    //            //}
    //            //allSpawnedObstacles.Clear();
    //        }
    //    }
    //}
    //Implement deducing of spawn Interval later
}
