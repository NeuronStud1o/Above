using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipButtons : MonoBehaviour
{
    [SerializeField] private BuySystem buySystem;
    [SerializeField] private int index;

    void Start()
    {
        if (LevelManager.instance.equipedHero == index)
        {
            gameObject.SetActive(false);
        }
    }

    public void EquipHero()
    {
        PlayerPrefs.SetInt("currentHero", index);
        LevelManager.instance.equipedHero = index;

        foreach (GameObject button in buySystem.equipButtons)
        {
            button.SetActive(true);
        }

        buySystem.equipButtons[index].SetActive(false);
    }
}
