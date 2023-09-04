using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinsManagerInMainMenu : MonoBehaviour
{
    public static int coinsF;
    public TextMeshProUGUI moneyText;

    public static int coinsS;
    public TextMeshProUGUI moneyText2;

    void Start()
    {
        if (PlayerPrefs.GetInt("coinsS") >= 10)
        {
            PlayerPrefs.SetInt("coinsSForTasks", 1);
        }

        if (PlayerPrefs.HasKey("coinsF"))
        {
            coinsF = PlayerPrefs.GetInt("coinsF", coinsF);
        }

        if (PlayerPrefs.HasKey("coinsS"))
        {
            coinsS = PlayerPrefs.GetInt("coinsS");
        }
    }
    void FixedUpdate()
    {
        coinsF = PlayerPrefs.GetInt("coinsF");
        PlayerPrefs.SetInt("coinsF", coinsF);
        moneyText.text = "" + coinsF;

        coinsS = PlayerPrefs.GetInt("coinsS");
        PlayerPrefs.SetInt("coinsS", coinsS);
        moneyText2.text = "" + coinsS;
    }
}
// sws
