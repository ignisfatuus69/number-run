using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : ObjectPooler
{
    public System.Action OnPowerUpSpawned;
    public System.Action OnPowerUpCantSpawn;

    [SerializeField] Transform playerTransform;
    private float currentPowerUpForwardSpawnOffset;
    private float currentSpawnIntervalTime=10;
    protected bool isSpawningPowerUp = true;
    [SerializeField] float poolTimer = 20;
    public void CreatePowerUp()
    {
        if (isSpawningPowerUp)
        {
            Debug.Log("spawned powerup");
            Spawn();
            return;
        }
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
        if (currentSpawnedObjects?.Count > 0)
        {
            for (int i = 0; i < SpawnCount; i++)
            {
                Pool(currentSpawnedObjects[0]);
            }
            Debug.Log("Pooling object");
        }
    }
    protected override void SetPoolingSpawnInitializations(GameObject obj)
    {
       // throw new System.NotImplementedException();
    }

    protected virtual void SetConditionForSpawning()
    {
  //      SetSpawnChance();
    }

    public void SpawnPowerUp()
    {
        SetConditionForSpawning();
        if (!isSpawningPowerUp)
        {
            OnPowerUpCantSpawn?.Invoke();
            Debug.Log("Cant spawn power up, choosing another one");
            return;
        }
        StartCoroutine(SpawnPowerUpInSeconds());
    }
    private IEnumerator SpawnPowerUpInSeconds()
    {
        Debug.Log("spawning power up");
        yield return new WaitForSeconds(Random.Range(minTimeSpawnInterval, maxTimeSpawnInterval));
        CreatePowerUp();
        OnPowerUpSpawned?.Invoke();
    }
}
