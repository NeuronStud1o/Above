using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharacterInGame : MonoBehaviour
{
    private int i = 0;

    [SerializeField] private GameObject[] AllCharacters;

    async void Start()
    {
        if (await DataBase.instance.LoadDataCheck("shop", "equip", "currentCharacter") == false)
        {
            await DataBase.instance.SaveDataAsync(0, "shop", "equip", "currentCharacter");
        }

        i = await DataBase.instance.LoadDataInt("shop", "equip", "currentCharacter");

        AllCharacters[i].SetActive(true);
    }
}
