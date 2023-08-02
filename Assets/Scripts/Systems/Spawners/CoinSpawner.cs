using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : ObjectPooler
{
    [SerializeField] GameObject[] CoinArrangementPrefabs;
    [SerializeField] Transform playerTransform;
    [SerializeField] int minXPosition, maxXPosition;
    [SerializeField] float poolTimer = 20;


    private void Start()
    {
        StartCoroutine(SpawnInSeconds());
    }
    protected override void InitilizeBeforeSpawn()
    {
        //Randomly chooses between different arranged coins prefabs
        ObjectToSpawn = CoinArrangementPrefabs[Random.Range(0, CoinArrangementPrefabs.Length)];
    }

    protected override void PostSpawningObjectsInitilizations()
    {
        StartCoroutine(DelayedPool());
    }

    protected override void SetInstantiateInitializations(GameObject obj)
    {
      //  Coin newCoin = obj.GetComponent<Coin>();
      //  newCoin.OnHitPlayer += Pool;
    }

    protected override void SetPoolingSpawnInitializations(GameObject obj)
    {
    }

    protected override void SetSpawnPosition(GameObject obj)
    {
        currentSpawnPosition = new Vector3(Random.Range(minXPosition, maxXPosition + 1), 0.3f, 
            playerTransform.position.z + Random.Range(minSpawnPositionForwardOffset, maxSpawnPositionForwardOffset));
        obj.transform.position = currentSpawnPosition;
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

    IEnumerator SpawnInSeconds()
    {
        while (true)
        {
            float randomSpawnTime = Random.Range(minTimeSpawnInterval, maxTimeSpawnInterval);
            yield return new WaitForSeconds(randomSpawnTime);
            Spawn();
        }
    }
}
