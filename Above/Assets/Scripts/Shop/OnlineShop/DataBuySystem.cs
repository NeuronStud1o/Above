using System.Collections;
using System.Collections.Generic;
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
            KeyForm key = JsonStorage.instance.jsonData.shop.skins.FirstOrDefault(item => item.name == purchaseName);

            if (key.isPurchased == true)
            {
                gameObject.SetActive(false);
            }
        }
        if (purchaseType == Think.Bg)
        {
            KeyForm key = JsonStorage.instance.jsonData.shop.bgs.FirstOrDefault(item => item.name == purchaseName);

            if (key.isPurchased == true)
            {
                gameObject.SetActive(false);
            }
        }
        if (purchaseType == Think.Boost)
        {
            KeyForm key = JsonStorage.instance.jsonData.shop.boosts.FirstOrDefault(item => item.name == purchaseName);

            if (key.isPurchased == true)
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
                    KeyForm purchaseItem = JsonStorage.instance.jsonData.shop.skins.FirstOrDefault(item => item.name == purchaseName);

                    if (purchaseItem.name != null)
                    {
                        purchaseItem.isPurchased = true;
                    }
                }
                else if (purchaseType == Think.Bg)
                {
                    KeyForm purchaseItem = JsonStorage.instance.jsonData.shop.bgs.FirstOrDefault(item => item.name == purchaseName);

                    if (purchaseItem.name != null)
                    {
                        purchaseItem.isPurchased = true;
                    }
                }
                else if (purchaseType == Think.Boost)
                {
                    KeyForm purchaseItem = JsonStorage.instance.jsonData.shop.boosts.FirstOrDefault(item => item.name == purchaseName);

                    if (purchaseItem.name != null)
                    {
                        purchaseItem.isPurchased = true;
                    }
                }

                if (GetComponent<AccIconsUnlockManager>())
                {
                    GetComponent<AccIconsUnlockManager>().Unlock(true);
                }

                gameObject.SetActive(false);

                CoinsManagerInMainMenu.instance.coinsF -= price;
                JsonStorage.instance.jsonData.userData.coinsF = CoinsManagerInMainMenu.instance.coinsF;

                CoinsManagerInMainMenu.instance.UpdateUI();
                
            }
        }
        else if (coinType == Coin.SuperCoins)
        {
            if (CoinsManagerInMainMenu.instance.coinsS >= price)
            {
                if (purchaseType == Think.Skin)
                {
                    KeyForm purchaseItem = JsonStorage.instance.jsonData.shop.skins.FirstOrDefault(item => item.name == purchaseName);

                    if (purchaseItem.name != null)
                    {
                        purchaseItem.isPurchased = true;
                    }
                }
                else if (purchaseType == Think.Bg)
                {
                    KeyForm purchaseItem = JsonStorage.instance.jsonData.shop.bgs.FirstOrDefault(item => item.name == purchaseName);

                    if (purchaseItem.name != null)
                    {
                        purchaseItem.isPurchased = true;
                    }
                }
                else if (purchaseType == Think.Boost)
                {
                    KeyForm purchaseItem = JsonStorage.instance.jsonData.shop.boosts.FirstOrDefault(item => item.name == purchaseName);

                    if (purchaseItem.name != null)
                    {
                        purchaseItem.isPurchased = true;
                    }
                }

                if (GetComponent<AccIconsUnlockManager>())
                {
                    GetComponent<AccIconsUnlockManager>().Unlock(true);
                }

                gameObject.SetActive(false);

                CoinsManagerInMainMenu.instance.coinsS -= price;
                JsonStorage.instance.jsonData.userData.coinsS = CoinsManagerInMainMenu.instance.coinsS;
                
                CoinsManagerInMainMenu.instance.UpdateUI();
                
            }
        }
    }
}