using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EXPmanager : MonoBehaviour
{
    [SerializeField] private Slider expSlider;
    [SerializeField] private Text countToNextLevel;
    [SerializeField] private Text level;

    public static int countEXP;
    public static int levelEXP;

    [SerializeField] private TextMeshProUGUI hightScore;

    void Start()
    {
        if (PlayerPrefs.HasKey("LevelEXP") && PlayerPrefs.HasKey("EXP"))
        {
            levelEXP = PlayerPrefs.GetInt("LevelEXP");
            countEXP = PlayerPrefs.GetInt("EXP");
        }
        else
        {
            PlayerPrefs.SetInt("EXP", 0);
            PlayerPrefs.SetInt("LevelEXP", 1);
        }

        hightScore.text = "" + PlayerPrefs.GetInt("recordScore");
        countToNextLevel.text = countEXP + " / 100";
        expSlider.value = countEXP;

        if (levelEXP >= 120)
        {
            level.text = "MAX";
        }
        else
        {
            level.text = levelEXP + "";
        }
    }

    void Update()
    {
        if (countEXP >= 100)
        {
            countEXP -= 100;
            levelEXP++;

            PlayerPrefs.SetInt("EXP", countEXP);
            PlayerPrefs.SetInt("LevelEXP", levelEXP);
        }
    }
}
