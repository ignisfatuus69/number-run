using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : ObjectPooler
{
    [SerializeField] GameObject[] CoinArrangementPrefabs;
    protected override void InitilizeBeforeSpawn()
    {
        //Randomly chooses between different arranged coins prefabs
        ObjectToSpawn = CoinArrangementPrefabs[Random.Range(0, CoinArrangementPrefabs.Length)];
    }

    protected override void PostSpawningObjectsInitilizations()
    {
        throw new System.NotImplementedException();
    }

    protected override void SetInstantiateInitializations(GameObject obj)
    {
        throw new System.NotImplementedException();
    }

    protected override void SetPoolingSpawnInitializations(GameObject obj)
    {
        throw new System.NotImplementedException();
    }

    protected override void SetSpawnPosition(GameObject obj)
    {
        throw new System.NotImplementedException();
    }

}
