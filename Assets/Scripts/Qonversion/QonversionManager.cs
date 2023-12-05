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
    public GameManager gameManager;
    private IQonversion qonversionInstance; // Instance of IQonversion
    public User userInfo;
    public Text qonversionId;
    public Text identityId;

    void Awake()
    {
        QonversionConfigBuilder qConfigBuilder = new QonversionConfigBuilder("F5fNlvs8EyR9pupP81C91d_Kr9j_1ck0", LaunchMode.Analytics);
        QonversionConfig newQConfig = qConfigBuilder.Build();
        Qonversion.Initialize(newQConfig);
        Debug.Log(newQConfig.ProjectKey);
        qonversionInstance = Qonversion.GetSharedInstance(); // Initializing the Qonversion instance
        qonversionInstance.SyncPurchases();
        removeAdsButton.onClick.AddListener(() => { PressedBuyProducts(); });
        qonversionInstance.UserInfo((user, error) =>
        {
            if (error == null)
            {
                userInfo = user;
                Debug.Log("User Info: " + user.QonversionId); // Example usage of retrieved userInfo
                qonversionId.text = "QonId:" + user.QonversionId;
                identityId.text = "IdentId:" + user.IdentityId;
            }
            else
            {
                // Handle error if needed
            }
        });
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
            qonversionInstance.Offerings((offerings, error) =>
            {
                premiumText.text = " Offerings";
                if (error == null)
                {
                    if (offerings.OfferingForID("Subscription") != null)
                    {
                        premiumText.text = "Get the " + offerings.OfferingForID("Subscription").Id + " Subscription";
                        premiumButton.onClick.AddListener(() => { MakePurchase(offerings.OfferingForID("Subscription").Id); });
                    }
                    if (offerings.OfferingForID("Remove_Ads") != null)
                    {
                        removeAdsButton.onClick.AddListener(() => { MakePurchase(offerings.OfferingForID("Remove_Ads").Id); });
                        // initiate check ads permission here
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

        qonversionInstance.Purchase(productID, (permissions, error, isCancelled) =>
        {
            if (error == null)
            {
                premiumText.text = "bought" + productID;
                //CheckAdsPermission(permissions.ToString());
            }
            else
            {
                premiumText.text = "error -> " + error;
            }
            if (isCancelled)
            {
                premiumText.text = "cancelled purchased:  -> " + isCancelled;
            }
        }); ;
    }
}
