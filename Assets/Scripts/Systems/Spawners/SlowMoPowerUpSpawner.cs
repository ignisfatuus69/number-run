using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMoPowerUpSpawner : PowerUpSpawner
{
    
    [SerializeField] EquationChecker equationCheckerObj;
    [SerializeField] PlayerMovement playerMovement;
    void Start()
    {
        StartCoroutine(SpawnPowerUp());
    }
    protected override void SetInstantiateInitializations(GameObject obj)
    {
        SlowMotionPowerUp slowMoPowerUp = obj.GetComponent<SlowMotionPowerUp>();
        slowMoPowerUp.equationCheckerObj = this.equationCheckerObj;
        slowMoPowerUp.playerMovement = this.playerMovement;
        slowMoPowerUp.OnSlowMotionEnded += Pool;
    }

    IEnumerator SpawnPowerUp()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minTimeSpawnInterval, maxTimeSpawnInterval));
            //temporarily adding a 50% chance to spawn to not spawn too many powerups
            int randomNumber = Random.Range(0, 2);
            if (randomNumber==0) CreatePowerUp();
        }
    }
}
