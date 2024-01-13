using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
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
        userName.text = UserData.instance.User.DisplayName;

        equipedIcon = JsonStorage.instance.jsonData.userData.userIcon;

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
