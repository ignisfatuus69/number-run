using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : ObjectPooler
{

    public GameObject[] tilePrefabs;
    public float zSpawnOffset = 0;
    public float tileLength = 8;
    public int numberOfTiles = 5;
    public Transform playerTransform;
    [SerializeField] float poolTimer = 20;
    private bool hasSpawned = false;
    // Update is called once per frame
    void Update()
    {
        if (hasSpawned) return;
        if (playerTransform.position.z - 35 > zSpawnOffset - (numberOfTiles * tileLength))
        {
            Debug.Log("Spawning Tile");
            Spawn();
            hasSpawned = true;
            StartCoroutine(SpawnCooldown());
        }
    }

    protected override void InitilizeBeforeSpawn()
    {
        //Randomize selection of tile object before spawning
        if (pooledObjects.Count <= 0)
        {
            ObjectToSpawn = tilePrefabs[0];
            return;
        }
        else ObjectToSpawn = tilePrefabs[(Random.Range(0, tilePrefabs.Length))];
    }

    protected override void SetSpawnPosition(GameObject obj)
    {
        obj.transform.position = currentSpawnPosition;
        currentSpawnPosition = transform.forward * zSpawnOffset;
    }

    protected override void SetPoolingInitializations(GameObject obj)
    {
        zSpawnOffset += tileLength;
        StartCoroutine(DelayedPool());
    }

    protected override void PostSpawningObjectsInitilizations()
    {
        hasSpawned = false;
        //DelayedPool();
    }

    IEnumerator SpawnCooldown()
    {
        yield return new WaitForSeconds(1);
        hasSpawned = false;

    }
    IEnumerator DelayedPool()
    {
        yield return new WaitForSeconds(poolTimer);
        Pool(currentSpawnedObjects[0]);
        Debug.Log("Pooling object");
        
    }

}
