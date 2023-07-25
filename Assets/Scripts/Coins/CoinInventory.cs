using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinInventory : MonoBehaviour
{
    public int coinAmount { get; private set; }
    public void AddCoin(int additive)
    {
        coinAmount += additive;
    }

    public void ReduceCoin(int subtrahend)
    {
        coinAmount -= subtrahend;
    }
}
