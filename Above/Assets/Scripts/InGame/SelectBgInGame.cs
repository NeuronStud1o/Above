using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectBgInGame : MonoBehaviour
{
    private int i;

    [SerializeField] private GameObject[] AllBg;
    [SerializeField] private GameObject[] AllRailings;

    void Start()
    {
        if (PlayerPrefs.HasKey("CurrentBg"))
        {
            i = PlayerPrefs.GetInt("CurrentBg");
        }
        else
        {
            PlayerPrefs.SetInt("CurrentBg", i);
        }

        AllBg[i].SetActive(true);
        AllRailings[i].SetActive(true);
    }
}
