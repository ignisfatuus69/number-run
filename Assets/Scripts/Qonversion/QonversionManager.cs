using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QonversionUnity;
using TMPro;
using UnityEngine.UI;

public class QonversionManager : MonoBehaviour
{

    public TextMeshProUGUI premiumText;
    public Button premiumButton;
    public Button chocolateCookieButton;
    public InterstetialAds interstitialAdExample;
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        premiumText.text = "No Offerings Available";
        //ShowOfferings();
    }



    //public void ShowOfferings()
    //{
    //    premiumText.text = "Getting Offerings";
    //    try{
    //        Qonversion.GetSharedInstance().Offerings((offerings, error) =>
    //        {
    //            premiumText.text = " Offerings";
    //            if (error == null)
    //            {
    //                if (offerings.OfferingForID("premium") != null)
    //                {
    //                    premiumText.text = "Get the " + offerings.OfferingForID("premium").Id + " Subscription";
    //                    /*DISCLAIMER: Remember that for the MakePurchase method you should always use the ProductID to search and purchase a product! Our case is using the offeringID only for demonstration purposes! 
    //                    This line would be the more correct one.
    //                    MakePurchase(offerings.offeringForID("premium").Products.First)*/
    //                    premiumButton.onClick.AddListener(() => { MakePurchase(offerings.off); });
    //                }
    //                if (offerings.OfferingForID("chocolatechipcookie") != null)
    //                {
    //                    chocolateCookieButton.onClick.AddListener(() => { MakePurchase(offerings.OfferingForID("chocolatechipcookie").Id); });
    //                }
    //            }
    //            else
    //            {
    //                premiumText.text = "error -> " + error;
    //            }
    //        });
    //    }
    //    catch
    //    {
    //        premiumText.text = "getting offerings broke";
    //    }
       
    //}


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
                        interstitialAdExample.adsShown = false;
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
                        interstitialAdExample.adsShown = true;
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


