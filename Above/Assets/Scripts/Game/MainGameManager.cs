using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGameManager : MonoBehaviour
{
    public static MainGameManager instance;

    [Header ("## Audio :")]
    [SerializeField] private AudioSource[] SFX;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    [HideInInspector]
    public AudioSource audioSource;

    private bool isReady = false;
    
    void Start()
    {
        instance = this;

        audioSource = GetComponent<AudioSource>();

        musicSlider.value = JsonStorage.instance.data.audioSettings.musicGame;
        sfxSlider.value = JsonStorage.instance.data.audioSettings.sfxGame;
        Debug.Log(sfxSlider.value + "sfx slider");

        audioSource.volume = musicSlider.value;

        float sfxVolume = JsonStorage.instance.data.audioSettings.sfxGame;

        for (int i = 0; i < SFX.Length; i++)
        {
            SFX[i].volume = sfxVolume;
        }

        isReady = true;
        
        StartCoroutine(GameMusicOn());
    }

    private IEnumerator GameMusicOn()
    {
        yield return new WaitForSeconds(0.5f);

        audioSource.enabled = true;
    }

    public void SaveAudioSettings()
    {
        if (isReady)
        {
            AudioControl.SetAudioSetting(musicSlider.value, "musicGame");
            AudioControl.SetAudioSetting(sfxSlider.value, "sfxGame");

            AudioControl.SetAudioValue(musicSlider, audioSource);
            AudioControl.SetAudioValue(sfxSlider, SFX);
        }
    }
}
