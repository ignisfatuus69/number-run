using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenu : MonoBehaviour
{
    public Text buyMoneyText;
    public Text buyCoinsText;
    public Text buyRemoveAdsText;
    public Text buyUnlockLevelText;
    public Text buySubscriptionText;
    public GameObject mainMenu;

    //we use OnEnable as before to update texts
    private void OnEnable()
    {
        //the button should contain the reward that user will receive and the price
        //consumable 1 product will be money
        buyMoneyText.text = "Buy " + IAPManager.Instance.GetValue(ShopProductNames.Consumable1) + " Money, " + IAPManager.Instance.GetLocalizedPriceString(ShopProductNames.Consumable1);

        //consumable 2 will be for coins
        buyCoinsText.text = "Buy " + IAPManager.Instance.GetValue(ShopProductNames.Consumable2) + " Coins, " + IAPManager.Instance.GetLocalizedPriceString(ShopProductNames.Consumable2);
        buyRemoveAdsText.text = "Remove ads, " + IAPManager.Instance.GetLocalizedPriceString(ShopProductNames.RemoveAds);

        buyUnlockLevelText.text = "Unlock level 1, " + IAPManager.Instance.GetLocalizedPriceString(ShopProductNames.UnlockLevel);

        buySubscriptionText.text = "Subscribe, " + IAPManager.Instance.GetLocalizedPriceString(ShopProductNames.Subscription);
    }

    // will go back to the main menu
    public void BackButtonPressed()
    {
        //hide this panel and show main manu panel
        gameObject.SetActive(false);
        mainMenu.SetActive(true);
    }

    //make buy money function
    public void BuyMoney()
    {
        IAPManager.Instance.BuyProduct(ShopProductNames.Consumable1, ProductBought);
    }

    //make buy coins method
    public void BuyCoins()
    {
        //the calback method can be the same or can be a new one
        //we will use the same methou for an easy to understand code
        IAPManager.Instance.BuyProduct(ShopProductNames.Consumable2, ProductBought);
    }

    //make remove ads method
    public void RemoveAds()
    {
        IAPManager.Instance.BuyProduct(ShopProductNames.RemoveAds, ProductBought);
    }

    //make unlock level method
    public void UnlockLevel()
    {
        IAPManager.Instance.BuyProduct(ShopProductNames.UnlockLevel, ProductBought);
    }

    //make subscribe method
    public void Subscribe()
    {
        IAPManager.Instance.BuyProduct(ShopProductNames.Subscription, ProductBought);
    }


    //this method will be automatically called when purchase is done
    private void ProductBought(IAPOperationStatus status, string errorMessage, StoreProduct boughtProduct)
    {
        if(status == IAPOperationStatus.Success)
        {
            //check if the product was the consumable 1 product
            if(boughtProduct.productName == ShopProductNames.Consumable1.ToString())
            {
                //we should give the reward to the user
                //we added the value of the in app product to the current balance of the user and saved that value
                Save.SetMoney(boughtProduct.value + Save.GetMoney());
            }
            //check if the product is consumable 2 to give coins
            if(boughtProduct.productName == ShopProductNames.Consumable2.ToString())
            {
                Save.SetCoins(boughtProduct.value + Save.GetCoins());
            }

            //check for remove ads
            if(boughtProduct.productName == ShopProductNames.RemoveAds.ToString())
            {
                //we saved that remove ads was bought
                Save.SetAdAvailable(true);
            }

            //check for unlock level
            if(boughtProduct.productName == ShopProductNames.UnlockLevel.ToString())
            {
                Save.UnlockLevel(true);
            }

            //check for subscription
            if(boughtProduct.productName == ShopProductNames.Subscription.ToString())
            {
                Save.SetSubscription(true);
            }
        }
    }
}
