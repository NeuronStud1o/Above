using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[Serializable]
public struct LevelValue
{
    public int Level;
    public int CountToNextLevel;
    public string Title;
}

public class EXPManager : MonoBehaviour
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
        OnLoadMainMenu.instance.scriptsList.Add(StartActivity());
    }

    private async Task StartActivity()
    {
        if (await DataBase.instance.LoadDataCheck("game", "recordScore") == false)
        {
            await DataBase.instance.SaveDataAsync(0, "game", "recordScore");
        }

        hightScore.text = "" + await DataBase.instance.LoadDataInt("game", "recordScore");

        if (await DataBase.instance.LoadDataCheck("menu", "levelManager", "exp") == false)
        {
            DataBase.instance.SaveData(0, "menu", "levelManager", "exp");
            DataBase.instance.SaveData(1, "menu", "levelManager", "level");
        }

        await GetValues();

        SetValues();
        CheckNextLevel();
    }

    public async Task GetValues()
    {
        exp = await DataBase.instance.LoadDataInt("menu", "levelManager", "exp");
        level = await DataBase.instance.LoadDataInt("menu", "levelManager", "level");

        print (exp + " is exp");
        print (level + " is level");
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
        while (exp > countToNextLevel)
        {
            DataBase.instance.SaveData(exp - countToNextLevel, "menu", "levelManager", "exp");
            DataBase.instance.SaveData(level + 1, "menu", "levelManager", "level");

            SetValues();
        }

        SetValues();
    }
}