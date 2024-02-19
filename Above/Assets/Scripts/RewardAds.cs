using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class RewardAds : MonoBehaviour
{
    private RewardedAd rewardedAdSupercoins;
    private RewardedAd rewardedAdFlycoins;
    private const string adUnitId = "ca-app-pub-9185697735170935/3240749118";
    private int supercoinsCount = 0;

    [SerializeField] private GameObject collectFlycoinsPanel;
    [SerializeField] private GameObject collectSuperCoinsPanel;

    private void Start()
    {
        Debug.Log(1);
        LoadFlycoins();
        LoadSupercoins();
    }

    public void HandleUserEarnedRewardFlycoins(object sender, GoogleMobileAds.Api.Reward args)
    {
        Debug.Log(4);
        JsonStorage.instance.jsonData.userData.coinsF += 2;
        JsonStorage.instance.jsonData.userData.coinsFAllTime += 2;

        collectFlycoinsPanel.SetActive(true);

        StorageData.instance.SaveJsonData();
    }

    public void HandleUserEarnedRewardSupercoins(object sender, GoogleMobileAds.Api.Reward args)
    {
        Debug.Log(5);
        supercoinsCount++;

        if (supercoinsCount >= 6)
        {
            JsonStorage.instance.jsonData.userData.coinsS += 1;
            JsonStorage.instance.jsonData.userData.coinsSAllTime += 1;

            collectSuperCoinsPanel.SetActive(true);

            StorageData.instance.SaveJsonData();
            supercoinsCount = 0;
        }
        
        CoinsManagerInMainMenu.instance.UpdateAdRewardUI(supercoinsCount);
    }

    public void UpdateCoinsData()
    {
        collectFlycoinsPanel.SetActive(false);
        collectSuperCoinsPanel.SetActive(false);

        CoinsManagerInMainMenu.instance.UpdateUI();
    }

    public void ShowFlycoins()
    {
        if (rewardedAdFlycoins.IsLoaded())
        {
            rewardedAdFlycoins.Show();
            LoadFlycoins();
        }
    }

    public void ShowSupercoins()
    {
        if (rewardedAdSupercoins.IsLoaded())
        {
            rewardedAdSupercoins.Show();
            LoadSupercoins();
        }
    }

    private void LoadFlycoins()
    {
        Debug.Log(2);
        rewardedAdFlycoins = new RewardedAd(adUnitId);
        AdRequest request = new AdRequest.Builder().Build();
        rewardedAdFlycoins.LoadAd(request);
        rewardedAdFlycoins.OnUserEarnedReward += HandleUserEarnedRewardFlycoins;
    }

    private void LoadSupercoins()
    {
        Debug.Log(3);
        rewardedAdSupercoins = new RewardedAd(adUnitId);
        AdRequest request = new AdRequest.Builder().Build();
        rewardedAdSupercoins.LoadAd(request);
        rewardedAdSupercoins.OnUserEarnedReward += HandleUserEarnedRewardSupercoins;
    }
}
