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
    [SerializeField] private BuySystem buySystem;

    void Update()
    {
        if (element == Element.Skin)
        {
            if (buySystem.elements[index] == true)
            {
                gameObject.SetActive(false);
            }
        }

        if (element == Element.Bg)
        {
            if (buySystem.elements2[index] == true)
            {
                gameObject.SetActive(false);
            }
        }

        if (element == Element.Boost)
        {
            if (buySystem.elements3[index] == true)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
