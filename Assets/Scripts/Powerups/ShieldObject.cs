using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldObject : MonoBehaviour
{
    [SerializeField] PowerUpTracker powerUpTrackerObj;
    [SerializeField] FadeOutObject fadeOutObj;
    [SerializeField] GameObject shieldObj;
    // Start is called before the first frame update
    void Start()
    {
        powerUpTrackerObj.OnPowerUpRemoved += SetInactive;
        powerUpTrackerObj.OnPowerUpAdded += SetActive;
    }

    private void SetActive(PowerUp obj)
    {
        Debug.Log(powerUpTrackerObj.ContainsPowerUp("ShieldPowerUp"));
        if (powerUpTrackerObj.ContainsPowerUp("ShieldPowerUp")) shieldObj.gameObject.GetComponent<MeshRenderer>().enabled = (true);
    }

    void SetInactive(PowerUp powerup)
    {
        if (powerup.GetType().ToString() == "ShieldPowerUp") shieldObj.gameObject.GetComponent<MeshRenderer>().enabled = (false);
    }
}
