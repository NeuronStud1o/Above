using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class CoinsManager : MonoBehaviour
{
    public int coinsF;
    public int coinsS;

    [SerializeField] private Text moneyTextF;
    [SerializeField] private Text moneyTextS;

    public static CoinsManager instance;

    void Start()
    {
        OnLoadGame.instance.scriptsList.Add(StartActivity());
    }

    public async Task StartActivity()
    {
        instance = this;
        
        if (await DataBase.instance.LoadDataCheck("shop", "equip", "boosts", "flyCoinsToAdd") == false)
        {
            DataBase.instance.SaveData(1, "shop", "equip", "boosts", "flyCoinsToAdd");
        }

        coinsF = await DataBase.instance.LoadDataInt("menu", "coins", "flyCoins");
        coinsS = await DataBase.instance.LoadDataInt("menu", "coins", "superCoins");

        UpdateUI();
    }

    public void UpdateUI()
    {
        moneyTextF.text = coinsF + "";
        moneyTextS.text = coinsS + "";
    }
}