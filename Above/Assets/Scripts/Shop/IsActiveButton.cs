using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Element
{
    Skin,
    Bg,
    Boost
}

public class IsActiveButton : MonoBehaviour
{
    [SerializeField] private BuySystem buySystem;

    public int index;
    public Element element;

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
