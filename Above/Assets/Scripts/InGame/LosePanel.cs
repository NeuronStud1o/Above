using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LosePanel : MonoBehaviour
{
    public static LosePanel instance;
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Start()
    {
        instance = this;
    }

    public void Death(int lastRunScore)
    {
        int gainedExp;
        int random;
        random = Random.Range(1, 4);

        if (random == 1)
        {
            //UnityInterstitialAd.Instace.ShowAd();
        }
        
        int recordScore = JsonStorage.instance.jsonData.userData.record;
        gainedExp = lastRunScore / 14;

        if (gainedExp > 100)
        {
            gainedExp = 100;
        }

        int doneExp = JsonStorage.instance.jsonData.userData.exp + gainedExp;

        JsonStorage.instance.jsonData.userData.exp = doneExp;
        JsonStorage.instance.SaveData();

        if (lastRunScore > recordScore)
        {
            recordScore = lastRunScore;
            JsonStorage.instance.jsonData.userData.record = recordScore;
            JsonStorage.instance.SaveData();
        }

        scoreText.text = recordScore.ToString();
    }
}
