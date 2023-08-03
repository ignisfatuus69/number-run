using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPowerUpSpawner : PowerUpSpawner
{
    [SerializeField] ObstacleSpawner obstacleSpawner;
    [SerializeField] EquationChecker equationChecker;
    [SerializeField] float currentSumBeforeSpawning = 20;
    // Start is called before the first frame update
    void Start()
    {
         obstacleSpawner.OnObstaclesSpawn +=CreatePowerUp;
    }

    protected override void SetConditionForSpawning()
    {
        base.SetConditionForSpawning();

        //add a second condition so no reset powerup spawns when the current sum is 20
        if (equationChecker.currentSum < currentSumBeforeSpawning) isSpawningPowerUp = false;
    }

}
