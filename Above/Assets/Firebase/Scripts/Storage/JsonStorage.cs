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
    private bool isCanCheck = true;
    public bool isFrozenTimer = false;
    public static JsonStorage instance;

    [Header ("## Json file :")]
    public JsonData jsonData;
    public JsonData pastData;

    [Header ("## Initial store settings :")]
    [SerializeField] private List<KeyForm> startSkinsList = new List<KeyForm>();
    [SerializeField] private List<KeyForm> startBgsList = new List<KeyForm>();
    [SerializeField] private List<KeyForm> startBoostsList = new List<KeyForm>();

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnApplicationQuit()
    {
        Debug.Log("Exit");

        if (pastData.userData.record != jsonData.userData.record)
        {
            Debug.Log("Record is saved");
            DataBase.instance.SaveData(jsonData.userData.record, "game", "recordScore");
        }

        if (pastData.userData.coinsF != jsonData.userData.coinsF)
        {
            Debug.Log("Fly coins is saved");
            DataBase.instance.SaveData(jsonData.userData.coinsFAllTime, "menu", "coins", "coinsFAllTime");
        }

        if (pastData.userData.coinsS != jsonData.userData.coinsS)
        {
            Debug.Log("Super coins is saved");
            DataBase.instance.SaveData(jsonData.userData.coinsSAllTime, "menu", "coins", "coinsSAllTime");
        }

        if (pastData.userData.level != jsonData.userData.level)
        {
            Debug.Log("Level is saved");
            DataBase.instance.SaveData(jsonData.userData.level, "menu", "levelManager", "level");
        }

        if (!pastData.ContentEquals(jsonData))
        {
            StorageData.instance.SaveJsonData();
        }
    }

    private void FixedUpdate()
    {
        if (seconds == 20 && isCanCheck)
        {
            Debug.Log(20 + " seconds");

            if (pastData.userData.record != jsonData.userData.record)
            {
                pastData.userData.record = jsonData.userData.record;

                DataBase.instance.SaveData(jsonData.userData.record, "game", "recordScore");
                Debug.Log("Record is saved");
            }

            isCanCheck = false;
        }

        if (seconds == 40 && isCanCheck)
        {
            Debug.Log(40 + " seconds");

            if (pastData.userData.coinsF != jsonData.userData.coinsF)
            {
                pastData.userData.coinsF = jsonData.userData.coinsF;
                pastData.userData.coinsFAllTime = jsonData.userData.coinsFAllTime;

                DataBase.instance.SaveData(jsonData.userData.coinsFAllTime, "menu", "coins", "coinsFAllTime");
                Debug.Log("All time flyCoins is saved");
            }

            isCanCheck = false;
        }

        if (seconds == 60 && isCanCheck)
        {
            Debug.Log(60 + " seconds");

            if (pastData.userData.coinsS != jsonData.userData.coinsS)
            {
                pastData.userData.coinsS = jsonData.userData.coinsS;
                pastData.userData.coinsSAllTime = jsonData.userData.coinsSAllTime;

                DataBase.instance.SaveData(jsonData.userData.coinsSAllTime, "menu", "coins", "coinsSAllTime");
                Debug.Log("All time superCoins is saved");
            }

            isCanCheck = false;
        }

        if (seconds == 80 && isCanCheck)
        {
            Debug.Log(80 + " seconds");

            if (pastData.userData.level != jsonData.userData.level)
            {
                pastData.userData.level = jsonData.userData.level;

                DataBase.instance.SaveData(jsonData.userData.level, "menu", "levelManager", "level");
                Debug.Log("Level is saved");
            }

            isCanCheck = false;
        }

        if (seconds == 100 && isCanCheck)
        {
            Debug.Log(100 + " seconds");

            if (!pastData.ContentEquals(jsonData))
            {
                StorageData.instance.SaveJsonData();

                pastData.CopyFrom(jsonData);
                Debug.Log("Json file is saved to www");
            }

            seconds = 0;
            isCanCheck = false;
        }
    }

    public async Task StartAction()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "gameData.json");

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
                        boosts = startBoostsList,
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

        string past = File.ReadAllText(filePath);

        pastData = JsonUtility.FromJson<JsonData>(past);

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
        isCanCheck = true;
        seconds++;
    }

    private void StartTimer()
    {
        InvokeRepeating("AddSeconds", 1, 1);
    }

    public void ActivateTimer(bool isActive)
    {
        if (!isActive)
        {
            CancelInvoke("AddSeconds");
            isFrozenTimer = true;

            Debug.Log("The game is frozen");
        }
        else
        {
            isCanCheck = true;
            isFrozenTimer = false;
            StartTimer();

            Debug.Log("The game is continiue");
        }
    }
}