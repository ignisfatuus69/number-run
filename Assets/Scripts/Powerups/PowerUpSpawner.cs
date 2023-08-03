using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : ObjectPooler
{
    [SerializeField] Transform playerTransform;
    private float currentPowerUpForwardSpawnOffset;
    private float currentSpawnIntervalTime=10;
    protected bool isSpawningPowerUp = false;
    [SerializeField] float poolTimer = 20;
    public void CreatePowerUp()
    {
        SetConditionForSpawning();

        if (isSpawningPowerUp)
        {
            Debug.Log("Spawning powerup");
            Spawn();
            isSpawningPowerUp = false;
            return;
        }
        else Debug.Log("Not spawning powerup");
    }

    protected override void SetSpawnPosition(GameObject obj)
    {
        currentSpawnPosition = new Vector3(Random.Range(-2, 2 + 1), 0.25f, (playerTransform.position.z + currentPowerUpForwardSpawnOffset));
        obj.transform.position = currentSpawnPosition;
    }

    protected override void SetInstantiateInitializations(GameObject obj)
    {
        PowerUp newPowerUp = obj.GetComponent<PowerUp>();
        newPowerUp.OnPowerUpActivated += Pool;
    }

    protected override void PostSpawningObjectsInitilizations()
    {
        currentSpawnIntervalTime = Random.Range(minTimeSpawnInterval, maxTimeSpawnInterval + 1);
        StartCoroutine(DelayedPool());
    }

    protected override void InitilizeBeforeSpawn()
    {
        currentPowerUpForwardSpawnOffset = Random.Range(minSpawnPositionForwardOffset, maxSpawnPositionForwardOffset);
    }

    IEnumerator DelayedPool()
    {
        yield return new WaitForSeconds(poolTimer);
        for (int i = 0; i < SpawnCount; i++)
        {
            Pool(currentSpawnedObjects[0]);
        }
        Debug.Log("Pooling object");

    }
    protected override void SetPoolingSpawnInitializations(GameObject obj)
    {
       // throw new System.NotImplementedException();
    }

    protected virtual void SetConditionForSpawning() 
    {
        int randomNumber = Random.Range(0, 2);
        if (randomNumber == 0) isSpawningPowerUp = true;
        else isSpawningPowerUp = false;
    }
}
