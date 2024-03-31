using System.Collections;
using System.Collections.Generic;
using System.IO;
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
        
        int recordScore = JsonStorage.instance.jsonData.userData.record;
        gainedExp = lastRunScore / 14;

        if (gainedExp > 100)
        {
            gainedExp = 100;
        }

        int doneExp = JsonStorage.instance.jsonData.userData.exp + gainedExp;

        JsonStorage.instance.jsonData.userData.exp = doneExp;

        if (lastRunScore > recordScore)
        {
            recordScore = lastRunScore;
            JsonStorage.instance.jsonData.userData.record = recordScore;
        }

        string filePath = Path.Combine(Application.persistentDataPath, "gameData.json");

        CryptoHelper.Encrypt(filePath, JsonStorage.instance.jsonData, JsonStorage.instance.password);

        scoreText.text = recordScore.ToString();
    }
}
