using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;
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

    void Start()
    {
        instance = this;

        OnLoadMainMenu.instance.scriptsList.Add(StartActivity());
    }

    private async Task StartActivity()
    {
        if (await DataBase.instance.LoadDataCheck("menu", "coins", "flyCoins") == false)
        {
            DataBase.instance.SaveData(0, "menu", "coins", "flyCoins");
            coinsF = 0;

            UpdateUI();

            return;
        }
        if (await DataBase.instance.LoadDataCheck("menu", "coins", "superCoins") == false)
        {
            DataBase.instance.SaveData(0, "menu", "coins", "superCoins");
            coinsS = 0;

            UpdateUI();

            return;
        }

        coinsF = await DataBase.instance.LoadDataInt("menu", "coins", "flyCoins");
        coinsS = await DataBase.instance.LoadDataInt("menu", "coins", "superCoins");

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