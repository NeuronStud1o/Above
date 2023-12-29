using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;
using UnityEngine.UI;
using System;

[Serializable]
public struct Icons
{
    public Sprite profileIcon;
    public string iconName;
}

public class AccountManagerMainMenu : MonoBehaviour
{
    private string equipedIcon;

    [SerializeField] private TextMeshProUGUI userName;
    [SerializeField] private Image profileImage;

    [SerializeField] private List<Icons> icons = new List<Icons>();

    void Start()
    {
        OnLoadMainMenu.instance.scriptsList.Add(StartActivity());
    }

    private async Task StartActivity()
    {
        userName.text = UserData.instance.User.DisplayName;

        if (await DataBase.instance.LoadDataCheck("userSettings", "icon") == false)
        {
            await DataBase.instance.SaveDataAsync("standart", "userSettings", "icon");
        }

        equipedIcon = await DataBase.instance.LoadDataString("userSettings", "icon");

        print(equipedIcon);

        foreach (Icons i in icons)
        {
            if (i.iconName == equipedIcon)
            {
                profileImage.sprite = i.profileIcon;

                return;
            }
        }
    }
}
