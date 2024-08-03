using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [Header ("## Audio settings :")]
    public Slider[] sliders;
    public AudioSource[] SFX;

    private AudioSource musicMainMenu;
    private bool isReady = false;

    void Start()
    {
        musicMainMenu = GameManager.instance.gameObject.GetComponent<AudioSource>();

        LoadAudioValue();
    }

    private void LoadAudioValue()
    {
        sliders[0].value = JsonStorage.instance.data.audioSettings.musicMainMenu;
        sliders[1].value = JsonStorage.instance.data.audioSettings.sfxMainMenu;
        sliders[2].value = JsonStorage.instance.data.audioSettings.musicGame;
        sliders[3].value = JsonStorage.instance.data.audioSettings.sfxGame;

        musicMainMenu.enabled = true;

        isReady = true;
    }

    public void SaveAudioSettings()
    {
        if (isReady)
        {
            AudioControl.SetAudioSetting(sliders[0].value, "musicMainMenu");
            AudioControl.SetAudioSetting(sliders[1].value, "sfxMainMenu");
            AudioControl.SetAudioSetting(sliders[2].value, "musicGame");
            AudioControl.SetAudioSetting(sliders[3].value, "sfxGame");

            AudioControl.SetAudioValue(sliders[0], musicMainMenu);
            AudioControl.SetAudioValue(sliders[1], SFX);
        }
    }
}
