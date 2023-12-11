using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioControl : MonoBehaviour
{
    [SerializeField] private Slider[] slider;
    [SerializeField] private AudioSource musicMainMenu;
    [SerializeField] private AudioSource[] SFX;

    private async void Start()
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

        LoadAudioValue();

        SetAudioValue();
    }

    private async void LoadAudioValue()
    {
        slider[0].value = await DataBase.instance.LoadDataFloat("menu", "settings", "audio", "musicMainMenuSA");
        slider[1].value = await DataBase.instance.LoadDataFloat("menu", "settings", "audio", "sfxMainMenuSA");
        slider[2].value = await DataBase.instance.LoadDataFloat("menu", "settings", "audio", "musicGameSA");
        slider[3].value = await DataBase.instance.LoadDataFloat("menu", "settings", "audio", "sfxGameSA");
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
        DataBase.instance.SaveData(slider[0], "menu", "settings", "audio", "musicMainMenuSA");
        DataBase.instance.SaveData(slider[1], "menu", "settings", "audio", "sfxMainMenuSA");
        DataBase.instance.SaveData(slider[2], "menu", "settings", "audio", "musicGameSA");
        DataBase.instance.SaveData(slider[3], "menu", "settings", "audio", "sfxGameSA");

        SetAudioValue();
    }
}
