using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    public System.Action<GameObject> OnPowerUpActivated;

    protected virtual void Effect(Collider other)
    {
        OnPowerUpActivated?.Invoke(this.gameObject);
    }

}
