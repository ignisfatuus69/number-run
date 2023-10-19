using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PowerUpSelector : MonoBehaviour
{
    [SerializeField] PowerUpSpawner[] powerUpSpawners;
    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i < powerUpSpawners.Length; i++)
        {
            powerUpSpawners[i].OnPowerUpSpawned += SpawnForNextSpawner;
            powerUpSpawners[i].OnPowerUpCantSpawn += SpawnShieldOrSlowmo;
        }
        SpawnForNextSpawner();
    }

    void SpawnForNextSpawner()
    {
        Debug.Log(powerUpSpawners.Length);
        int randomNumber = UnityEngine.Random.Range(0, powerUpSpawners.Length);
        powerUpSpawners[randomNumber].SpawnPowerUp();
        Debug.Log("The selected random number for powerup is:" + randomNumber);
    }
    void SpawnShieldOrSlowmo()
    {
        Debug.Log("Spawning another kind of power up instead ");
        //this makes sure that the next one ensures we spawn a power up cuz we already failed spawning at index 0
        int randomNumber = UnityEngine.Random.Range(1, 3);
        powerUpSpawners[randomNumber].SpawnPowerUp();
    }
}
