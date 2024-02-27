using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using GoogleMobileAds.Api;
using UnityEngine;

public class AdsManager : MonoBehaviour
{
    public static AdsManager instance;
    string interstitialId = "ca-app-pub-9185697735170935/3389741167";
    string rewardedId = "ca-app-pub-9185697735170935/3240749118";

    int adsCoinsSCount = 0;

    InterstitialAd interstitialAd;
    RewardedAd rewardedAd;

    [SerializeField] private GameObject collectFlycoinsPanel;
    [SerializeField] private GameObject collectSuperCoinsPanel;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        MobileAds.RaiseAdEventsOnUnityMainThread = true;

        MobileAds.Initialize(initStatus => {

            print("Ads Initialised");

        });

        LoadInterstitialAd();
        LoadRewardedAd();
    }

    public void LoadInterstitialAd()
    {
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
            interstitialAd = null;
        }

        var adRequest = new AdRequest();
        adRequest.Keywords.Add("unity-admob-sample");

        InterstitialAd.Load(interstitialId, adRequest, (InterstitialAd ad, LoadAdError error) =>
        {
            if (error != null || ad == null)
            {
                return;
            }

            interstitialAd = ad;
            InterstitialEvent(interstitialAd);
        });

    }

    public void ShowInterstitialAd()
    {
        if (interstitialAd != null && interstitialAd.CanShowAd())
        {
            interstitialAd.Show();
            LoadInterstitialAd();
        }
        else
        {
            print("Intersititial ad not ready!!");
        }
    }

    public void InterstitialEvent(InterstitialAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            print("Interstitial ad paid {0} {1}." +
                adValue.Value +
                adValue.CurrencyCode);
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
            print("Interstitial ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
            print("Interstitial ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
            print("Interstitial ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            print("Interstitial ad full screen content closed.");
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            print("Interstitial ad failed to open full screen content " +
                           "with error : " + error);
        };
    }

    public void LoadRewardedAd()
    {
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }
        var adRequest = new AdRequest();
        adRequest.Keywords.Add("unity-admob-sample");

        RewardedAd.Load(rewardedId, adRequest, (RewardedAd ad, LoadAdError error) =>
        {
            if (error != null || ad == null)
            {
                return;
            }

            rewardedAd = ad;
            RewardedAdEvents(rewardedAd);
        });
    }

    public void ShowRewardedFAd()
    {
        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardedAd.Show((GoogleMobileAds.Api.Reward reward) =>
            {
                GrantCoinsF();
            });
        }
        else
        {
            print("Rewarded ad not ready");
        }

        LoadRewardedAd();
    }

    public void ShowRewardedSAd()
    {
        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardedAd.Show((GoogleMobileAds.Api.Reward reward) =>
            {
                GrantCoinsS();

            });
        }
        else
        {
            print("Rewarded ad not ready");
        }

        LoadRewardedAd();
    }

    public void RewardedAdEvents(RewardedAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            print("Rewarded ad paid {0} {1}." +
                adValue.Value +
                adValue.CurrencyCode);
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
            print("Rewarded ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
            print("Rewarded ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
            print("Rewarded ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            print("Rewarded ad full screen content closed.");
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            print("Rewarded ad failed to open full screen content " +
                           "with error : " + error);
        };
    }

    void GrantCoinsF()
    {
        JsonStorage.instance.jsonData.userData.coinsF += 2;
        JsonStorage.instance.jsonData.userData.coinsFAllTime += 2;

        StorageData.instance.SaveJsonData();
        collectFlycoinsPanel.SetActive(true);
    }

    void GrantCoinsS()
    {
        adsCoinsSCount++;

        if (adsCoinsSCount == 6)
        {
            JsonStorage.instance.jsonData.userData.coinsS += 1;
            JsonStorage.instance.jsonData.userData.coinsSAllTime += 1;

            StorageData.instance.SaveJsonData();
            collectSuperCoinsPanel.SetActive(true);

            adsCoinsSCount = 0;
        }
        
        CoinsManagerInMainMenu.instance.UpdateAdRewardUI(adsCoinsSCount);
    }

    public void UpdateUI()
    {
        CoinsManagerInMainMenu.instance.UpdateUI();
    }
}
