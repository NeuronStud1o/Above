using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections.Generic;

[Serializable]
public struct LevelValue
{
    public int Level;
    public int CountToNextLevel;
    public string Title;
}

public class EXPmanager : MonoBehaviour
{
    [SerializeField] private Slider expSlider;
    [SerializeField] private TextMeshProUGUI countToNextLevel;
    [SerializeField] private TextMeshProUGUI level;
    [SerializeField] private TextMeshProUGUI title;

    [SerializeField] private TextMeshProUGUI hightScore;
    [SerializeField] private List<LevelValue> keyFrame = new List<LevelValue>();

    private int CountToNextLevel = 0;


    void Start()
    {
        hightScore.text = "" + PlayerPrefs.GetInt("recordScore");

        if (!PlayerPrefs.HasKey("LevelEXP") && !PlayerPrefs.HasKey("EXP"))
        {
            PlayerPrefs.SetInt("EXP", 0);
            PlayerPrefs.SetInt("LevelEXP", 1);
        }

        SetValues();
        CheckNextLevel();

        if (PlayerPrefs.GetInt("LevelEXP") >= 120)
        {
            level.text = "MAX";
        }
    }

    private void SetValues()
    {
        foreach (LevelValue valueLevel in keyFrame)
        {
            if (valueLevel.Level > PlayerPrefs.GetInt("LevelEXP"))
            {
                countToNextLevel.text = PlayerPrefs.GetInt("EXP") + " / " +  valueLevel.CountToNextLevel;
                level.text = PlayerPrefs.GetInt("LevelEXP") + "";

                expSlider.maxValue = valueLevel.CountToNextLevel;
                expSlider.value = PlayerPrefs.GetInt("EXP");

                title.text = valueLevel.Title;

                CountToNextLevel = (int)expSlider.maxValue;

                return;
            }
        }
    }

    public void CheckNextLevel()
    {
        while (PlayerPrefs.GetInt("EXP") > CountToNextLevel)
        {
            PlayerPrefs.SetInt("EXP", PlayerPrefs.GetInt("EXP") - CountToNextLevel);
            PlayerPrefs.SetInt("LevelEXP", PlayerPrefs.GetInt("LevelEXP") + 1);

            SetValues();
        }

        SetValues();
    }

    public void AddEXP()
    {
        PlayerPrefs.SetInt("EXP", PlayerPrefs.GetInt("EXP") + 10);
        CheckNextLevel();
    }
}