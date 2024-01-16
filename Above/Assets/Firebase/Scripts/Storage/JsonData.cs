using System.Collections.Generic;

[System.Serializable]
public class JsonData
{
    public JsonData()
    {
        boolean = new Boolean();
        userData = new UserData();
        shop = new Shop();
        currentShop = new CurrentShop();
        audioSettings = new AudioSettings();
        otherSettings = new OtherSettings();
    }

    public Boolean boolean;
    public UserData userData;
    public Shop shop;
    public CurrentShop currentShop;
    public AudioSettings audioSettings;
    public OtherSettings otherSettings;

    [System.Serializable]
    public struct Boolean
    {
        public bool isTutorial;
        public bool isFirstTimeAudio;
    }

    [System.Serializable]
    public struct UserData
    {
        public string userName;
        public string userEmail;
        public string userIcon;

        public int exp;
        public int level;
        public int record;
        public int coinsF;
        public int coinsS;

        public int coinsFAllTime;
        public int coinsSAllTime;
    }

    [System.Serializable]
    public struct Shop
    {
        public List<KeyForm> skins;
        public List<KeyForm> bgs;
        public List<KeyForm> boosts;
    }

    [System.Serializable]
    public struct CurrentShop
    {
        public int currentBg;
        public int currentSkin;
        public int currentBoost;
    }

    [System.Serializable]
    public struct AudioSettings
    {
        public float musicMainMenu;
        public float sfxMainMenu;
        public float musicGame;
        public float sfxGame;
    }

    [System.Serializable]
    public struct OtherSettings
    {
        public bool showLevelRanks;
        public bool autoSave;
        public bool particles;
        public bool showSelectedBoostInGame;
        public bool cameraShake;
        public bool vibration;
    }
}