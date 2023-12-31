using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinInventory : MonoBehaviour
{
    [SerializeField] int coinMultiplier= 1;
    [SerializeField] AudioClip coinSFX;
    public int coinAmount { get; private set; }
    public void AddCoin(int additive)
    {
        coinAmount += additive * coinMultiplier;
        AudioManager.Instance.PlayOneShot(coinSFX);
    }

    public void ReduceCoin(int subtrahend)
    {
        coinAmount -= subtrahend;
    }
}
