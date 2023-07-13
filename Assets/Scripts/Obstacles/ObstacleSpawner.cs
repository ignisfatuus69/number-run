using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] float spawnTimeInterval=3;
    [SerializeField] Obstacle obstaclePrefab;
    [SerializeField] Vector2[] spawnPositions;
    [SerializeField] Transform playerPosition;
    [SerializeField] Text additiveText;
    [SerializeField] int maxRange = 5;
    [SerializeField] float obstacleOffsetFromPlayer;
    private List<Obstacle> currentObstacles = new List<Obstacle>();
    private int currentSum = 0;
    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(SpawnObstacle());
    }
    private void CreateEquation()
    {
        int randomAdditive = Random.Range(1, maxRange+1);
        //Generating all 3 obstacles
        for (int i = 0; i < 3; i++)
        {
            Obstacle newObstacle = Instantiate(obstaclePrefab, new Vector3(spawnPositions[i].x, spawnPositions[i].y,
                playerPosition.transform.position.z + obstacleOffsetFromPlayer),Quaternion.identity);
            currentObstacles.Add(newObstacle);
        }
        int randomIndex = Random.Range(0, currentObstacles.Count);
        Debug.Log("THE RANDOM INDEX IS " + randomIndex);
        //Setting the correct answer
        currentObstacles[randomIndex].SetNumberValue(randomAdditive);
        //Setting wrong answers for the rest of the obstacles
        for (int i = 0; i < currentObstacles.Count; i++)
        {
            if (currentObstacles[i].numberValue <= 0 && i % 2 == 0) currentObstacles[i].SetNumberValue(randomAdditive - i);
            if (currentObstacles[i].numberValue <= 0 && i % 2 != 0) currentObstacles[i].SetNumberValue(randomAdditive + i);
        }
        currentObstacles.Clear();
        additiveText.text = ("+" + randomAdditive.ToString());
        additiveText.transform.position = new Vector3(0, 1, playerPosition.transform.position.z + obstacleOffsetFromPlayer);
    }
    IEnumerator SpawnObstacle()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTimeInterval);
            CreateEquation();
        }
    }
    //Implement deducing of spawn Interval later
}
