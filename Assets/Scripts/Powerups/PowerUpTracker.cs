using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PowerUpTracker : MonoBehaviour
{
    public System.Action<PowerUp> OnPowerUpAdded;
    public System.Action<PowerUp> OnPowerUpRemoved;
    private List<PowerUp> powerUpList = new List<PowerUp>();
    
    public void AddPowerUp(PowerUp powerUpToAdd)
    {
        OnPowerUpAdded?.Invoke(powerUpToAdd);
        for (int i = 0; i < powerUpList.Count; i++)
        {
            if (powerUpToAdd.GetComponent<PowerUp>().ToString() == powerUpList[i].GetComponent<PowerUp>().ToString())
            {
                powerUpList[i].ResetDuration();
                return;
            }
        }
        powerUpList.Add(powerUpToAdd);
    }

    public void RemovePowerUp(PowerUp powerUpToRemove)
    {
        powerUpList.Remove(powerUpToRemove);
        OnPowerUpRemoved?.Invoke(powerUpToRemove);
    }
}
