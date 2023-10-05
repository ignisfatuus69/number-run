using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QonversionUnity;
using TMPro;
using UnityEngine.UI;

public class QonversionManager : MonoBehaviour
{

    public Text premiumText;
    public Button premiumButton;
    public Button removeAdsButton;
    public InterstetialAds intAdsObject;
    public GameManager gameManager;

    void Awake()
    {
        QonversionConfigBuilder qConfigBuilder = new QonversionConfigBuilder("VSOFFsHF", LaunchMode.Analytics);
        QonversionConfig newQConfig = qConfigBuilder.Build();
        Qonversion.Initialize(newQConfig);
        Debug.Log(newQConfig.ProjectKey);
        Qonversion.GetSharedInstance().SyncPurchases();
        removeAdsButton.onClick.AddListener(() => { PressedBuyProducts(); });
    }

    // Start is called before the first frame update
    void Start()
    {
        premiumText.text = "No Offerings Available";
        ShowOfferings();
    }

    // Update is called once per frame
    public void ShowOfferings()
    {
        premiumText.text = "Getting Offerings";
        try
        {
            Qonversion.GetSharedInstance().Offerings((offerings, error) =>
            {
                premiumText.text = " Offerings";
                if (error == null)
                {
                    if (offerings.OfferingForID("Subscription") != null)
                    {
                        premiumText.text = "Get the " + offerings.OfferingForID("Subscription").Id + " Subscription";
                        /*DISCLAIMER: Remember that for the MakePurchase method you should always use the ProductID to search and purchase a product! Our case is using the offeringID only for demonstration purposes! 
                        This line would be the more correct one.
                        MakePurchase(offerings.offeringForID("premium").Products.First)*/
                        premiumButton.onClick.AddListener(() => { MakePurchase(offerings.OfferingForID("Subscription").Id); });
                    }
                    if (offerings.OfferingForID("Remove_Ads") != null)
                    {
                        removeAdsButton.onClick.AddListener(() => { MakePurchase(offerings.OfferingForID("Remove_Ads").Id); });
                    }
                }
                else
                {
                    premiumText.text = "error -> " + error;
                }
            });
        }
        catch
        {
            premiumText.text = "getting offerings broke";
        }

    }
    
    public void PressedBuyProducts()
    {
        Debug.Log("Buying");
    }

    public void MakePurchase(string productID)
    {
        premiumText.text = "buying" + productID;

        Qonversion.GetSharedInstance().Purchase(productID, (permissions, error,isCancelled) =>
        {
            if (error == null)
            {
                premiumText.text = "bought" + productID;
                CheckPermission(permissions.ToString());
            }
            else
            {
                premiumText.text = "error -> " + error;
            }
            if (isCancelled)
            {
                premiumText.text = "cancelled purchased:  -> " + isCancelled;
            }
        });;
    }

    public void CheckPermission(string entitlementsId)
    {
        Qonversion.GetSharedInstance().CheckEntitlements((permissions, error) =>
        {
            if (error == null)
            {
                if (permissions.TryGetValue(entitlementsId, out Entitlement plus) && plus.IsActive)
                {
                    if (entitlementsId == "Remove_Ads")
                    {
                        premiumText.text = "Got the removeads!";
                        intAdsObject.adsShown = false;
                    }
                    //else if (entitlementsId == "Remove_Ads")
                    //{
                    //    intAdsObject.adsShown = true;
                    //}
                }
                else
                {
                    //if (entitlementsId == "Subscription")
                    //{
                    //    intAdsObject.adsShown = false;
                    //}
                    if (entitlementsId == "Remove_Ads")
                    {
                        intAdsObject.adsShown = true;
                    }
                }
            }
            else
            {
            }
        });
    }
}