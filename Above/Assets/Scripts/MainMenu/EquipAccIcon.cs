using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EquipAccIcon : MonoBehaviour
{
    public static EquipAccIcon instance;

    [SerializeField] private List<SelectAccIcon> equipButtonsList;
    [SerializeField] private AccountManagerMainMenu accountManagerMainMenu;

    void Start()
    {
        instance = this;
    }

    public void CheckLock()
    {
        foreach (SelectAccIcon select in equipButtonsList)
        {
            select.CheckLock();
        }
    }

    public void Change(string name, Image button)
    {
        JsonStorage.instance.jsonData.userData.userIcon = name;

        foreach (SelectAccIcon select in equipButtonsList)
        {
            Color currentColor = select.button.color;
            currentColor.a = 0f;
            select.button.color = currentColor;
        }

        Color color = button.color;
        color.a = 0.5f;
        button.color = color;

        accountManagerMainMenu.ChangeIcon(name);

        string filePath = Path.Combine(Application.persistentDataPath, "gameData.json");
        CryptoHelper.Encrypt(filePath, JsonStorage.instance.jsonData, JsonStorage.instance.password);
    }
}
