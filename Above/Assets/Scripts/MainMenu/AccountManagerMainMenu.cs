using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

[Serializable]
public struct Icons
{
    public Sprite profileIcon;
}

public class AccountManagerMainMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI userName;
    [SerializeField] private Image profileImage;

    [SerializeField] private List<Icons> icons = new List<Icons>();
    
    void Start()
    {
        userName.text = UserData.instance.User.DisplayName;

        string equipedIcon = JsonStorage.instance.data.userData.userIcon;

        foreach (Icons i in icons)
        {
            if (i.profileIcon.name == equipedIcon)
            {
                profileImage.sprite = i.profileIcon;

                return;
            }
        }
    }

    public void ChangeIcon(string iconName)
    {
        foreach (Icons i in icons)
        {
            if (i.profileIcon.name == iconName)
            {
                profileImage.sprite = i.profileIcon;

                return;
            }
        }
    }
}
