using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectBg : MonoBehaviour
{
    private int i;

    public GameObject[] AllBg;
    public GameObject[] AllRailings;

    public GameObject[] EquipButtons;
    public GameObject[] EquipedButtons;

    void Start()
    {
        if (PlayerPrefs.HasKey("CurrentBg"))
        {
            i = PlayerPrefs.GetInt("CurrentBg");

            EquipedButtons[i].SetActive(true);
            EquipButtons[i].SetActive(false);
        }
        else
        {
            PlayerPrefs.SetInt("CurrentBg", i);
        }

        AllBg[i].SetActive(true);
        AllRailings[i].SetActive(true);
    }

    public void Change(int thisBg)
    {
        for (int i = 0; i < AllBg.Length; i++)
        {
            AllBg[i].SetActive(false);
            AllRailings[i].SetActive(false);

            EquipedButtons[i].SetActive(false);
            EquipButtons[i].SetActive(true);
        }

        PlayerPrefs.SetInt("CurrentBg", thisBg);

        AllBg[thisBg].SetActive(true);
        AllRailings[thisBg].SetActive(true);

        EquipedButtons[thisBg].SetActive(true);
        EquipButtons[thisBg].SetActive(false);
    }
}
