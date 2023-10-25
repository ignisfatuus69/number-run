using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QonversionUnity;
public class AdEntitlementChecker : MonoBehaviour
{
    public InterstetialAds intAdsObject;
    public UnityBannerAd bannerAdsObject;
    void Awake()
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
                    if (entitlementsId == "Remove_Ads")
                    {
                        intAdsObject.adsShown = false;
                        bannerAdsObject.canShowAds = false;
                    }
                }
                else
                {
                    if (entitlementsId == "Remove_Ads")
                    {
                        intAdsObject.adsShown = true;
                        bannerAdsObject.canShowAds = true;
                    }
                }
            }
            else
            {
            }
        });
    }
}
