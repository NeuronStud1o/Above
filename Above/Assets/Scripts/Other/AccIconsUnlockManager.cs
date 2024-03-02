using System.IO;
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

            string filePath = Path.Combine(Application.persistentDataPath, "gameData.json");
            CryptoHelper.Encrypt(filePath, JsonStorage.instance.jsonData, JsonStorage.instance.password);

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

            string filePath = Path.Combine(Application.persistentDataPath, "gameData.json");
            CryptoHelper.Encrypt(filePath, JsonStorage.instance.jsonData, JsonStorage.instance.password);

            CoinsManagerInMainMenu.instance.UpdateUI();
        }
    }
}