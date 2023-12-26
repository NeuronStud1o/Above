using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LosePanel : MonoBehaviour
{
    public static LosePanel instance;
    [SerializeField] private Text scoreText;

    private void Start()
    {
        instance = this;
    }

    public async void Death(int lastRunScore)
    {
        int gainedExp;
        int random;
        random = Random.Range(1, 4);

        if (random == 1)
        {
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
}
