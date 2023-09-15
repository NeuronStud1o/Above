using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LosePanel : MonoBehaviour
{
    public Text scoreText;
    public Camera thisCamera;
    public GameObject panel;

    private void Start()
    {
        Handheld.Vibrate();
        int random;

        random = Random.Range(1, 4);

        if (random == 1)
        {
            thisCamera.rect = new Rect(0f, 0f, 1f, 1f);
            panel.SetActive(true);
            UnityInterstitialAd.Instace.ShowAd();
        }
        
        int gainedExp;

        int lastRunScore = PlayerPrefs.GetInt("lastRunScore");
        int recordScore = PlayerPrefs.GetInt("recordScore");

        int tasksRecordScore = PlayerPrefs.GetInt("tasksRecordScore");

        gainedExp = lastRunScore / 14;

        if (gainedExp > 100)
        {
            gainedExp = 100;
        }

        int doneExp = PlayerPrefs.GetInt("EXP") + gainedExp;
        PlayerPrefs.SetInt("EXP", doneExp);

        if (EXPmanager.countEXP >= 100)
        {
            EXPmanager.countEXP -= 100;
            EXPmanager.levelEXP++;

            PlayerPrefs.SetInt("EXP", EXPmanager.countEXP);
            PlayerPrefs.SetInt("LevelEXP", EXPmanager.levelEXP);
        }

        if (ProgressEveryDayTasks.points != 0)
        {
            if (lastRunScore > tasksRecordScore)
            {
                PlayerPrefs.SetInt("tasksRecordScore", lastRunScore);
            }
        }

        if (lastRunScore > recordScore)
        {
            recordScore = lastRunScore;
            PlayerPrefs.SetInt("recordScore", recordScore);
            scoreText.text = recordScore.ToString();
        }
        else
        {
            scoreText.text = recordScore.ToString();
        }

    }

    public void ToMenu()
    {
        SceneManager.LoadScene(1);
    }
}
