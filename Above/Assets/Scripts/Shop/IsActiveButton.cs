using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsActiveButton : MonoBehaviour
{
    public int index;

    public enum Element
    {
        Skin,
        Bg,
        Boost
    }

    public Element element;

    void Update()
    {
        if (element == Element.Skin)
        {
            if (BuySystem.elements[index] == true)
            {
                gameObject.SetActive(false);
            }
        }

        if (element == Element.Bg)
        {
            if (BuySystem.elements2[index] == true)
            {
                gameObject.SetActive(false);
            }
        }

        if (element == Element.Boost)
        {
            if (BuySystem.elements3[index] == true)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
