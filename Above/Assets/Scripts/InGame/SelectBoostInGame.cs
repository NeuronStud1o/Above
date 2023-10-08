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

    private void Start()
    {
        if (PlayerPrefs.GetInt("IsBoostEquiped") == 1)
        {
            PlayerPrefs.SetInt("BoostIsEquiped", 1);
        }

        if (PlayerPrefs.GetInt("CurrentBoost") == 3)
        {
            PlayerPrefs.SetInt("HeroHP", 1);

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

        i = PlayerPrefs.GetInt("CurrentBoost");
        AllBoosts[i].SetActive(true);
    }
}
