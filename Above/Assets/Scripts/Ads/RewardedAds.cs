using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardedAds : MonoBehaviour
{
    public void ShowAdForCoinsF()
    {
        AdsManager.instance.ShowRewardedFAd();
    }

    public void ShowAdForCoinsS()
    {
        AdsManager.instance.ShowRewardedSAd();
    }
}
