using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SelectBgInGame : MonoBehaviour
{
    private int i = 0;

    [SerializeField] private GameObject[] AllBg;
    [SerializeField] private GameObject[] AllRailings;
    
    public async Task Start()
    {
        i = await DataBase.instance.LoadDataInt("shop", "equip", "currentBg");

        AllBg[i].SetActive(true);
        AllRailings[i].SetActive(true);

        print (i + " is equiped bg");
    }
}
