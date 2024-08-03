using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckActiveOtherSettingTog : MonoBehaviour
{
    [SerializeField] private Toggle tog;
    [SerializeField] private OtherSetting setting;

    enum OtherSetting
    {
        ShowLevelRank,
        Particles,
        ShowTheSelectedBoost,
        CameraShake,
        Vibration
    }


    void Awake()
    {
        switch (setting)
        {
            case OtherSetting.ShowLevelRank:
                tog.isOn = JsonStorage.instance.data.otherSettings.showLevelRanks;
                break; 
            case OtherSetting.Particles:
                tog.isOn = JsonStorage.instance.data.otherSettings.particles;
                break;
            case OtherSetting.ShowTheSelectedBoost:
                tog.isOn = JsonStorage.instance.data.otherSettings.showSelectedBoostInGame;
                break;
            case OtherSetting.CameraShake:
                tog.isOn = JsonStorage.instance.data.otherSettings.cameraShake;
                break;
            case OtherSetting.Vibration:
                tog.isOn = JsonStorage.instance.data.otherSettings.vibration;
                break;
        }
    }
}
