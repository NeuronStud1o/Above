using System;
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
    public static JsonStorage instance;
    
    private int seconds = 0;
    private bool isCanCheck = true;

    [Header ("## Json file :")]
    public Data data;

    [Header ("## References :")]
    public string password;
    public bool isFrozenTimer = false;
    public bool isDataException = false;

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
        string filePath = Path.Combine(Application.persistentDataPath, "data.json");

        if (!File.Exists(filePath) || StorageData.instance.auth == null || isDataException || data.userData.level == 0)
        {
            Handheld.Vibrate();
            UnityEngine.Debug.Log("Vibration");
            return;
        }

        UnityEngine.Debug.Log("User is not null");

        SaveEncryptDataOnExit();

        UnityEngine.Debug.Log("Exit");
    }

    private void FixedUpdate()
    {
        if (isDataException)
        {
            GameManager.instance.SetActiveLoadingScreen(true);
            GameManager.instance.SetMessage("Data loading error.\nPlease re-enter the game");
            return;
        }

        if (seconds == 5 && isCanCheck && !isDataException && password != null && data.userData.level != 0)
        {
            CryptoHelper.Encrypt(data, password);
            StorageData.instance.SaveJsonData();

            seconds = 0;

            isCanCheck = false;
        }
    }

    public async Task StartAction()
    {
        Debug.Log("Start action");

        password = CryptoHelper.GenerateKeyFromUid(UserData.instance.User.UserId);
        GameManager.instance.SetMessage("Checking the availability of a file");

        await CheckJson();
    }

    public async Task CheckJson()
    {
        Debug.Log("Check json");
        string filePath = Path.Combine(Application.persistentDataPath, "data.json");

        if (!File.Exists(filePath))
        {
            try
            {
                if (UserData.instance.User != null)
                {
                    if (await StorageData.instance.CheckIfJsonDataExists() == false)
                    {
                        GameManager.instance.SetMessage("Creating new json data");

                        data = CreateNewJsonData();

                        SaveEncryptDataOnExit();
                        StartTimer();

                        Debug.Log("File is created in storage");

                        return;
                    }
                    else
                    {
                        Debug.Log("File is received from storage");
                        GameManager.instance.SetMessage("Loading json data");

                        data = await StorageData.instance.LoadJsonData<Data>();

                        while (!File.Exists(filePath))
                        {
                            await Task.Delay(100);
                        }

                        await Task.Delay(1000);

                        CryptoHelper.Encrypt(data, password);

                        return;
                    }
                }
                else
                {
                    GameManager.instance.SetMessage("User exception");
                }
                
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
                GameManager.instance.SetMessage(e.ToString());
            }
            
        }

        while (!File.Exists(filePath))
        {
            await Task.Delay(1000);
        }

        data = CryptoHelper.LoadAndDecrypt<Data>(filePath, password);

        GameManager.instance.SetMessage("Enabling add-ons");

        if (data.userData.level == 0)
        {
            isDataException = true;
            GameManager.instance.SetActiveLoadingScreen(true);
            GameManager.instance.SetMessage("Data loading error.\nPlease re-enter the game");
            return;
        }

        StartTimer();
    }

    private void SaveEncryptDataOnExit()
    {
        if (UserData.instance.User != null)
        {
            StorageData.instance.SetReference();

            string password = CryptoHelper.GenerateKeyFromUid(UserData.instance.User.UserId);
            
            StorageData.instance.SaveJsonData();
            CryptoHelper.Encrypt(data, password);
        }
        else
        {
            Debug.LogError("User is null, cannot save encrypted data.");
        }
    }

    private Data CreateNewJsonData()
    {
        return new Data
        {
            boolean = new Data.Boolean
            {
                isTutorial = false
            },

            userData = new Data.UserData
            {
                userIcon = "blackThrush",

                exp = 0,
                level = 1,
                coinsS = 0,
                coinsF = 0,

                coinsFAllTime = 0,
                coinsSAllTime = 0
            },

            purchasedItems = new Data.PurchasedItems
            {
                skins = new List<string>()
                {
                    "blackThrush"
                },

                bgs = new List<string>()
                {
                    "street"
                },

                boosts = new List<string>()
                {
                    "none"
                },
            },

            currentShop = new Data.CurrentShop
            {
                currentSkin = 0,
                currentBg = 0,
                currentBoost = 0
            },

            audioSettings = new Data.AudioSettings
            {
                musicMainMenu = 0.5f,
                musicGame = 0.5f,
                sfxMainMenu = 1,
                sfxGame = 1
            },

            otherSettings = new Data.OtherSettings
            {
                showLevelRanks = true,
                particles = true,
                showSelectedBoostInGame = true,
                cameraShake = true,
                vibration = true
            },

            icons = new Data.Icons
            {
                icons = new List<string>()
                {
                    "blackThrush",
                    "chimney"
                }
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