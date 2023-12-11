using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinsManagerInMainMenu : MonoBehaviour
{
    public static int coinsF;

    public static CoinsManagerInMainMenu instance;

    public static int coinsS;

    [SerializeField] private TextMeshProUGUI SuperCoinsText;
    [SerializeField] private TextMeshProUGUI FlyCoinsText;
    [SerializeField] private TextMeshProUGUI SuperCoinsInShopText;
    [SerializeField] private TextMeshProUGUI FlyCoinsInShopText;

    async void Start()
    {
        instance = this;

        if (await DataBase.instance.LoadDataInt("menu", "coins", "superCoins") >= 10)
        {
            DataBase.instance.SaveData(1, "tasks", "taskSuperCoins");
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