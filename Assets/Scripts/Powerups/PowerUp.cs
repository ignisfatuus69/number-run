using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    [SerializeField] public float duration;
    protected float currentDuration;
    public System.Action<GameObject> OnPowerUpActivated;

    protected virtual void Effect(Collider other)
    {
        OnPowerUpActivated?.Invoke(this.gameObject);
        other.GetComponent<PowerUpTracker>().AddPowerUp(this);
    }

    public void ResetDuration()
    {
        Debug.Log("reset duration");
        currentDuration = duration;
        Debug.Log(currentDuration);
    }
}
