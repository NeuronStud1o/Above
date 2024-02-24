using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class KeyForm
{
    public string name;
    public bool isPurchased;
}

public class JsonStorage : MonoBehaviour
{
    /*private string password;
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

    [Header ("## Initial account icons settings :")]
    [SerializeField] private List<KeyForm> startIcons = new List<KeyForm>();

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    private void OnApplicationQuit()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "gameData.json");

        Debug.Log(FirebaseAuthManager.instance.user);

        if (!File.Exists(filePath) || FirebaseAuthManager.instance.user == null) return;

        Debug.Log("User is not null");

        SaveEncryptDataOnExit(filePath);

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

        password = CryptoHelper.GenerateKeyFromUid(UserData.instance.User.UserId);

        if (!File.Exists(filePath))
        {
            if (await StorageData.instance.CheckIfJsonDataExists() == false)
            {
                jsonData = CreateNewJsonData();
                pastData = CreateNewJsonData();

                SaveEncryptDataOnExit(filePath);

                StartTimer();

                Debug.Log("File is created in storage");

                return;
            }
            else
            {
                await StorageData.instance.LoadJsonData();
                Debug.Log("File is received from storage");
            }
        }

        while (!File.Exists(filePath))
        {
            await Task.Delay(1000);
        }

        Debug.Log(File.Exists(filePath) + " is file exist");

        jsonData = CryptoHelper.LoadAndDecrypt<JsonData>(filePath, password);

        pastData = CryptoHelper.LoadAndDecrypt<JsonData>(filePath, password);

        StartTimer();
    }

    private void SaveEncryptDataOnExit(string filePath)
    {
        CryptoHelper.EncryptAndSave(filePath, jsonData, password);
    }

    private JsonData CreateNewJsonData()
    {
        return new JsonData
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
                musicMainMenu = 0.5f,
                musicGame = 0.5f,
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
            },

            accountIcons = new JsonData.AccountIcons
            {
                icons = startIcons
            }
        };
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

    public void CancelTimer()
    {
        CancelInvoke("AddSeconds");
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
    }*/
}