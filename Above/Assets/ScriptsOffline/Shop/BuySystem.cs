using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

[Serializable]
public class Skins
{
    [Serializable]
    public class OpenSkin
    {
        public string name;
        public int price;
        public bool isOpen;
        public bool isFlyCoin;
    }

    public List<OpenSkin> skinsList;
}

public class BuySystem : MonoBehaviour
{
    string filePath;
    public Skins skins;
    [SerializeField] private TextMeshProUGUI coinsF;
    [SerializeField] private TextMeshProUGUI coinsS;
    [SerializeField] private TextMeshProUGUI hightScore;
    [SerializeField] private GameObject shop;

    void Start()
    {
        filePath = Path.Combine(Application.persistentDataPath, "shop.json"); 
        skins = LoadFromJson();

        UpdateUI();
        shop.SetActive(true);
    }

    public void UpdateUI()
    {
        coinsF.text = PlayerPrefs.GetInt("coinsF") + "";
        coinsS.text = PlayerPrefs.GetInt("coinsS") + "";
        hightScore.text = PlayerPrefs.GetInt("hightScore") + "";
    }

    public void SaveToJson()
    {
        try
        {
            string jsonData = JsonUtility.ToJson(skins, true);
            File.WriteAllText(filePath, jsonData);
            Debug.Log("Data saved to JSON file: " + filePath);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error saving data to JSON file: " + e.Message);
        }
    }

    Skins LoadFromJson()
    {
        try
        {
            if (File.Exists(filePath))
            {
                string jsonData = File.ReadAllText(filePath);
                Skins data = JsonUtility.FromJson<Skins>(jsonData);
                Debug.Log("Data loaded from JSON file: " + filePath);
                return data;
            }
            else
            {
                var i = CreateJson();
                return i;
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error loading data from JSON file: " + e.Message);
            return null;
        }
    }

    Skins CreateJson()
    {
        skins = new Skins
        {
            skinsList = new List<Skins.OpenSkin>()
            {
                new Skins.OpenSkin { name = "blackThrush", isOpen = true, },
                new Skins.OpenSkin { name = "blackThrushFemale", isOpen = false, isFlyCoin = true, price = 50 },
                new Skins.OpenSkin { name = "pigeon", isOpen = false, isFlyCoin = true, price = 95 },
                new Skins.OpenSkin { name = "swallow", isOpen = false, isFlyCoin = true, price = 110 },
                new Skins.OpenSkin { name = "wheatearFemale", isOpen = false, isFlyCoin = true, price = 160 },
                new Skins.OpenSkin { name = "nightingaleFemale", isOpen = false, isFlyCoin = true, price = 215 },
                new Skins.OpenSkin { name = "whitePigeon", isOpen = false, isFlyCoin = true, price = 240 },
                new Skins.OpenSkin { name = "redCardinal", isOpen = false, isFlyCoin = false, price = 12 },
                new Skins.OpenSkin { name = "kiwi", isOpen = false, isFlyCoin = false, price = 30 },
            }
        };

        SaveToJson();

        return skins;
    }
}
