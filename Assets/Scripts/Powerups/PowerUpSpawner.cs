using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [SerializeField] float minTimeSpawnInterval = 10;
    [SerializeField] float maxTimeSpawnInterval = 20;
    [SerializeField] ResetPowerUp powerUpPrefab;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] float powerUpOffset;
    [SerializeField] ObstacleSpawner obstacleSpawner;
    private float currentSpawnIntervalTime=10;
    private bool isOnSpawnCooldown = false;
    private bool isSpawningPowerUp = false;

    private int currentSum = 0;
    // Start is called before the first frame update
    private void Start()
    {
        obstacleSpawner.OnObstaclesSpawn += SpawnPowerUpOnObstacleSpawn;
    }
    
    public void SpawnPowerUpOnObstacleSpawn()
    {
        int randomNumber = Random.Range(0, 2);
        if (randomNumber == 0) isSpawningPowerUp = true;
        else isSpawningPowerUp = false;

        if (isSpawningPowerUp)
        {
            StartCoroutine(SpawnObstacle());
            isSpawningPowerUp = false;
        }
    }
    public void CreatePowerup()
    {
        powerUpOffset = Random.Range(7, 16);
        Debug.Log("test");
        ResetPowerUp newPowerUp = Instantiate(powerUpPrefab, new Vector3(obstacleSpawner.nextSpawnPosition.x,obstacleSpawner.nextSpawnPosition.y, (obstacleSpawner.nextSpawnPosition.z + powerUpOffset)),Quaternion.identity);
        currentSpawnIntervalTime = Random.Range(minTimeSpawnInterval, maxTimeSpawnInterval + 1);
    }
    IEnumerator SpawnObstacle()
    {
            yield return new WaitForSeconds(currentSpawnIntervalTime);
            Debug.Log(Mathf.RoundToInt(playerMovement.distanceTravelled));

                    //Spawn
                    Debug.Log("Spawn PowerUp");
                CreatePowerup();
                    isOnSpawnCooldown = true;
                    StartCoroutine(SpawnCoolDown());

    }

    //adding a spawncooldown so we don't get multiple obstacles spawned in one spot
    IEnumerator SpawnCoolDown()
    {
        yield return new WaitForSeconds(0.5f);
        isOnSpawnCooldown = false;
    }
}
