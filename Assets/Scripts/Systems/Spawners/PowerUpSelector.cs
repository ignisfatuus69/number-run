using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PowerUpSelector : MonoBehaviour
{
    [SerializeField] PowerUpSpawner[] powerUpSpawners;
    // Start is called before the first frame update
    void Start()
    {
        SpawnForNextSpawner();
        for (int i = 0; i < powerUpSpawners.Length; i++)
        {
            powerUpSpawners[i].OnPowerUpSpawned += SpawnForNextSpawner;
            powerUpSpawners[i].OnPowerUpCantSpawn += SpawnForNextSpawner;
        }
    }

    void SpawnForNextSpawner()
    {
        int randomNumber = UnityEngine.Random.Range(0, 3);
        powerUpSpawners[randomNumber].SpawnPowerUp();

    }
}
