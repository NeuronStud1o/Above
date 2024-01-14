using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioControl : MonoBehaviour
{
    [SerializeField] private Slider[] slider;
    private AudioSource musicMainMenu;
    [SerializeField] private AudioSource[] SFX;

    void Start()
    {
        musicMainMenu = DataBase.instance.gameObject.GetComponent<AudioSource>();
        
        bool isAudioSettings = JsonStorage.instance.jsonData.boolean.isFirstTimeAudio;

        if (!isAudioSettings)
        {
            JsonStorage.instance.jsonData.audioSettings.musicGame = 1;
            JsonStorage.instance.jsonData.audioSettings.musicMainMenu = 1;
            JsonStorage.instance.jsonData.audioSettings.sfxGame = 1;
            JsonStorage.instance.jsonData.audioSettings.sfxMainMenu = 1;

            JsonStorage.instance.jsonData.boolean.isFirstTimeAudio = true;

            JsonStorage.instance.SaveData();
        }

        LoadAudioValue();
    }

    private void LoadAudioValue()
    {
        float musicMainMenuValue = JsonStorage.instance.jsonData.audioSettings.musicMainMenu;
        float sfxMainMenuValue = JsonStorage.instance.jsonData.audioSettings.sfxMainMenu;
        float musicGameValue = JsonStorage.instance.jsonData.audioSettings.musicGame;
        float sfxGameValue = JsonStorage.instance.jsonData.audioSettings.sfxGame;

        slider[0].value = musicMainMenuValue;
        slider[1].value = sfxMainMenuValue;
        slider[2].value = musicGameValue;
        slider[3].value = sfxGameValue;

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
        JsonStorage.instance.jsonData.audioSettings.musicMainMenu = slider[0].value;
        JsonStorage.instance.jsonData.audioSettings.sfxMainMenu = slider[1].value;
        JsonStorage.instance.jsonData.audioSettings.musicGame = slider[2].value;
        JsonStorage.instance.jsonData.audioSettings.sfxGame = slider[3].value;

        JsonStorage.instance.SaveData();

        SetAudioValue();
    }
}
