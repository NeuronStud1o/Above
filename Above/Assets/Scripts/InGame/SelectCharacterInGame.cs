using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharacterInGame : MonoBehaviour
{
    private int i;

    public GameObject[] AllCharacters;

    void Start()
    {
        if (PlayerPrefs.HasKey("CurrentCharacter"))
        {
            i = PlayerPrefs.GetInt("CurrentCharacter");
        }
        else
        {
            PlayerPrefs.SetInt("CurrentCharacter", i);
        }

        AllCharacters[i].SetActive(true);
    }
}
