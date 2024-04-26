using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

    void Start()
    {
        if (purchaseType == Think.Skin)
        {
            bool isBought = JsonStorage.instance.data.purchasedItems.skins.Contains(purchaseName);

            if (isBought == true)
            {
                gameObject.SetActive(false);
            }
        }
        if (purchaseType == Think.Bg)
        {
            bool isBought = JsonStorage.instance.data.purchasedItems.bgs.Contains(purchaseName);

            if (isBought == true)
            {
                gameObject.SetActive(false);
            }
        }
        if (purchaseType == Think.Boost)
        {
            bool isBought = JsonStorage.instance.data.purchasedItems.boosts.Contains(purchaseName);

            if (isBought == true)
            {
                gameObject.SetActive(false);
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
                    JsonStorage.instance.data.purchasedItems.skins.Add(purchaseName);
                }
                else if (purchaseType == Think.Bg)
                {
                    JsonStorage.instance.data.purchasedItems.bgs.Add(purchaseName);
                }
                else if (purchaseType == Think.Boost)
                {
                    JsonStorage.instance.data.purchasedItems.boosts.Add(purchaseName);
                }

                if (GetComponent<AccIconsUnlockManager>())
                {
                    GetComponent<AccIconsUnlockManager>().Unlock();
                }

                gameObject.SetActive(false);

                CoinsManagerInMainMenu.instance.coinsF -= price;
                JsonStorage.instance.data.userData.coinsF = CoinsManagerInMainMenu.instance.coinsF;

                CryptoHelper.Encrypt(JsonStorage.instance.data, JsonStorage.instance.password);

                CoinsManagerInMainMenu.instance.UpdateUI();
            }
        }
        else if (coinType == Coin.SuperCoins)
        {
            if (CoinsManagerInMainMenu.instance.coinsS >= price)
            {
                if (purchaseType == Think.Skin)
                {
                    JsonStorage.instance.data.purchasedItems.skins.Add(purchaseName);
                }
                else if (purchaseType == Think.Bg)
                {
                    JsonStorage.instance.data.purchasedItems.bgs.Add(purchaseName);
                }
                else if (purchaseType == Think.Boost)
                {
                    JsonStorage.instance.data.purchasedItems.boosts.Add(purchaseName);
                }

                if (GetComponent<AccIconsUnlockManager>())
                {
                    GetComponent<AccIconsUnlockManager>().Unlock();
                }

                gameObject.SetActive(false);

                CoinsManagerInMainMenu.instance.coinsS -= price;
                JsonStorage.instance.data.userData.coinsS = CoinsManagerInMainMenu.instance.coinsS;

                CryptoHelper.Encrypt(JsonStorage.instance.data, JsonStorage.instance.password);
                
                CoinsManagerInMainMenu.instance.UpdateUI();
            }
        }
    }
}