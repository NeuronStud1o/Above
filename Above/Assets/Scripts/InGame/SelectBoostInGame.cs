using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectBoostInGame : MonoBehaviour
{
    [SerializeField] private Color shieldColor;
    [SerializeField] private Color standartColor;

    [SerializeField] private GameObject[] Heroes;
    [SerializeField] private GameObject[] AllBoosts;

    private int i;

    void Start()
    {
        i = JsonStorage.instance.jsonData.currentShop.currentBg;

        if (i == 3)
        {
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

        AllBoosts[i].SetActive(true);
    }
}
