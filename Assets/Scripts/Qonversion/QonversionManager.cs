using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QonversionUnity;
using TMPro;
using UnityEngine.UI;

public class QonversionManager : MonoBehaviour
{
    [Header("Qonversion Configuration")]
    [SerializeField] private string projectKey = "F5fNlvs8EyR9pupP81C91d_Kr9j_1ck0";
    [SerializeField] private LaunchMode launchMode = LaunchMode.Analytics;

    [Header("UI Elements")]
    [SerializeField] private Text premiumText;
    //[SerializeField] private Button buyProductsButton;

    [Header("Offering Buttons")]
    [SerializeField] private List<OfferingButton> offeringButtons;

    private void Awake()
    {
        QonversionConfigBuilder qConfigBuilder = new QonversionConfigBuilder(projectKey, launchMode);
        QonversionConfig newQConfig = qConfigBuilder.Build();
        Qonversion.Initialize(newQConfig);

        Qonversion.GetSharedInstance().SyncPurchases();
        //buyProductsButton.onClick.AddListener(() => { PressedBuyProducts(); });
    }

    private void Start()
    {
        premiumText.text = "No Offerings Available";
        ShowOfferings();
    }

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
                    foreach (var offeringButton in offeringButtons)
                    {
                        if (offerings.OfferingForID(offeringButton.offeringID) != null)
                        {
                            offeringButton.buttonText.text = "Get the " + offerings.OfferingForID(offeringButton.offeringID).Id;
                            offeringButton.button.onClick.AddListener(() => { MakePurchase(offeringButton.offeringID); });
                        }
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
            premiumText.text = "Getting offerings broke";
        }
    }


    public void MakePurchase(string productID)
    {
        premiumText.text = "Buying " + productID;

        Qonversion.GetSharedInstance().Purchase(productID, (permissions, error, isCancelled) =>
        {
            if (error == null)
            {
                premiumText.text = "Bought " + productID;
                //CheckAdsPermission(permissions.ToString());
            }
            else
            {
                premiumText.text = "Error -> " + error;
            }
            if (isCancelled)
            {
                premiumText.text = "Cancelled purchased:  -> " + isCancelled;
            }
        });
    }
}