using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour
{

    const string moneyFileName = "MoneyFileName";
    const string coinsFIleName = "CoinsFIleName";
    const string adAvailableFileName = "AdAvailable";
    const string level1UnlockedFileName = "Level1Unlocked";
    const string subscribedFileName = "Subscribed";


    //ad available should be a bool but player prefs does not support bool variables so
    //we will make a convension
    //0=false
    //1=true
    public static void SetAdAvailable(bool state)
    {
        if (state == false)
        {
            PlayerPrefs.SetInt(adAvailableFileName, 0);
        }
        else
        {
            PlayerPrefs.SetInt(adAvailableFileName, 1);
        }
    }

    public static bool IsAdAvailable()
    {
        int available;
        if (PlayerPrefs.HasKey(adAvailableFileName))
        {
            available = PlayerPrefs.GetInt(adAvailableFileName);
        }
        else
        {
            available = 0;
        }

        //if the number from save is 0 it means that ads are available
        if (available == 0)
        {
            return false;
        }
        //else remove ads was bought
        return true;
    }

    //will use the same method for subscribed and level unlocked
    public static void SetSubscription(bool state)
    {
        if (state == false)
        {
            PlayerPrefs.SetInt(subscribedFileName, 0);
        }
        else
        {
            PlayerPrefs.SetInt(subscribedFileName, 1);
        }
    }

    public static bool IsSubscriptionBought()
    {
        int status=0;
        if(PlayerPrefs.HasKey(subscribedFileName))
        {
            status = PlayerPrefs.GetInt(subscribedFileName);
        }
        if(status == 0)
        {
            //subscription was not bought
            return false;
        }
        //else subscription was bought
        return true;
    }

    public static void UnlockLevel(bool state)
    {
        if(state == false)
        {
            PlayerPrefs.SetInt(level1UnlockedFileName, 0);
        }
        else
        {
            PlayerPrefs.SetInt(level1UnlockedFileName, 1);
        }
    }

    public static bool UnlockLevelWasBought()
    {
        int status = 0;
        if(PlayerPrefs.HasKey(level1UnlockedFileName))
        {
            status = PlayerPrefs.GetInt(level1UnlockedFileName);
        }
        if(status==0)
        {
            //level is not unlocked
            return false;
        }
        return true;
    }


    public static void SetCoins(int coins)
    {
        PlayerPrefs.SetInt(coinsFIleName, coins);
    }

    public static int GetCoins()
    {
        if (PlayerPrefs.HasKey(coinsFIleName))
        {
            return PlayerPrefs.GetInt(coinsFIleName);
        }
        return 0;
    }


    public static void SetMoney(int money)
    {
        PlayerPrefs.SetInt(moneyFileName, money);
    }

    public static int GetMoney()
    {
        if (PlayerPrefs.HasKey(moneyFileName))
        {
            return PlayerPrefs.GetInt(moneyFileName);
        }
        return 0;
    }
}
