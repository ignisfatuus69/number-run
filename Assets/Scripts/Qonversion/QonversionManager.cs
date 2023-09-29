using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QonversionUnity;
using TMPro;
using UnityEngine.UI;

public class QonversionManager : MonoBehaviour
{
  
    [SerializeField] string[] productIds;
    public Text premiumText;
    //public Button premiumButton;
    //public Button chocolateCookieButton;
    //public InterstetialAds interstitialAdExample;
    //public GameManager gameManager;

    void Awake()
    {
        QonversionConfigBuilder qConfigBuilder = new QonversionConfigBuilder("VSOFFsHF", LaunchMode.Analytics);
        QonversionConfig newQConfig= qConfigBuilder.Build();
        Qonversion.Initialize(newQConfig);
        Debug.Log(newQConfig.ProjectKey);
        Qonversion.GetSharedInstance().SyncPurchases();
    }
    // Start is called before the first frame update
    void Start()
    {
        ShowProducts();
    }



    public void ShowProducts()
    {
        try
        {
            Qonversion.GetSharedInstance().Products((products, error) =>
                {
                    Debug.Log("testtesttesttest");
                    if (error == null)
                    {
                        for (int i = 0; i < productIds.Length; i++)
                        {
                            //Debug.Log($"The product {products[productIds[i]]} is with the price of" + products[productIds[i]].Price);
                            premiumText.text = $"The product is {products[productIds[i]].ToString()}";
                        }
                        //if (products.p("premium") != null)
                        //{
                        //    premiumText.text = "Get the " + offerings.OfferingForID("premium").Id + " Subscription";
                        //    /*DISCLAIMER: Remember that for the MakePurchase method you should always use the ProductID to search and purchase a product! Our case is using the offeringID only for demonstration purposes! 
                        //    This line would be the more correct one.
                        //    MakePurchase(offerings.offeringForID("premium").Products.First)*/
                        //    premiumButton.onClick.AddListener(() => { MakePurchase(offerings.AvailableOfferings.); });
                        //}
                        //if (offerings.OfferingForID("chocolatechipcookie") != null)
                        //{
                        //    chocolateCookieButton.onClick.AddListener(() => { MakePurchase(offerings.OfferingForID("chocolatechipcookie").Id); });
                        //}
                    }
                    else
                    {
                        premiumText.text = "There was an error: -> " + error;
                    }
                });

        }
        catch
        {
            premiumText.text = "Cannot get products";
        }

    }


    //public void MakePurchase(Product product)
    //{
    //    premiumText.text = "buying " + productID;

    //    Qonversion.GetSharedInstance().Purchase(productID, (entitlements, error) =>
    //    {
    //        if (error == null)
    //        {
    //            premiumText.text = "bought " + productID;
    //            CheckPermission(entitlements);
    //        }
    //        else
    //        {
    //            premiumText.text = "error -> " + error;
    //        }
    //    });
    //}

    public void CheckPermission(string entitlementId)
    {
        Qonversion.GetSharedInstance().CheckEntitlements((permissions, error) =>
        {
            if (error == null)
            {
                if (permissions.TryGetValue(entitlementId, out Entitlement plus) && plus.IsActive)
                {
                    if (entitlementId == "premium")
                    {
                        premiumText.text = "Got the Subscription!";
                      //  interstitialAdExample.adsShown = false;
                    }
                    //else if (entitlementId == "chocolatechipcookie")
                    //{
                    //    gameManager.swapAllowed = true;
                    //}
                }
                else
                {
                    if (entitlementId == "premium")
                    {
                    //    interstitialAdExample.adsShown = true;
                    }
                    //else if (entitlementId == "chocolatechipcookie")
                    //{
                    //    gameManager.swapAllowed = false;
                    //}
                }
            }
            else
            {
            }
        });
    }
}


