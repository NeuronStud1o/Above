using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Coin
{
    FlyCoins,
    SuperCoins
}

enum Think
{
    Skin,
    Bg,
    Boost
}

public class DataBuySystem : MonoBehaviour
{
    [SerializeField] private Coin coinType;
    [SerializeField] private Think purchaseType;
    [SerializeField] private int price;
    [SerializeField] private string purchaseName;

    async void Start()
    {
        if (purchaseType == Think.Skin)
        {
            if (await DataBase.instance.LoadDataCheck("shop", "toBuy", "skins", purchaseName) == true)
            {
                if (await DataBase.instance.LoadDataBool("shop", "toBuy", "skins", purchaseName) == true)
                {
                    gameObject.SetActive(false);
                }
            }
            else
            {
                DataBase.instance.SaveData(false, "shop", "toBuy", "skins", purchaseName);
            }
        }
        if (purchaseType == Think.Bg)
        {
            if (await DataBase.instance.LoadDataCheck("shop", "toBuy", "bgs", purchaseName) == true)
            {
                if (await DataBase.instance.LoadDataBool("shop", "toBuy", "bgs", purchaseName) == true)
                {
                    gameObject.SetActive(false);
                }
            }
            else
            {
                DataBase.instance.SaveData(false, "shop", "toBuy", "bgs", purchaseName);
            }
        }
        if (purchaseType == Think.Boost)
        {
            if (await DataBase.instance.LoadDataCheck("shop", "toBuy", "boosts", purchaseName) == true)
            {
                if (await DataBase.instance.LoadDataBool("shop", "toBuy", "boosts", purchaseName) == true)
                {
                    gameObject.SetActive(false);
                }
            }
            else
            {
                DataBase.instance.SaveData(false, "shop", "toBuy", "boosts", purchaseName);
            }
        }
    }

    public void Buy()
    {
        if (coinType == Coin.FlyCoins)
        {
            if (CoinsManagerInMainMenu.instance.coinsF >= price)
            {
                if (purchaseType == Think.Skin)
                {
                    DataBase.instance.SaveData(true, "shop", "toBuy", "skins", purchaseName);
                }
                else if (purchaseType == Think.Bg)
                {
                    DataBase.instance.SaveData(true, "shop", "toBuy", "bgs", purchaseName);
                }
                else if (purchaseType == Think.Boost)
                {
                    DataBase.instance.SaveData(true, "shop", "toBuy", "boosts", purchaseName);
                }

                gameObject.SetActive(false);

                CoinsManagerInMainMenu.instance.coinsF -= price;
                CoinsManagerInMainMenu.instance.UpdateUI();
                DataBase.instance.SaveData(CoinsManagerInMainMenu.instance.coinsF, "menu", "coins", "flyCoins");
            }
        }
        else if (coinType == Coin.SuperCoins)
        {
            if (CoinsManagerInMainMenu.instance.coinsS >= price)
            {
                if (purchaseType == Think.Skin)
                {
                    DataBase.instance.SaveData(true, "shop", "toBuy", "skins", purchaseName);
                }
                else if (purchaseType == Think.Bg)
                {
                    DataBase.instance.SaveData(true, "shop", "toBuy", "bgs", purchaseName);
                }
                else if (purchaseType == Think.Boost)
                {
                    DataBase.instance.SaveData(true, "shop", "toBuy", "boosts", purchaseName);
                }

                gameObject.SetActive(false);

                CoinsManagerInMainMenu.instance.coinsS -= price;
                CoinsManagerInMainMenu.instance.UpdateUI();
                DataBase.instance.SaveData(CoinsManagerInMainMenu.instance.coinsS, "menu", "coins", "superCoins");
            }
        }
    }
}