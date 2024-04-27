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
        
        int recordScore = JsonStorage.instance.data.userData.record;
        gainedExp = lastRunScore / 14;

        if (gainedExp > 100)
        {
            gainedExp = 100;
        }

        int doneExp = JsonStorage.instance.data.userData.exp + gainedExp;

        JsonStorage.instance.data.userData.exp = doneExp;

        if (lastRunScore > recordScore)
        {
            recordScore = lastRunScore;
            JsonStorage.instance.data.userData.record = recordScore;
        }

        CryptoHelper.Encrypt(JsonStorage.instance.data, JsonStorage.instance.password);

        scoreText.text = recordScore.ToString();
    }
}
