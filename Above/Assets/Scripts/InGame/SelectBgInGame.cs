using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectBgInGame : MonoBehaviour
{
    private int i = 0;

    [SerializeField] private GameObject[] AllBg;
    [SerializeField] private GameObject[] AllRailings;

    async void Start()
    {
        i = await DataBase.instance.LoadDataInt("shop", "equip", "currentBg");

        AllBg[i].SetActive(true);
        AllRailings[i].SetActive(true);
    }
}
