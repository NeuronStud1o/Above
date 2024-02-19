using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class InterstitialAds : MonoBehaviour
{
    private InterstitialAd interstitial;
    private const string adUnitId = "ca-app-pub-9185697735170935/3389741167";

    public static InterstitialAds instance;

    void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        Load();
    }

    public void Show()
    {
        if (interstitial.IsLoaded())
        {
            interstitial.Show();
            Load();
        }
    }

    private void Load()
    {
        interstitial = new InterstitialAd(adUnitId);
        AdRequest request =  new AdRequest.Builder().Build();
        interstitial.LoadAd(request);
    }
}
