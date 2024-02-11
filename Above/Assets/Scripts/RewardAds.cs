using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class RewardAds : MonoBehaviour
{
    private RewardedAd rewardedAd;
    private const string adUnitId = "ca-app-pub-3940256099942544/5224354917";

    [SerializeField] private GameObject collectRewardPanel;

    private void OnEnable()
    {
        Load();
    }

    public void HandleUserEarnedReward(object sender, GoogleMobileAds.Api.Reward args)
    {
        JsonStorage.instance.jsonData.userData.coinsF += 4;
        JsonStorage.instance.jsonData.userData.coinsFAllTime += 4;

        collectRewardPanel.SetActive(true);

        StorageData.instance.SaveJsonData();
    }

    public void UpdateCoinsData()
    {
        collectRewardPanel.SetActive(false);
        CoinsManagerInMainMenu.instance.UpdateUI();
    }

    public void Show()
    {
        if (rewardedAd.IsLoaded())
        {
            rewardedAd.Show();
            Load();
        }
    }

    private void Load()
    {
        rewardedAd = new RewardedAd(adUnitId);
        AdRequest request = new AdRequest.Builder().Build();
        rewardedAd.LoadAd(request);
        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
    }
}
