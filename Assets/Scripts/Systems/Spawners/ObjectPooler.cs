using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OnObjectPooled : UnityEvent<GameObject> { };

[System.Serializable]
public class OnObjectSpawned : UnityEvent<GameObject> { };

public abstract class ObjectPooler : MonoBehaviour
{
    public OnObjectSpawned EVT_OnObjectSpawned;
    public OnObjectPooled EVT_OnObjectPooled;

    [SerializeField] protected GameObject ObjectToSpawn;
    [SerializeField] protected int SpawnCount = 1;

    public List<GameObject> currentSpawnedObjects { get; protected set; } = new List<GameObject>();
    public List<GameObject> pooledObjects { get; protected set; } = new List<GameObject>();

    protected Vector3 currentSpawnPosition;

    public int totalSpawnsCount { get; private set; } = 0;
    public int totalPooledCount { get; protected set; } = 0;

    [SerializeField] protected float minTimeSpawnInterval;
    [SerializeField] protected float maxTimeSpawnInterval;
    [SerializeField] protected float minSpawnPositionForwardOffset;
    [SerializeField] protected float maxSpawnPositionForwardOffset;
    public virtual void Spawn()
    {
        InitilizeBeforeSpawn();
        for (int i = 0; i < SpawnCount; i++)
        {

            // Spawn the object. If we have an object in the pool, use that instead. Else, instantiate.
            GameObject obj;
            if (pooledObjects.Count > 0)
            {
                // get the last pooled object
                obj = pooledObjects[0];
                pooledObjects.RemoveAt(0);
                obj.SetActive(true);
                currentSpawnedObjects.Add(obj);
                SetPoolingSpawnInitializations(obj);

            }
            else
            {
                obj = Instantiate(ObjectToSpawn);
                currentSpawnedObjects.Add(obj);
                SetInstantiateInitializations(obj);
            }

            totalSpawnsCount += 1;
            //Set Spawn Position
            SetSpawnPosition(obj);

            EVT_OnObjectSpawned.Invoke(obj);
        }
        PostSpawningObjectsInitilizations();
    }

    protected abstract void InitilizeBeforeSpawn();
    protected abstract void SetSpawnPosition(GameObject obj);
    protected abstract void SetInstantiateInitializations(GameObject obj);

    protected abstract void SetPoolingSpawnInitializations(GameObject obj);

    protected abstract void PostSpawningObjectsInitilizations();
    protected virtual void Pool(GameObject obj)
    {
        pooledObjects.Add(obj.gameObject);
        currentSpawnedObjects.Remove(obj.gameObject);
        EVT_OnObjectPooled?.Invoke(obj);
        obj.gameObject.SetActive(false);
    }



}
