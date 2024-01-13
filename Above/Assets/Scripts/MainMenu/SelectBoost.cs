using System;
using System.Collections;
using System.Collections.Generic;
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
        currentBoost = JsonStorage.instance.jsonData.currentShop.currentBoost;

        EquipedButtons[currentBoost].SetActive(true);
        EquipButtons[currentBoost].SetActive(false);

        AllBoosts[currentBoost].SetActive(true);
    }

    public void Change(int thisBoost)
    {
        for (int i = 0; i < AllBoosts.Length; i++)
        {
            AllBoosts[i].SetActive(false);
            EquipedButtons[i].SetActive(false);
            EquipButtons[i].SetActive(true);
        }

        JsonStorage.instance.jsonData.currentShop.currentBoost = thisBoost;

        AllBoosts[thisBoost].SetActive(true);

        EquipedButtons[thisBoost].SetActive(true);
        EquipButtons[thisBoost].SetActive(false);
    }
}
