using UnityEngine;
using UnityEngine.UI;

public static class AudioControl
{
    public static void SetAudioValue(Slider slider, AudioSource[] audioSources)
    {
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.volume = slider.value;
        }
    }

    public static void SetAudioValue(Slider slider, AudioSource audioSource)
    {
        audioSource.volume = slider.value;
    }

    public static void SetAudioValue(Slider[] sliders, AudioSource[] audioSources)
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            if (sliders[i] != null)
            {
                audioSources[i].volume = sliders[i].value;
            }
        }
    }

    public static void SetAudioSetting(float value, string setting)
    {
        switch (setting)
        {
            case "musicMainMenu":
                JsonStorage.instance.data.audioSettings.musicMainMenu = value;
                break;
            case "sfxMainMenu":
                JsonStorage.instance.data.audioSettings.sfxMainMenu = value;
                break;
            case "musicGame":
                JsonStorage.instance.data.audioSettings.musicGame = value;
                break;
            case "sfxGame":
                JsonStorage.instance.data.audioSettings.sfxGame = value;
                break;
        }

        CryptoHelper.Encrypt(JsonStorage.instance.data, JsonStorage.instance.password);
    }
}
