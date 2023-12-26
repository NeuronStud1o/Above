using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SelectBoost : MonoBehaviour
{
    private int currentBoost;

    [SerializeField] private GameObject[] AllBoosts;
    [SerializeField] private GameObject[] Heroes;

    [SerializeField] private GameObject[] EquipButtons;
    [SerializeField] private GameObject[] EquipedButtons;

    [SerializeField] private Color shieldColor;
    [SerializeField] private Color standartColor;

    void Start()
    {
        OnLoadMainMenu.instance.scriptsList.Add(StartActivity());
    }

    private async Task StartActivity()
    {
        if (await DataBase.instance.LoadDataCheck("shop", "equip", "currentBoost") == false)
        {
            await DataBase.instance.SaveDataAsync(0, "shop", "equip", "currentBoost");
        }

        currentBoost = await DataBase.instance.LoadDataInt("shop", "equip", "currentBoost");

        EquipedButtons[currentBoost].SetActive(true);
        EquipButtons[currentBoost].SetActive(false);

        AllBoosts[currentBoost].SetActive(true);

        Check();
    }

    private void Check()
    {
        if (currentBoost == 1)
        {
            DataBase.instance.SaveData(2, "shop", "equip", "boosts", "flyCoinsToAdd");
            DataBase.instance.SaveData(0, "player", "hp");
            DataBase.instance.SaveData(2.2f, "player", "speed");
        }
        else
        {
            DataBase.instance.SaveData(1, "shop", "equip", "boosts", "flyCoinsToAdd");
        }

        if (currentBoost == 2)
        {
            DataBase.instance.SaveData(2, "shop", "equip", "boosts", "flyCoinsToAdd");
            DataBase.instance.SaveData(0, "player", "hp");
            DataBase.instance.SaveData(1.5f, "player", "speed");
        }
        else
        {
            DataBase.instance.SaveData(2.2f, "player", "speed");
        }

        if (currentBoost == 3)
        {
            DataBase.instance.SaveData(2, "shop", "equip", "boosts", "flyCoinsToAdd");
            DataBase.instance.SaveData(1, "player", "hp");
            DataBase.instance.SaveData(2.2f, "player", "speed");

            for (int i = 0; i < Heroes.Length; i++)
            {
                Heroes[i].GetComponent<SpriteRenderer>().color = shieldColor;
            }
        }
        else
        {
            DataBase.instance.SaveData(0, "player", "hp");
            
            for (int i = 0; i < Heroes.Length; i++)
            {
                Heroes[i].GetComponent<SpriteRenderer>().color = standartColor;
            }
        }
    }

    public void Change(int thisCharacter)
    {
        for (int i = 0; i < AllBoosts.Length; i++)
        {
            AllBoosts[i].SetActive(false);
            EquipedButtons[i].SetActive(false);
            EquipButtons[i].SetActive(true);
        }

        DataBase.instance.SaveData(thisCharacter, "shop", "equip", "currentBoost");

        AllBoosts[thisCharacter].SetActive(true);

        EquipedButtons[thisCharacter].SetActive(true);
        EquipButtons[thisCharacter].SetActive(false);

        Check();
    }
}
