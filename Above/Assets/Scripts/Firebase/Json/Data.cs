using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Data
{
    public bool ContentEquals(Data other)
    {
        string thisJson = JsonUtility.ToJson(this);
        string otherJson = JsonUtility.ToJson(other);

        return thisJson == otherJson;
    }

    public void CopyFrom(Data other)
    {
        boolean = other.boolean;
        userData = other.userData;
        purchasedItems = other.purchasedItems;
        currentShop = other.currentShop;
        audioSettings = other.audioSettings;
        otherSettings = other.otherSettings;
        icons = other.icons;
    }
    
    public Data()
    {
        boolean = new Boolean();
        userData = new UserData();
        purchasedItems = new PurchasedItems();
        currentShop = new CurrentShop();
        audioSettings = new AudioSettings();
        otherSettings = new OtherSettings();
        icons = new Icons();
    }

    public Boolean boolean;
    public UserData userData;
    public PurchasedItems purchasedItems;
    public CurrentShop currentShop;
    public AudioSettings audioSettings;
    public OtherSettings otherSettings;
    public Icons icons;

    [System.Serializable]
    public struct Boolean
    {
        public bool isTutorial;
    }

    [System.Serializable]
    public struct UserData
    {
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
    public struct PurchasedItems
    {
        public List<string> skins;
        public List<string> bgs;
        public List<string> boosts;
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
        public bool particles;
        public bool showSelectedBoostInGame;
        public bool cameraShake;
        public bool vibration;
    }

    [System.Serializable]
    public struct Icons
    {
        public List<string> icons;
    }
}