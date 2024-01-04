using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonStorage : MonoBehaviour
{
    public static JsonStorage instance;

    [Header ("## Json file :")]
    private JsonData jsonData;

    [Header ("## Initial store settings :")]
    [SerializeField] private List<bool> skinsList = new List<bool>();
    [SerializeField] private List<bool> bgsList = new List<bool>();
    [SerializeField] private List<bool> boostsList = new List<bool>();

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "gameData.json");

        Debug.Log(filePath);

        if (!File.Exists(filePath))
        {
            jsonData = new JsonData
            {
                boolean = new JsonData.Boolean
                {
                    isTutorial = false
                },

                userData = new JsonData.UserData
                {
                    userName = UserData.instance.User.DisplayName,
                    userEmail = UserData.instance.User.Email,
                    userIcon = 0
                },

                shop = new JsonData.Shop
                {
                    skins = skinsList,
                    bgs = bgsList,
                    boosts = boostsList
                },

                currentShop = new JsonData.CurrentShop
                {
                    currentSkin = 0,
                    currentBg = 0,
                    currentBoost = 0
                },

                audioSettings = new JsonData.AudioSettings
                {
                    musicMainMenu = 1,
                    musicGame = 1,
                    sfxMainMenu = 1,
                    sfxGame = 1
                },

                otherSettings = new JsonData.OtherSettings
                {
                    showLevelRanks = true,
                    autoSave = false,
                    particles = true,
                    showSelectedBoostInGame = true,
                    cameraShake = true,
                    vibration = true
                }
            };

            SaveData();
        }
        else
        {
            string json = File.ReadAllText(filePath);

            jsonData = JsonUtility.FromJson<JsonData>(json);
        }
    }

    public void SaveData()
    {
        string json = JsonUtility.ToJson(jsonData, true);
        string filePath = Path.Combine(Application.persistentDataPath, "gameData.json");

        File.WriteAllText(filePath, json);

        Debug.Log("JSON file saved!");
    }
}