using UnityEngine;

public class AccIconsUnlockManager : MonoBehaviour
{
    [SerializeField] private string iconName;
    [SerializeField] private bool isNeedToBuy;

    void Start()
    {
        if (isNeedToBuy)
        {
            bool isBought = JsonStorage.instance.data.icons.icons.Contains(iconName);

            if (isBought)
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void Unlock()
    {
        JsonStorage.instance.data.icons.icons.Add(iconName);

        EquipAccIcon.instance.CheckLock();
    }

    public void BuyIconWithCoinsF(int price)
    {
        if (CoinsManagerInMainMenu.instance.coinsF >= price)
        {
            JsonStorage.instance.data.icons.icons.Add(iconName);

            gameObject.SetActive(false);

            EquipAccIcon.instance.CheckLock();
            
            CoinsManagerInMainMenu.instance.coinsF -= price;
            JsonStorage.instance.data.userData.coinsF = CoinsManagerInMainMenu.instance.coinsF;

            CryptoHelper.Encrypt(JsonStorage.instance.data, JsonStorage.instance.password);

            CoinsManagerInMainMenu.instance.UpdateUI();
        }
    }

    public void BuyIconWithCoinsS(int price)
    {
        if (CoinsManagerInMainMenu.instance.coinsS >= price)
        {
            JsonStorage.instance.data.icons.icons.Add(iconName);

            gameObject.SetActive(false);

            EquipAccIcon.instance.CheckLock();
            
            CoinsManagerInMainMenu.instance.coinsS -= price;
            JsonStorage.instance.data.userData.coinsS = CoinsManagerInMainMenu.instance.coinsS;

            CryptoHelper.Encrypt(JsonStorage.instance.data, JsonStorage.instance.password);

            CoinsManagerInMainMenu.instance.UpdateUI();
        }
    }
}