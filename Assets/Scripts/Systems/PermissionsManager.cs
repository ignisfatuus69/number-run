using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QonversionUnity;
using TMPro;
using UnityEngine.UI;
public class PermissionsManager : MonoBehaviour
{
    [SerializeField] private UnityBannerAd bannerAds;

    void Awake()
    {
        QonversionConfigBuilder qConfigBuilder = new QonversionConfigBuilder("F5fNlvs8EyR9pupP81C91d_Kr9j_1ck0", LaunchMode.Analytics);
        QonversionConfig newQConfig = qConfigBuilder.Build();
        Qonversion.Initialize(newQConfig);
        Debug.Log(newQConfig.ProjectKey);
        Qonversion.GetSharedInstance().SyncPurchases();
    }

    private void Start()
    {
        CheckAdsPermission("Remove_Ads");
    }
    public void CheckAdsPermission(string entitlementsId)
    {
        Qonversion.GetSharedInstance().CheckEntitlements((permissions, error) =>
        {
            if (error == null)
            {
                if (permissions.TryGetValue(entitlementsId, out Entitlement plus) && plus.IsActive)
                {
                    Debug.Log("Checking remove_ads permission");
                    if (entitlementsId == "Remove_Ads")
                    {
                        Debug.Log("entitlement Remove_Ads detected, initializing ads to not be shown");
                        bannerAds.canShowAds = false;
                    }
                }
                else
                {
                    if (entitlementsId == "Remove_Ads")
                    {
                        bannerAds.canShowAds = true;
                    }
                }
            }
            else
            {
            }
        });
    }

}
