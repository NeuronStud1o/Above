using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectBoost : MonoBehaviour
{
    private int i;

    [SerializeField] private GameObject[] AllBoosts;
    [SerializeField] private GameObject[] Heroes;

    [SerializeField] private GameObject[] EquipButtons;
    [SerializeField] private GameObject[] EquipedButtons;
    [SerializeField] private GameObject[] BuyButtons;

    [SerializeField] private Color shieldColor;
    [SerializeField] private Color standartColor;

    void Start()
    {
        if (PlayerPrefs.HasKey("CurrentBoost"))
        {
            i = PlayerPrefs.GetInt("CurrentBoost");

            EquipedButtons[i].SetActive(true);
            EquipButtons[i].SetActive(false);
        }
        else
        {
            PlayerPrefs.SetInt("CurrentBoost", i);
        }

        AllBoosts[i].SetActive(true);
    }

    private void Update()
    {
        if (PlayerPrefs.GetInt("CurrentBoost") == 0)
        {
            PlayerPrefs.SetInt("IsBoostEquiped", 0);
        }

        if (PlayerPrefs.GetInt("CurrentBoost") == 1)
        {
            PlayerPrefs.SetInt("CoinsFAdd", 2);
            PlayerPrefs.SetInt("IsBoostEquiped", 1);
        }
        else
        {
            PlayerPrefs.SetInt("CoinsFAdd", 1);
        }

        if (PlayerPrefs.GetInt("CurrentBoost") == 2)
        {
            PlayerPrefs.SetFloat("Speed", 1.5f);
            PlayerPrefs.SetInt("IsBoostEquiped", 1);
        }
        else
        {
            PlayerPrefs.SetFloat("Speed", 2.2f);
        }

        if (PlayerPrefs.GetInt("CurrentBoost") == 3)
        {
            PlayerPrefs.SetInt("IsBoostEquiped", 1);

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
    }

    public void Change(int thisCharacter)
    {
        for (int i = 0; i < AllBoosts.Length; i++)
        {
            AllBoosts[i].SetActive(false);
            EquipedButtons[i].SetActive(false);
            EquipButtons[i].SetActive(true);
        }

        PlayerPrefs.SetInt("CurrentBoost", thisCharacter);

        AllBoosts[thisCharacter].SetActive(true);

        EquipedButtons[thisCharacter].SetActive(true);
        EquipButtons[thisCharacter].SetActive(false);
    }
}
