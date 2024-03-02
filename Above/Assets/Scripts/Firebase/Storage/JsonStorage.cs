using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
    public string password;
    private int seconds = 0;
    private bool isCanCheck = true;
    public bool isFrozenTimer = false;
    public bool isDataException = false;
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

        if (!File.Exists(filePath) || FirebaseAuthManager.instance.user.UserId == null ||
            FirebaseAuthManager.instance.user == null || isDataException || jsonData.userData.level == 0)
        {
            Handheld.Vibrate();
            UnityEngine.Debug.Log("Vibration");
            return;
        }

        UnityEngine.Debug.Log("User is not null");

        SaveEncryptDataOnExit(filePath);

        UnityEngine.Debug.Log("Exit");

        if (pastData.userData.record != jsonData.userData.record)
        {
            UnityEngine.Debug.Log("Record is saved");
            DataBase.instance.SaveData(jsonData.userData.record, "game", "recordScore");
        }

        if (pastData.userData.coinsF != jsonData.userData.coinsF)
        {
            UnityEngine.Debug.Log("Fly coins is saved");
            DataBase.instance.SaveData(jsonData.userData.coinsFAllTime, "menu", "coins", "coinsFAllTime");
        }

        if (pastData.userData.coinsS != jsonData.userData.coinsS)
        {
            UnityEngine.Debug.Log("Super coins is saved");
            DataBase.instance.SaveData(jsonData.userData.coinsSAllTime, "menu", "coins", "coinsSAllTime");
        }

        if (pastData.userData.level != jsonData.userData.level)
        {
            UnityEngine.Debug.Log("Level is saved");
            DataBase.instance.SaveData(jsonData.userData.level, "menu", "levelManager", "level");
        }
    }

    private void FixedUpdate()
    {
        if (isDataException)
        {
            DataBase.instance.SetActiveLoadingScreen(true);
            DataBase.instance.SetMessage("Data loading error.\nPlease re-enter the game");
            return;
        }

        if (seconds % 5 == 0 && isCanCheck && !isDataException && password != null && jsonData.userData.level != 0)
        {
            string filePath = Path.Combine(Application.persistentDataPath, "gameData.json");

            CryptoHelper.Encrypt(filePath, jsonData, password);
            StorageData.instance.SaveJsonData();
            isCanCheck = false;
        }

        if (seconds == 10 && isCanCheck && !isDataException)
        {
            if (pastData.userData.record != jsonData.userData.record)
            {
                pastData.userData.record = jsonData.userData.record;

                DataBase.instance.SaveData(jsonData.userData.record, "game", "recordScore");
                UnityEngine.Debug.Log("Record is saved");
            }

            isCanCheck = false;
        }

        if (seconds == 20 && isCanCheck && !isDataException)
        {
            if (pastData.userData.coinsF != jsonData.userData.coinsF)
            {
                pastData.userData.coinsF = jsonData.userData.coinsF;
                pastData.userData.coinsFAllTime = jsonData.userData.coinsFAllTime;

                DataBase.instance.SaveData(jsonData.userData.coinsFAllTime, "menu", "coins", "coinsFAllTime");
                UnityEngine.Debug.Log("All time flyCoins is saved");
            }

            isCanCheck = false;
        }

        if (seconds == 30 && isCanCheck && !isDataException)
        {
            if (pastData.userData.coinsS != jsonData.userData.coinsS)
            {
                pastData.userData.coinsS = jsonData.userData.coinsS;
                pastData.userData.coinsSAllTime = jsonData.userData.coinsSAllTime;

                DataBase.instance.SaveData(jsonData.userData.coinsSAllTime, "menu", "coins", "coinsSAllTime");
                UnityEngine.Debug.Log("All time superCoins is saved");
            }

            isCanCheck = false;
        }

        if (seconds == 40 && isCanCheck && !isDataException)
        {
            if (pastData.userData.level != jsonData.userData.level)
            {
                pastData.userData.level = jsonData.userData.level;

                DataBase.instance.SaveData(jsonData.userData.level, "menu", "levelManager", "level");
                UnityEngine.Debug.Log("Level is saved");
            }

            isCanCheck = false;
        }

        if (seconds == 50 && isCanCheck && !isDataException)
        {
            if (!pastData.ContentEquals(jsonData))
            {
                pastData.CopyFrom(jsonData);

                string filePath = Path.Combine(Application.persistentDataPath, "gameData.json");

                CryptoHelper.Encrypt(filePath, jsonData, password);
                StorageData.instance.SaveJsonData();
                UnityEngine.Debug.Log("Json file is saved to www");
            }

            seconds = 0;
            isCanCheck = false;
        }
    }

    public async Task StartAction()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "gameData.json");

        password = CryptoHelper.GenerateKeyFromUid(UserData.instance.User.UserId);

        DataBase.instance.SetMessage("Checking the availability of a file");

        UnityEngine.Debug.Log(File.Exists(filePath) + " is file exist");

        if (!File.Exists(filePath))
        {
            try
            {
                if (UserData.instance.User != null)
                {
                    if (await StorageData.instance.CheckIfJsonDataExists() == false)
                    {
                        DataBase.instance.SetMessage("Creating new json data");

                        jsonData = CreateNewJsonData();
                        pastData = CreateNewJsonData();

                        SaveEncryptDataOnExit(filePath);

                        StartTimer();

                        UnityEngine.Debug.Log("File is created in storage");

                        return;
                    }
                    else
                    {
                        UnityEngine.Debug.Log("File is received from storage");
                        DataBase.instance.SetMessage("Loading json data");

                        jsonData = await StorageData.instance.LoadJsonData<JsonData>();

                        while (!File.Exists(filePath))
                        {
                            await Task.Delay(100);
                        }

                        await Task.Delay(1000);

                        CryptoHelper.Encrypt(filePath, jsonData, password);

                        return;
                    }
                }
                else
                {
                    DataBase.instance.SetMessage("User exception");
                }
                
            }
            catch (System.Exception e)
            {
                UnityEngine.Debug.LogError(e);
                DataBase.instance.SetMessage(e.ToString());
            }
            
        }

        while (!File.Exists(filePath))
        {
            await Task.Delay(1000);
        }

        jsonData = CryptoHelper.LoadAndDecrypt<JsonData>(filePath, password);
        pastData = CryptoHelper.LoadAndDecrypt<JsonData>(filePath, password);

        DataBase.instance.SetMessage("Enabling add-ons");

        if (jsonData.userData.level == 0)
        {
            isDataException = true;
            DataBase.instance.SetActiveLoadingScreen(true);
            DataBase.instance.SetMessage("Data loading error.\nPlease re-enter the game");
            return;
        }

        StartTimer();
    }

    private void SaveEncryptDataOnExit(string filePath)
    {
        if (UserData.instance.User != null)
        {
            StorageData.instance.SetReference();

            string password = CryptoHelper.GenerateKeyFromUid(UserData.instance.User.UserId);
            
            StorageData.instance.SaveJsonData();
            CryptoHelper.Encrypt(filePath, jsonData, password);
        }
        else
        {
            UnityEngine.Debug.LogError("User is null, cannot save encrypted data.");
        }
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

            UnityEngine.Debug.Log("The game is frozen");
        }
        else
        {
            isCanCheck = true;
            isFrozenTimer = false;
            StartTimer();

            UnityEngine.Debug.Log("The game is continiue");
        }
    }
}