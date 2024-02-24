using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharacterInGame : MonoBehaviour
{
    private int i = 0;

    [SerializeField] private GameObject[] AllCharacters;

    void Start()
    {
        //i = JsonStorage.instance.jsonData.currentShop.currentSkin;

        AllCharacters[i].SetActive(true);
    }
}
