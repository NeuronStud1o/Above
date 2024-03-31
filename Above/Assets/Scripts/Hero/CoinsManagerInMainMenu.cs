using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CoinsManagerInMainMenu : MonoBehaviour
{
    public static CoinsManagerInMainMenu instance;

    public int coinsF;
    public int coinsS;

    [SerializeField] private TextMeshProUGUI SuperCoinsText;
    [SerializeField] private TextMeshProUGUI FlyCoinsText;
    [SerializeField] private TextMeshProUGUI SuperCoinsInShopText;
    [SerializeField] private TextMeshProUGUI FlyCoinsInShopText;
    [SerializeField] private TextMeshProUGUI AdsForSupercoinsCount;

    [SerializeField] private GameObject fAdErrorPanel;
    [SerializeField] private GameObject sAdErrorPanel;

    private void Start()
    {
        instance = this;

        coinsF = JsonStorage.instance.jsonData.userData.coinsF;
        coinsS = JsonStorage.instance.jsonData.userData.coinsS;

        UpdateUI();
    }

    public void UpdateUI()
    {
        coinsF = JsonStorage.instance.jsonData.userData.coinsF;
        coinsS = JsonStorage.instance.jsonData.userData.coinsS;
        
        SuperCoinsText.text = coinsS + "";
        FlyCoinsText.text = coinsF + "";
        SuperCoinsInShopText.text = coinsS + "";
        FlyCoinsInShopText.text = coinsF + "";
    }

    public void UpdateAdRewardUI(int ads)
    {
        AdsForSupercoinsCount.text = ads + " / 6"; 
    }

    public void ShowFErrorAd()
    {
        StartCoroutine(ErrorAd(fAdErrorPanel));
    }

    public void ShowSErrorAd()
    {
        StartCoroutine(ErrorAd(sAdErrorPanel));
    }

    IEnumerator ErrorAd(GameObject panel)
    {
        panel.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        panel.SetActive(false);
    }
}