using System.Linq;
using UnityEngine;

public class AccIconsUnlockManager : MonoBehaviour
{
    [SerializeField] private string iconName;

    public void Unlock(bool isUlock)
    {
        KeyForm icon = JsonStorage.instance.jsonData.accountIcons.icons.FirstOrDefault(item => item.name == iconName);

        icon.isPurchased = isUlock;

        EquipAccIcon.instance.CheckLock();

        JsonStorage.instance.SaveData();
    }
}