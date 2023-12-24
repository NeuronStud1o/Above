using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LosePanel : MonoBehaviour
{
    public static LosePanel instance;
    [SerializeField] private Text scoreText;
    [SerializeField] private Camera thisCamera;
    [SerializeField] private GameObject panel;

    public int lastRunScore = 0;

    private async void Start()
    {
        instance = this;

        int gainedExp;
        int random;
        random = Random.Range(1, 4);

        if (random == 1)
        {
            thisCamera.rect = new Rect(0f, 0f, 1f, 1f);
            panel.SetActive(true);
            UnityInterstitialAd.Instace.ShowAd();
        }
        
        int recordScore = await DataBase.instance.LoadDataInt("game", "recordScore");
        gainedExp = lastRunScore / 14;

        if (gainedExp > 100)
        {
            gainedExp = 100;
        }

        int doneExp = await DataBase.instance.LoadDataInt("menu", "levelManager", "exp") + gainedExp;
        DataBase.instance.SaveData(doneExp, "menu", "levelManager", "exp");

        if (lastRunScore > recordScore)
        {
            recordScore = lastRunScore;
            DataBase.instance.SaveData(recordScore, "game", "recordScore");
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
