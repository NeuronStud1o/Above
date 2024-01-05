using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class AudioControl : MonoBehaviour
{
    [SerializeField] private Slider[] slider;
    [SerializeField] private AudioSource musicMainMenu;
    [SerializeField] private AudioSource[] SFX;

    public async Task Start()
    {
        bool isAudioSettings = await DataBase.instance.LoadDataCheck("boolean", "ftAudio");

        if (!isAudioSettings)
        {
            DataBase.instance.SaveData(1, "menu", "settings", "audio", "musicMainMenuSA");
            DataBase.instance.SaveData(1, "menu", "settings", "audio", "sfxMainMenuSA");
            DataBase.instance.SaveData(1, "menu", "settings", "audio", "musicGameSA");
            DataBase.instance.SaveData(1, "menu", "settings", "audio", "sfxGameSA");

            DataBase.instance.SaveData("done", "boolean", "ftAudio");
        }

        await LoadAudioValue();
    }

    private async Task LoadAudioValue()
    {
        float s1Value = await DataBase.instance.LoadDataFloat("menu", "settings", "audio", "musicMainMenuSA");
        float s2Value = await DataBase.instance.LoadDataFloat("menu", "settings", "audio", "sfxMainMenuSA");
        float s3Value = await DataBase.instance.LoadDataFloat("menu", "settings", "audio", "musicGameSA");
        float s4Value = await DataBase.instance.LoadDataFloat("menu", "settings", "audio", "sfxGameSA");

        slider[0].value = s1Value;
        slider[1].value = s2Value;
        slider[2].value = s3Value;
        slider[3].value = s4Value;

        musicMainMenu.enabled = true;
        SetAudioValue();
    }

    private void SetAudioValue()
    {
        musicMainMenu.volume = slider[0].value;

        for (int i = 0; i < SFX.Length; i++)
        {
            SFX[i].volume = slider[1].value;
        }
    }

    public void SaveAudioSettings()
    {
        DataBase.instance.SaveData(slider[0].value, "menu", "settings", "audio", "musicMainMenuSA");
        DataBase.instance.SaveData(slider[1].value, "menu", "settings", "audio", "sfxMainMenuSA");
        DataBase.instance.SaveData(slider[2].value, "menu", "settings", "audio", "musicGameSA");
        DataBase.instance.SaveData(slider[3].value, "menu", "settings", "audio", "sfxGameSA");

        SetAudioValue();
    }
}
