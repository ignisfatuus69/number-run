using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerUpSpawner : PowerUpSpawner
{
    [SerializeField] EquationChecker equationCheckerObj;
    [SerializeField] GameObject shieldObj;

    private void Start()
    {
        StartCoroutine(SpawnPowerUp());
    }
    protected override void SetInstantiateInitializations(GameObject obj)
    {
        ShieldPowerUp shieldPowerup = obj.GetComponent<ShieldPowerUp>();
        shieldPowerup.equationCheckerObj = this.equationCheckerObj;
        shieldPowerup.shieldObj = this.shieldObj;
        shieldPowerup.OnShieldEnded += Pool;
    }

    IEnumerator SpawnPowerUp()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minTimeSpawnInterval, maxTimeSpawnInterval));
            //temporarily adding a 50% chance to spawn to not spawn too many powerups
            int randomNumber = Random.Range(0, 2);
            if (randomNumber == 0) CreatePowerUp();
        }
    }
}
