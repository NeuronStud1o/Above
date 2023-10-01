using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinsManagerInMainMenu : MonoBehaviour
{
    public static int coinsF;

    public static CoinsManagerInMainMenu instance;

    public static int coinsS;

    [SerializeField] private TextMeshProUGUI SuperCoinsText;
    [SerializeField] private TextMeshProUGUI FlyCoinsText;
    [SerializeField] private TextMeshProUGUI SuperCoinsInShopText;
    [SerializeField] private TextMeshProUGUI FlyCoinsInShopText;

    void Start()
    {
        instance = this;

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

        SuperCoinsText.text = coinsS + "";
        FlyCoinsText.text = coinsF + "";
        SuperCoinsInShopText.text = coinsS + "";
        FlyCoinsInShopText.text = coinsF + "";
    }

    public void UpdateUI()
    {
        coinsF = PlayerPrefs.GetInt("coinsF", coinsF);
        coinsS = PlayerPrefs.GetInt("coinsS");

        SuperCoinsText.text = coinsS + "";
        FlyCoinsText.text = coinsF + "";
        SuperCoinsInShopText.text = coinsS + "";
        FlyCoinsInShopText.text = coinsF + "";
    }
}
