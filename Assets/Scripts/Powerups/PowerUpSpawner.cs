using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [SerializeField] float minTimeSpawnInterval = 10;
    [SerializeField] float maxTimeSpawnInterval = 20;
    [SerializeField] ResetPowerUp powerUpPrefab;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] float obstacleOffsetFromPlayer;
    private float currentSpawnIntervalTime=10;
    private bool isOnSpawnCooldown = false;

    private int currentSum = 0;
    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(SpawnObstacle());
    }
    
    public void CreatePowerup()
    {
        Debug.Log("test");
        ResetPowerUp newObstacle = Instantiate(powerUpPrefab, new Vector3(Random.Range(-1.5f,1.5f), 0.5f,
     playerMovement.transform.position.z + obstacleOffsetFromPlayer), Quaternion.identity);
        currentSpawnIntervalTime = Random.Range(minTimeSpawnInterval, maxTimeSpawnInterval + 1);
    }
    IEnumerator SpawnObstacle()
    {
        while (true)
        {
            yield return new WaitForSeconds(currentSpawnIntervalTime);
            Debug.Log(Mathf.RoundToInt(playerMovement.distanceTravelled));
           
            
                
                    //Spawn
                    Debug.Log("Spawn PowerUp");
                CreatePowerup();
                    isOnSpawnCooldown = true;
                    StartCoroutine(SpawnCoolDown());
                
            
        }
    }

    //adding a spawncooldown so we don't get multiple obstacles spawned in one spot
    IEnumerator SpawnCoolDown()
    {
        yield return new WaitForSeconds(0.5f);
        isOnSpawnCooldown = false;
    }
}
