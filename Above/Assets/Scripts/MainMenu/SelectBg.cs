using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SelectBg : MonoBehaviour
{
    private int i;

    [SerializeField] private GameObject[] AllBg;
    [SerializeField] private GameObject[] AllRailings;

    [SerializeField] private GameObject[] EquipButtons;
    [SerializeField] private GameObject[] EquipedButtons;

    void Start()
    {
        OnLoadMainMenu.instance.scriptsList.Add(StartActivity());
    }

    private async Task StartActivity()
    {
        if (await DataBase.instance.LoadDataCheck("shop", "equip", "currentBg") == false)
        {
            await DataBase.instance.SaveDataAsync(0, "shop", "equip", "currentBg");
        }

        i = await DataBase.instance.LoadDataInt("shop", "equip", "currentBg");

        EquipedButtons[i].SetActive(true);
        EquipButtons[i].SetActive(false);

        AllBg[i].SetActive(true);
        AllRailings[i].SetActive(true);
    }

    public void Change(int thisBg)
    {
        for (int i = 0; i < AllBg.Length; i++)
        {
            AllBg[i].SetActive(false);
            AllRailings[i].SetActive(false);

            EquipedButtons[i].SetActive(false);
            EquipButtons[i].SetActive(true);
        }

        DataBase.instance.SaveData(thisBg, "shop", "equip", "currentBg");

        AllBg[thisBg].SetActive(true);
        AllRailings[thisBg].SetActive(true);

        EquipedButtons[thisBg].SetActive(true);
        EquipButtons[thisBg].SetActive(false);
    }
}
