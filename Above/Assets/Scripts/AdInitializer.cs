using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using GoogleMobileAds.Api;

public class AdInitializer : MonoBehaviour
{
    void Awake()
    {
        //MobileAds.Initialize(initStatus => { } );
        DontDestroyOnLoad(gameObject);
    }
}
