using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioControl : MonoBehaviour
{
    [SerializeField] private Slider[] slider;

    [SerializeField] private AudioSource AllAudio;
    [SerializeField] private AudioSource[] SFX;

    private void Start()
    {
        if (PlayerPrefs.GetInt("FirstTimeInGameAudio") == 0)
        {
            PlayerPrefs.SetFloat("Slider", 1f);
            PlayerPrefs.SetFloat("Slider2", 1f);
            PlayerPrefs.SetFloat("Slider3", 1f);
            PlayerPrefs.SetFloat("Slider4", 1f);
            PlayerPrefs.SetInt("FirstTimeInGameAudio", 1);
        }

        slider[0].value = PlayerPrefs.GetFloat("Slider");
        slider[1].value = PlayerPrefs.GetFloat("Slider2");
        slider[2].value = PlayerPrefs.GetFloat("Slider3");
        slider[3].value = PlayerPrefs.GetFloat("Slider4");

        SaveAudioSettings();
    }

    public void SaveAudioSettings()
    {
        AllAudio.volume = slider[0].value;

        for (int i = 0; i < SFX.Length; i++)
        {
            SFX[i].volume = slider[1].value;
        }

        PlayerPrefs.SetFloat("Slider", slider[0].value);
        PlayerPrefs.SetFloat("Slider2", slider[1].value);
        PlayerPrefs.SetFloat("Slider3", slider[2].value);
        PlayerPrefs.SetFloat("Slider4", slider[3].value);
    }
}
