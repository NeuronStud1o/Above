using System.Linq;
using UnityEngine;

public class AccIconsUnlockManager : MonoBehaviour
{
    [SerializeField] private string iconName;
    [SerializeField] private bool isNeedToBuy;

    void Start()
    {
        if (isNeedToBuy)
        {
            KeyForm data = JsonStorage.instance.jsonData.accountIcons.icons.FirstOrDefault(item => item.name == iconName);

            if (data.isPurchased)
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void Unlock(bool isUlock)
    {
        KeyForm icon = JsonStorage.instance.jsonData.accountIcons.icons.FirstOrDefault(item => item.name == iconName);

        icon.isPurchased = isUlock;

        EquipAccIcon.instance.CheckLock();
    }

    public void BuyIconWithCoinsF(int price)
    {
        if (CoinsManagerInMainMenu.instance.coinsF >= price)
        {
            KeyForm purchaseItem = JsonStorage.instance.jsonData.accountIcons.icons.FirstOrDefault(item => item.name == iconName);

            if (purchaseItem.name != null)
            {
                purchaseItem.isPurchased = true;
                gameObject.SetActive(false);
            }

            EquipAccIcon.instance.CheckLock();
            
            CoinsManagerInMainMenu.instance.coinsF -= price;
            JsonStorage.instance.jsonData.userData.coinsF = CoinsManagerInMainMenu.instance.coinsF;

            CoinsManagerInMainMenu.instance.UpdateUI();
        }
    }

    public void BuyIconWithCoinsS(int price)
    {
        if (CoinsManagerInMainMenu.instance.coinsS >= price)
        {
            KeyForm purchaseItem = JsonStorage.instance.jsonData.accountIcons.icons.FirstOrDefault(item => item.name == iconName);

            if (purchaseItem.name != null)
            {
                purchaseItem.isPurchased = true;
                gameObject.SetActive(false);
            }

            EquipAccIcon.instance.CheckLock();

            CoinsManagerInMainMenu.instance.coinsS -= price;
            JsonStorage.instance.jsonData.userData.coinsS = CoinsManagerInMainMenu.instance.coinsS;

            CoinsManagerInMainMenu.instance.UpdateUI();
        }
    }
}