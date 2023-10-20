using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificButtons : MonoBehaviour
{
    public void OpenShopMenu()
    {
        ActivityNotificManager.instance.OpenShop();
    }
}
