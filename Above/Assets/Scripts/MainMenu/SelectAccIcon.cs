using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SelectAccIcon : MonoBehaviour
{
    [SerializeField] private GameObject blockedPanel;
    public string iconName;
    public Image button;

    void Start()
    {
        iconName = GetComponent<Image>().sprite.name;

        KeyForm key = JsonStorage.instance.jsonData.accountIcons.icons.FirstOrDefault(item => item.name == iconName);
        
        if (key.isPurchased)
        {
            blockedPanel.SetActive(false);
        }

        if (iconName == JsonStorage.instance.jsonData.userData.userIcon)
        {
            Change();
        }
    }

    public void Change()
    {
        EquipAccIcon.instance.Change(iconName, button);
    }
}
