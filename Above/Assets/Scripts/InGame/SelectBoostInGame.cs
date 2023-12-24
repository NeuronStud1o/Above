using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectBoostInGame : MonoBehaviour
{
    [SerializeField] private Color shieldColor;
    [SerializeField] private Color standartColor;

    [SerializeField] private GameObject[] Heroes;
    [SerializeField] private GameObject[] AllBoosts;
    private int i = 0;
    private int currentBoost;

    private async void Start()
    {
        currentBoost = await DataBase.instance.LoadDataInt("shop", "equip", "currentBoost");

        if (currentBoost == 3)
        {
            DataBase.instance.SaveData(1, "player", "hp");

            for (int i = 0; i < Heroes.Length; i++)
            {
                Heroes[i].GetComponent<SpriteRenderer>().color = shieldColor;
            }
        }
        else
        {
            for (int i = 0; i < Heroes.Length; i++)
            {
                Heroes[i].GetComponent<SpriteRenderer>().color = standartColor;
            }
        }

        i = await DataBase.instance.LoadDataInt("shop", "equip", "currentBoost");
        AllBoosts[i].SetActive(true);
    }
}
