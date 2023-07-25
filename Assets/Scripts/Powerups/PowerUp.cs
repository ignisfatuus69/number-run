using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    System.Action<GameObject> OnGameObjectCollision;

    protected abstract void Effect(Collider other);

}
