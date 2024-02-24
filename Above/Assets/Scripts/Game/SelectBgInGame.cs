using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectBgInGame : MonoBehaviour
{
    private int i = 0;

    [SerializeField] private GameObject[] AllBg;
    [SerializeField] private GameObject[] AllRailings;
    
    void Start()
    {
        StartCoroutine(StartActivity());
    }

    IEnumerator StartActivity()
    {
        //i = JsonStorage.instance.jsonData.currentShop.currentBg;

        AllBg[i].SetActive(true);
        AllRailings[i].SetActive(true);

        yield return new WaitForSeconds(1f);

        //DataBase.instance.SetActiveLoadingScreen(false);
    }
}
