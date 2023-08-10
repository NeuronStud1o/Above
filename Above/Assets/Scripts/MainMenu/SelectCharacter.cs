using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectCharacter : MonoBehaviour
{
    private int i;

    public GameObject[] AllCharacters;

    public GameObject[] EquipButtons;
    public GameObject[] EquipedButtons;
    public GameObject[] BuyButtons;

    void Start()
    {
        if (PlayerPrefs.HasKey("CurrentCharacter"))
        {
            i = PlayerPrefs.GetInt("CurrentCharacter");

            EquipedButtons[i].SetActive(true);
            EquipButtons[i].SetActive(false);
        }
        else
        {
            PlayerPrefs.SetInt("CurrentCharacter", i);
        }

        AllCharacters[i].SetActive(true);
    }

    public void Change(int thisCharacter)
    {
        for (int i = 0; i < AllCharacters.Length; i++)
        {
            AllCharacters[i].SetActive(false);
            EquipedButtons[i].SetActive(false);
            EquipButtons[i].SetActive(true);
        }

        PlayerPrefs.SetInt("CurrentCharacter", thisCharacter);

        AllCharacters[thisCharacter].SetActive(true);

        EquipedButtons[thisCharacter].SetActive(true);
        EquipButtons[thisCharacter].SetActive(false);
    }
}
