using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public struct KeyForm
{
    public string name;
    public bool isPurchased;
}

public class JsonStorage : MonoBehaviour
{
    public static JsonStorage instance;

    [Header ("## Json file :")]
    public JsonData jsonData;

    [Header ("## Initial store settings :")]
    [SerializeField] private List<KeyForm> startSkinsList = new List<KeyForm>();
    [SerializeField] private List<KeyForm> startBgsList = new List<KeyForm>();
    [SerializeField] private List<KeyForm> startBoostsList = new List<KeyForm>();

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    async void Start()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "gameData.json");

        Debug.Log(filePath);

        if (!File.Exists(filePath))
        {
            if (await StorageData.instance.CheckIfJsonDataExists() == false)
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
                        userIcon = "blackThrush",

                        exp = 0,
                        level = 1,
                        coinsS = 0,
                        coinsF = 0
                    },

                    shop = new JsonData.Shop
                    {
                        skins = startSkinsList,
                        bgs = startBgsList,
                        boosts = startBoostsList
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

                Debug.Log("File is created in storage");

                SaveData();

                StorageData.instance.SaveJsonData();
            }
            else
            {
                await StorageData.instance.LoadJsonData();
                Debug.Log("File is received from storage");

                SaveData();
            }
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