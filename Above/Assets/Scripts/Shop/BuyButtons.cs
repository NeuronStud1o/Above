using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyButtons : MonoBehaviour
{
    [SerializeField] private Coin coin;
    [SerializeField] private int price;
    [SerializeField] private BuySystem buySystem;

    public void BuySkins(int indexItem)
    {
        if (coin == Coin.FlyCoins)
        {
            if (PlayerPrefs.GetInt("coinsF") >= price)
            {
                buySystem.elements[indexItem] = true;

                PlayerPrefs.SetInt("coinsF", PlayerPrefs.GetInt("coinsF") - price);
                buySystem.SaveGame();

                int items = PlayerPrefs.GetInt("BoughtItems");
                items++;
                PlayerPrefs.SetInt("BoughtItems", items);

                CoinsManagerInMainMenu.instance.UpdateUI();
            }
        }
        if (coin == Coin.SuperCoins)
        {
            if (PlayerPrefs.GetInt("coinsS") >= price)
            {
                buySystem.elements[indexItem] = true;

                PlayerPrefs.SetInt("coinsS", PlayerPrefs.GetInt("coinsS") - price);
                buySystem.SaveGame();

                int items = PlayerPrefs.GetInt("BoughtItems");
                items++;
                PlayerPrefs.SetInt("BoughtItems", items);

                CoinsManagerInMainMenu.instance.UpdateUI();
            }
        }
    }

    public void BuyBGs(int indexItem)
    {
        if (coin == Coin.FlyCoins)
        {
            if (PlayerPrefs.GetInt("coinsF") >= price)
            {
                buySystem.elements2[indexItem] = true;

                PlayerPrefs.SetInt("coinsF", PlayerPrefs.GetInt("coinsF") - price);
                buySystem.SaveGame();

                int items = PlayerPrefs.GetInt("BoughtItems");
                items++;
                PlayerPrefs.SetInt("BoughtItems", items);

                CoinsManagerInMainMenu.instance.UpdateUI();
            }
        }
        if (coin == Coin.SuperCoins)
        {
            if (PlayerPrefs.GetInt("coinsS") >= price)
            {
                buySystem.elements2[indexItem] = true;

                PlayerPrefs.SetInt("coinsS", PlayerPrefs.GetInt("coinsS") - price);
                buySystem.SaveGame();

                int items = PlayerPrefs.GetInt("BoughtItems");
                items++;
                PlayerPrefs.SetInt("BoughtItems", items);

                CoinsManagerInMainMenu.instance.UpdateUI();
            }
        }
    }

    public void BuyBoosts(int indexItem)
    {
        if (coin == Coin.FlyCoins)
        {
            if (PlayerPrefs.GetInt("coinsF") >= price)
            {
                buySystem.elements3[indexItem] = true;

                PlayerPrefs.SetInt("coinsF", PlayerPrefs.GetInt("coinsF") - price);
                buySystem.SaveGame();

                int items = PlayerPrefs.GetInt("BoughtItems");
                items++;
                PlayerPrefs.SetInt("BoughtItems", items);

                CoinsManagerInMainMenu.instance.UpdateUI();
            }
        }
        if (coin == Coin.SuperCoins)
        {
            if (PlayerPrefs.GetInt("coinsS") >= price)
            {
                buySystem.elements3[indexItem] = true;

                PlayerPrefs.SetInt("coinsS", PlayerPrefs.GetInt("coinsS") - price);
                buySystem.SaveGame();

                int items = PlayerPrefs.GetInt("BoughtItems");
                items++;
                PlayerPrefs.SetInt("BoughtItems", items);

                CoinsManagerInMainMenu.instance.UpdateUI();
            }
        }
    }
}
