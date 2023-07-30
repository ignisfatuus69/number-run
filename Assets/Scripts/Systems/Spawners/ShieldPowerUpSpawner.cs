using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerUpSpawner : PowerUpSpawner
{
    [SerializeField] ObstacleSpawner obstacleSpawner;
    [SerializeField] EquationChecker equationCheckerObj;
    [SerializeField] GameObject shieldObj;
    // Start is called before the first frame update
    void Start()
    {
        obstacleSpawner.OnObstaclesSpawn += CreatePowerUp;
    }
    protected override void SetPoolingInitializations(GameObject obj)
    {
        ShieldPowerUp shieldPowerup = obj.GetComponent<ShieldPowerUp>();
        shieldPowerup.equationCheckerObj = this.equationCheckerObj;
        shieldPowerup.shieldObj = this.shieldObj;
        shieldPowerup.OnShieldEnded += Pool;
    }
}
