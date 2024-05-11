using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct LevelValue
{
    public int Level;
    public int CountToNextLevel;
    public string Title;
}

public class ManagerEXP : MonoBehaviour
{
    [SerializeField] private Slider expSlider;
    [SerializeField] private TextMeshProUGUI countToNextLevelText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI titleText;

    [SerializeField] private TextMeshProUGUI hightScore;
    [SerializeField] private List<LevelValue> keyFrame = new List<LevelValue>();

    private int countToNextLevel = 0;

    private int exp;
    private int level;
    
    void Start()
    {
        if (WIFIChecking.instance != null)
        {
            WIFIChecking.instance.ChangeSceneName();
        }
        
        if (JsonStorage.instance.data.userData.level >= 20)
        {
            bool isBought = JsonStorage.instance.data.icons.icons.Contains("exp");

            if (isBought == false)
            {
                JsonStorage.instance.data.icons.icons.Add("exp");
                EquipAccIcon.instance.CheckLock();
            }
        }

        hightScore.text = "" + JsonStorage.instance.data.userData.record;

        GetValues();

        SetValues();
        CheckNextLevel();

        if (level == 0)
        {
            JsonStorage.instance.isDataException = true;
            DataBase.instance.SetActiveLoadingScreen(true);
            DataBase.instance.SetMessage("Data loading error. Please re-enter the game");
        }
    }

    public void GetValues()
    {
        exp = JsonStorage.instance.data.userData.exp;
        level = JsonStorage.instance.data.userData.level;
    }

    private void SetValues()
    {
        foreach (LevelValue valueLevel in keyFrame)
        {
            if (valueLevel.Level > level)
            {
                countToNextLevelText.text = exp + " / " +  valueLevel.CountToNextLevel;
                levelText.text = level + "";

                expSlider.maxValue = valueLevel.CountToNextLevel;
                expSlider.value = exp;

                titleText.text = valueLevel.Title;

                countToNextLevel = (int)expSlider.maxValue;

                return;
            }
        }
    }

    public void CheckNextLevel()
    {
        while (exp >= countToNextLevel)
        {
            exp -= countToNextLevel;
            level++;
            JsonStorage.instance.data.userData.exp -= countToNextLevel;
            JsonStorage.instance.data.userData.level++;

            CryptoHelper.Encrypt(JsonStorage.instance.data, JsonStorage.instance.password);

            SetValues();
        }

        SetValues();
    }
}
