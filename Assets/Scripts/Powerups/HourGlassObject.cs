using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HourGlassObject : MonoBehaviour
{
    [SerializeField] PowerUpTracker powerUpTrackerObj;
    [SerializeField] GameObject hourGlass;
    // Start is called before the first frame update
    void Start()
    {
        powerUpTrackerObj.OnPowerUpRemoved += SetInactive;
        powerUpTrackerObj.OnPowerUpAdded += SetActive;
    }

    private void SetActive(PowerUp obj)
    {
        Debug.Log("This powerup is" + obj.GetType().ToString());
        Debug.Log(powerUpTrackerObj.ContainsPowerUp("SlowMotionPowerUp"));
        if (powerUpTrackerObj.ContainsPowerUp("SlowMotionPowerUp")) hourGlass.gameObject.SetActive(true);
    }

    void SetInactive(PowerUp powerup)
    {
        if (powerup.GetType().ToString() == "SlowMotionPowerUp") hourGlass.gameObject.SetActive(false);
    }
}
