using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopMainButtonsManager : MonoBehaviour
{
    [SerializeField] private List<Image> mainButtons;
    [SerializeField] private Color notActiveButtonColor;

    public void ChangeCollor(Image a)
    {
        foreach (Image i in mainButtons)
        {
            i.color = notActiveButtonColor;
        }

        a.color = Color.white;
    }
}
