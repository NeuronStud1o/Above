using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SelectCharacter : MonoBehaviour
{
    private int i;

    [SerializeField] private GameObject[] AllCharacters;

    [SerializeField] private GameObject[] EquipButtons;
    [SerializeField] private GameObject[] EquipedButtons;

    void Start()
    {
        OnLoadMainMenu.instance.scriptsList.Add(StartActivity());
    }

    private async Task StartActivity()
    {
        if (await DataBase.instance.LoadDataCheck("shop", "equip", "currentCharacter") == false)
        {
            await DataBase.instance.SaveDataAsync(0, "shop", "equip", "currentCharacter");
        }

        i = await DataBase.instance.LoadDataInt("shop", "equip", "currentCharacter");

        EquipedButtons[i].SetActive(true);
        EquipButtons[i].SetActive(false);

        AllCharacters[i].SetActive(true);
    }

    public void Change(int thisCharacter)
    {
        for (int i = 0; i < AllCharacters.Length; i++)
        {
            AllCharacters[i].SetActive(false);
            EquipedButtons[i].SetActive(false);
            EquipButtons[i].SetActive(true);
        }

        DataBase.instance.SaveData(thisCharacter, "shop", "equip", "currentCharacter");

        AllCharacters[thisCharacter].SetActive(true);

        EquipedButtons[thisCharacter].SetActive(true);
        EquipButtons[thisCharacter].SetActive(false);
    }
}
