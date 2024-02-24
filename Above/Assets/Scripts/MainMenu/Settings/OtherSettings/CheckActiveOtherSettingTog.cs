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
        AutoSave,
        Particles,
        ShowTheSelectedBoost,
        CameraShake,
        Vibration
    }


    void Awake()
    {
        /*switch (setting)
        {
            case OtherSetting.ShowLevelRank:
                tog.isOn = JsonStorage.instance.jsonData.otherSettings.showLevelRanks;
                break; 
            case OtherSetting.AutoSave:
                tog.isOn = JsonStorage.instance.jsonData.otherSettings.autoSave;
                break;
            case OtherSetting.Particles:
                tog.isOn = JsonStorage.instance.jsonData.otherSettings.particles;
                break;
            case OtherSetting.ShowTheSelectedBoost:
                tog.isOn = JsonStorage.instance.jsonData.otherSettings.showSelectedBoostInGame;
                break;
            case OtherSetting.CameraShake:
                tog.isOn = JsonStorage.instance.jsonData.otherSettings.cameraShake;
                break;
            case OtherSetting.Vibration:
                tog.isOn = JsonStorage.instance.jsonData.otherSettings.vibration;
                break;
        }*/
    }
}
