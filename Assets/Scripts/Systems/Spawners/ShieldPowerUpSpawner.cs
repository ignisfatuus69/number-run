using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerUpSpawner : PowerUpSpawner
{
    [SerializeField] EquationChecker equationCheckerObj;
    [SerializeField] GameObject shieldObj;
    protected override void SetInstantiateInitializations(GameObject obj)
    {
        ShieldPowerUp shieldPowerup = obj.GetComponent<ShieldPowerUp>();
        shieldPowerup.equationCheckerObj = this.equationCheckerObj;
        shieldPowerup.OnShieldEnded += Pool;
    }


}

