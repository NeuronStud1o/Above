using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyButtons : MonoBehaviour
{
    public enum Coin
    {
        FlyCoins,
        SuperCoins
    }

    public Coin coin;

    public int price;

    public void BuySkins(int indexItem)
    {
        if (coin == Coin.FlyCoins)
        {
            if (PlayerPrefs.GetInt("coinsF") >= price)
            {
                BuySystem.elements[indexItem] = true;

                PlayerPrefs.SetInt("coinsF", PlayerPrefs.GetInt("coinsF") - price);
                BuySystem.SaveGame();

                int items = PlayerPrefs.GetInt("BoughtItems");
                items++;
                PlayerPrefs.SetInt("BoughtItems", items);
            }
        }
        if (coin == Coin.SuperCoins)
        {
            if (PlayerPrefs.GetInt("coinsS") >= price)
            {
                BuySystem.elements[indexItem] = true;

                PlayerPrefs.SetInt("coinsS", PlayerPrefs.GetInt("coinsS") - price);
                BuySystem.SaveGame();

                int items = PlayerPrefs.GetInt("BoughtItems");
                items++;
                PlayerPrefs.SetInt("BoughtItems", items);
            }
        }
    }

    public void BuyBGs(int indexItem)
    {
        if (coin == Coin.FlyCoins)
        {
            if (PlayerPrefs.GetInt("coinsF") >= price)
            {
                BuySystem.elements2[indexItem] = true;

                PlayerPrefs.SetInt("coinsF", PlayerPrefs.GetInt("coinsF") - price);
                BuySystem.SaveGame();

                int items = PlayerPrefs.GetInt("BoughtItems");
                items++;
                PlayerPrefs.SetInt("BoughtItems", items);
            }
        }
        if (coin == Coin.SuperCoins)
        {
            if (PlayerPrefs.GetInt("coinsS") >= price)
            {
                BuySystem.elements2[indexItem] = true;

                PlayerPrefs.SetInt("coinsS", PlayerPrefs.GetInt("coinsS") - price);
                BuySystem.SaveGame();

                int items = PlayerPrefs.GetInt("BoughtItems");
                items++;
                PlayerPrefs.SetInt("BoughtItems", items);
            }
        }
    }

    public void BuyBoosts(int indexItem)
    {
        if (coin == Coin.FlyCoins)
        {
            if (PlayerPrefs.GetInt("coinsF") >= price)
            {
                BuySystem.elements3[indexItem] = true;

                PlayerPrefs.SetInt("coinsF", PlayerPrefs.GetInt("coinsF") - price);
                BuySystem.SaveGame();

                int items = PlayerPrefs.GetInt("BoughtItems");
                items++;
                PlayerPrefs.SetInt("BoughtItems", items);
            }
        }
        if (coin == Coin.SuperCoins)
        {
            if (PlayerPrefs.GetInt("coinsS") >= price)
            {
                BuySystem.elements3[indexItem] = true;

                PlayerPrefs.SetInt("coinsS", PlayerPrefs.GetInt("coinsS") - price);
                BuySystem.SaveGame();

                int items = PlayerPrefs.GetInt("BoughtItems");
                items++;
                PlayerPrefs.SetInt("BoughtItems", items);
            }
        }
    }
}
