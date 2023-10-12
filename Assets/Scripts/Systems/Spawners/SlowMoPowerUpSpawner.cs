using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMoPowerUpSpawner : PowerUpSpawner
{
    
    [SerializeField] EquationChecker equationCheckerObj;
    [SerializeField] PlayerMovement playerMovement;
    protected override void SetInstantiateInitializations(GameObject obj)
    {
        SlowMotionPowerUp slowMoPowerUp = obj.GetComponent<SlowMotionPowerUp>();
        slowMoPowerUp.equationCheckerObj = this.equationCheckerObj;
        slowMoPowerUp.playerMovement = this.playerMovement;
        slowMoPowerUp.OnSlowMotionEnded += Pool;
    }

}
