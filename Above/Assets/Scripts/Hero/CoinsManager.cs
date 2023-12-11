using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsManager : MonoBehaviour
{
    public int coinsF;
    public int coinsS;

    [SerializeField] private Text moneyTextF;
    [SerializeField] private Text moneyTextS;
    [SerializeField] private StartOnClick startOnClick;
    [SerializeField] private CoinSpawner coinSpawner;

    public static CoinsManager instance;

    async void Start()
    {
        instance = this;

        startOnClick.player = GetComponent<Player>();
        
        coinSpawner.Hero = gameObject;

        if (await DataBase.instance.LoadDataCheck("shop", "equip", "boosts", "flyCoinsToAdd") == false)
        {
            DataBase.instance.SaveData(1, "shop", "equip", "boosts", "flyCoinsToAdd");
        }

        coinsF = await DataBase.instance.LoadDataInt("menu", "coins", "flyCoins");
        coinsS = await DataBase.instance.LoadDataInt("menu", "coins", "superCoins");

        UpdateUI();
    }

    public void UpdateUI()
    {
        moneyTextF.text = coinsF + "";
        moneyTextS.text = coinsS + "";
    }
}