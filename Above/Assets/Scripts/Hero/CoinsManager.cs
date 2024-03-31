using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinsManager : MonoBehaviour
{
    public static CoinsManager instance;

    public int coinsF;
    public int coinsS;

    [SerializeField] private TextMeshProUGUI moneyTextF;
    [SerializeField] private TextMeshProUGUI moneyTextS;

    void Start()
    {
        instance = this;

        coinsF = JsonStorage.instance.jsonData.userData.coinsF;
        coinsS = JsonStorage.instance.jsonData.userData.coinsS;

        UpdateUI();
    }

    public void UpdateUI()
    {
        moneyTextF.text = coinsF + "";
        moneyTextS.text = coinsS + "";
    }
}