using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    public Text totalMoneyText;
    public Text totalCoinsText;
    public Text adAvailableText;
    public Text subscribedText;
    public Button openLevelButton;
    public GameObject shop;

    private void Start()
    {
        //initialize the EasyIAP plugin at the very beggining of your app
        IAPManager.Instance.InitializeIAPManager(InitComplete);
    }

    //this method will be called when init is complete
    private void InitComplete(IAPOperationStatus status, string errorMessage, List<StoreProduct> allStoreProducts)
    {
        //check is initialization is success
        if (status == IAPOperationStatus.Success)
        {
            //here we should check if user has already boucht some non consumable products or subscription

            //check for remove ads
            //we iterate through all products and check for remove ads product
            for (int i = 0; i < allStoreProducts.Count; i++)
            {
                if (allStoreProducts[i].productName == ShopProductNames.RemoveAds.ToString())
                {
                    //we set the remove ads variable
                    //we set this only if product was bought
                    if (allStoreProducts[i].active == true)
                    {
                        Save.SetAdAvailable(true);
                    }
                }

                //check for unlock level
                if(allStoreProducts[i].productName == ShopProductNames.UnlockLevel.ToString())
                {
                    //check if it was bought
                    if(allStoreProducts[i].active)
                    {
                        //set the variable
                        Save.UnlockLevel(true);
                    }
                }


                //check for subscription
                if(allStoreProducts[i].productName == ShopProductNames.Subscription.ToString())
                {
                    if(allStoreProducts[i].active)
                    {
                        Save.SetSubscription(true);
                    }
                    else
                    {
                        //subscription was not bought, or it might be bought and expired so we have to disable it
                        Save.SetSubscription(false);
                    }
                }
            }
            //after the init is done we shoul refresh our interface
            RefreshInterface();
        }
        //if initialization was not successful we log an error message
        else
        {
            Debug.Log("Initialization failed " + errorMessage);
        }
    }

    private void OnEnable()
    {
        //we use on enable instead of start because we will enable and disable this gameobject later
        //every time this object is enabled it will read the saved properties we created early
        //and it will update the interface accordingly
        RefreshInterface();
    }


    //make a function for refresn
    void RefreshInterface()
    {
        totalMoneyText.text = "Total Money: " + Save.GetMoney();
        totalCoinsText.text = "Total Coins: " + Save.GetCoins();
        adAvailableText.text = "Remove ads bought: " + Save.IsAdAvailable();
        subscribedText.text = "Subscription active " + Save.IsSubscriptionBought();
        if (Save.UnlockLevelWasBought() == false)
        {
            openLevelButton.interactable = false;
        }
        else
        {
            openLevelButton.interactable = true;
        }
    }


    //open shop method
    public void OpenShop()
    {
        gameObject.SetActive(false);
        shop.SetActive(true);
    }
}
