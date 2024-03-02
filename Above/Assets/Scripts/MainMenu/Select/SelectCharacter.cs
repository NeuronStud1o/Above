using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SelectCharacter : MonoBehaviour
{
    private int i;

    [SerializeField] private GameObject[] AllCharacters;

    [SerializeField] private GameObject[] EquipButtons;
    [SerializeField] private GameObject[] EquipedButtons;

    void Start()
    {
        i = JsonStorage.instance.jsonData.currentShop.currentSkin;

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

        JsonStorage.instance.jsonData.currentShop.currentSkin = thisCharacter;

        string filePath = Path.Combine(Application.persistentDataPath, "gameData.json");
        CryptoHelper.Encrypt(filePath, JsonStorage.instance.jsonData, JsonStorage.instance.password);

        AllCharacters[thisCharacter].SetActive(true);

        EquipedButtons[thisCharacter].SetActive(true);
        EquipButtons[thisCharacter].SetActive(false);
    }
}
