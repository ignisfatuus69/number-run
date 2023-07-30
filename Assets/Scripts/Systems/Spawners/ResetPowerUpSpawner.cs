using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPowerUpSpawner : PowerUpSpawner
{
    [SerializeField] ObstacleSpawner obstacleSpawner;
    // Start is called before the first frame update
    void Start()
    {
         obstacleSpawner.OnObstaclesSpawn +=CreatePowerUp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
