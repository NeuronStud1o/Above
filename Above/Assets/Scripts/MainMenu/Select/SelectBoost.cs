using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
        if (JsonStorage.instance.jsonData.currentShop.currentBoost == 3)
        {
            foreach (GameObject hero in Heroes)
            {
                hero.GetComponent<SpriteRenderer>().color = shieldColor;
            }
        }
        else
        {
            foreach (GameObject hero in Heroes)
            {
                hero.GetComponent<SpriteRenderer>().color = standartColor;
            }
        }

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

        if (thisBoost == 3)
        {
            foreach (GameObject hero in Heroes)
            {
                hero.GetComponent<SpriteRenderer>().color = shieldColor;
            }
        }
        else
        {
            foreach (GameObject hero in Heroes)
            {
                hero.GetComponent<SpriteRenderer>().color = standartColor;
            }
        }

        JsonStorage.instance.jsonData.currentShop.currentBoost = thisBoost;

        string filePath = Path.Combine(Application.persistentDataPath, "gameData.json");
        CryptoHelper.Encrypt(filePath, JsonStorage.instance.jsonData, JsonStorage.instance.password);

        AllBoosts[thisBoost].SetActive(true);

        EquipedButtons[thisBoost].SetActive(true);
        EquipButtons[thisBoost].SetActive(false);
    }
}
