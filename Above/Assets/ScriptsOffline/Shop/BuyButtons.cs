using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BuyButtons : MonoBehaviour
{
    [SerializeField] private string nameSkin;
    [SerializeField] private BuySystem buySystem;
    Skins.OpenSkin skin;

    void Start()
    {
        skin = buySystem.skins.skinsList.FirstOrDefault(item => item.name == nameSkin);

        if (skin.isOpen)
        {
            gameObject.SetActive(false);
        }
    }

    public void BuySkins()
    {
        if (skin.isFlyCoin == true)
        {
            if (PlayerPrefs.GetInt("coinsF") >= skin.price && skin.isOpen == false)
            {
                skin.isOpen = true;

                buySystem.SaveToJson();

                gameObject.SetActive(false);
                PlayerPrefs.SetInt("coinsF", PlayerPrefs.GetInt("coinsF") - skin.price);
                
                buySystem.UpdateUI();
            }
        }
        else
        {
            if (PlayerPrefs.GetInt("coinsS") >= skin.price && skin.isOpen == false)
            {
                skin.isOpen = true;

                buySystem.SaveToJson();
                
                gameObject.SetActive(false);
                PlayerPrefs.SetInt("coinsS", PlayerPrefs.GetInt("coinsS") - skin.price);

                buySystem.UpdateUI();
            }
        }
    }
}
