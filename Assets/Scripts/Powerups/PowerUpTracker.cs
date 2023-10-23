using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PowerUpTracker : MonoBehaviour
{
    public System.Action<PowerUp> OnPowerUpAdded;
    public System.Action<PowerUp> OnPowerUpRemoved;
    public System.Action<PowerUp> OnResetDuration;
    private List<PowerUp> powerUpList = new List<PowerUp>();
    
    
    public void AddPowerUp(PowerUp powerUpToAdd)
    {
        Debug.Log(powerUpToAdd.name);
        for (int i = 0; i < powerUpList.Count; i++)
        {
            if (ContainsPowerUp(powerUpToAdd.GetType().ToString()))
            {
                powerUpList[i].ResetDuration();
                OnResetDuration?.Invoke(powerUpToAdd);
                return;
            }
        }
        powerUpList.Add(powerUpToAdd);
        OnPowerUpAdded?.Invoke(powerUpToAdd);
    }

    public void RemovePowerUp(PowerUp powerUpToRemove)
    {
        Debug.Log("invoke remove powerup");
        powerUpList.Remove(powerUpToRemove);
        OnPowerUpRemoved?.Invoke(powerUpToRemove);
    }

    public bool ContainsPowerUp(string powerUpName)
    {
        for (int i = 0; i < powerUpList.Count; i++)
        {
            if (powerUpName == powerUpList[i].GetType().ToString())
            {
                return true;
            }
        }
        return false;
    }

    public PowerUp GetPowerUp(string powerUpName)
    {
        if (!ContainsPowerUp(powerUpName)) return null;
        for (int i = 0; i < powerUpList.Count; i++)
        {
            if (powerUpName == powerUpList[i].GetType().ToString())
            {
                return powerUpList[i];
            }
        }
        return null;
    }
}
