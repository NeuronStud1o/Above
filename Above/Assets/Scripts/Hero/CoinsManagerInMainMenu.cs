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

    private void Start()
    {
        instance = this;

        coinsF = JsonStorage.instance.jsonData.userData.coinsF;
        coinsS = JsonStorage.instance.jsonData.userData.coinsS;

        UpdateUI();
    }

    public void UpdateUI()
    {
        SuperCoinsText.text = coinsS + "";
        FlyCoinsText.text = coinsF + "";
        SuperCoinsInShopText.text = coinsS + "";
        FlyCoinsInShopText.text = coinsF + "";
    }
}