using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EXPmanager : MonoBehaviour
{
    public Slider expSlider;
    public Text countToNextLevel;
    public Text level;

    public static int countEXP;
    public int levelEXP;

    public Text hightScore;

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

        hightScore.text = PlayerPrefs.GetInt("recordScore") + "";
    }

    void Update()
    {
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

        PlayerPrefs.SetInt("EXP", countEXP);
        PlayerPrefs.SetInt("LevelEXP", levelEXP);

        if (countEXP >= 100)
        {
            countEXP = 0;
            levelEXP++;

            PlayerPrefs.SetInt("EXP", countEXP);
            PlayerPrefs.SetInt("LevelEXP", levelEXP);
        }
    }
}
