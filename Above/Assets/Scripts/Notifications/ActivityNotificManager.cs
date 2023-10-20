using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivityNotificManager : MonoBehaviour
{
    public static ActivityNotificManager instance;
    [SerializeField] private GameObject ShopArrow;
    [SerializeField] private GameObject Shop;

    void Start()
    {
        instance = this;
    }

    public void OpenShop()
    {
        Shop.SetActive(true);
        ShopArrow.SetActive(true);
    }

    public void ExitShop()
    {
        StartCoroutine(ExitShopArrow());
    }

    public void CloseShopArrow()
    {
        ShopArrow.SetActive(false);
    }

    IEnumerator ExitShopArrow()
    {
        yield return new WaitForSeconds(1);
        ShopArrow.SetActive(false);
    }
}
