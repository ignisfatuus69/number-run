﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class Coin : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 0.5f;
    public System.Action<GameObject> OnHitPlayer;
    public int coinValue = 1;
    //public Score ScoreObj;

    private void Update()
    {
        gameObject.transform.Rotate(0, 0, rotationSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Add score");
        CoinInventory playerCoinInventory = other?.GetComponent<CoinInventory>();
        if (playerCoinInventory == null) return;

        playerCoinInventory.AddCoin(coinValue);
        this.gameObject.SetActive(false);
    }
}
