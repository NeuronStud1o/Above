using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public struct KeyForm
{
    public string name;
    public bool isPurchased;
}

public class JsonStorage : MonoBehaviour
{
    private int seconds = 0;
    public static JsonStorage instance;

    [Header ("## Json file :")]
    public JsonData jsonData;
    public PastData pastData;

    [Header ("## Initial store settings :")]
    [SerializeField] private List<KeyForm> startSkinsList = new List<KeyForm>();
    [SerializeField] private List<KeyForm> startBgsList = new List<KeyForm>();
    [SerializeField] private List<KeyForm> startBoostsList = new List<KeyForm>();

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void FixedUpdate()
    {
        if (seconds == 45)
        {
            if (pastData.FlyCoins != jsonData.userData.coinsF)
            {
                Debug.Log(pastData.FlyCoins + " - is past data flyCoins");
                Debug.Log(jsonData.userData.coinsF + " - is json flyCoins");

                pastData.FlyCoins = jsonData.userData.coinsF;

                DataBase.instance.SaveData(jsonData.userData.coinsFAllTime, "menu", "coins", "coinsFAllTime");
                Debug.Log("All time flyCoins is saved");
            }
        }
        if (seconds == 90)
        {
            if (pastData.SuperCoins != jsonData.userData.coinsS)
            {
                Debug.Log(pastData.FlyCoins + " - is past data superCoins");
                Debug.Log(jsonData.userData.coinsF + " - is json superCoins");

                pastData.SuperCoins = jsonData.userData.coinsS;

                DataBase.instance.SaveData(jsonData.userData.coinsSAllTime, "menu", "coins", "coinsSAllTime");
                Debug.Log("All time superCoins is saved");
            }
        }
        if (seconds == 135)
        {
            if (pastData.HightScore != jsonData.userData.record)
            {
                Debug.Log(pastData.FlyCoins + " - is past data record");
                Debug.Log(jsonData.userData.coinsF + " - is json record");

                pastData.HightScore = jsonData.userData.record;

                DataBase.instance.SaveData(jsonData.userData.record, "game", "recordScore");
                Debug.Log("Record is saved");
            }
        }
        if (seconds == 180)
        {
            if (pastData.Level != jsonData.userData.level)
            {
                Debug.Log(pastData.FlyCoins + " - is past data level");
                Debug.Log(jsonData.userData.coinsF + " - is json level");

                pastData.Level = jsonData.userData.level;

                DataBase.instance.SaveData(jsonData.userData.record, "menu", "levelManager", "level");
                Debug.Log("Level is saved");
            }

            seconds = 0;
        }
    }

    public async Task StartAction()
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
                        coinsF = 0,

                        coinsFAllTime = 0,
                        coinsSAllTime = 0
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

        pastData = new PastData
        {
            FlyCoins = jsonData.userData.coinsF,
            SuperCoins = jsonData.userData.coinsS,
                    
            Level = jsonData.userData.level,
            HightScore = jsonData.userData.record
        };

        StartTimer();
    }

    public void SaveData()
    {
        string json = JsonUtility.ToJson(jsonData, true);
        string filePath = Path.Combine(Application.persistentDataPath, "gameData.json");

        File.WriteAllText(filePath, json);

        Debug.Log("JSON file saved!");
    }

    private void AddSeconds()
    {
        seconds++;
    }

    private void StartTimer()
    {
        InvokeRepeating("AddSeconds", 1, 1);
    }
}