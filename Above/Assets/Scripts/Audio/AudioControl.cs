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
        
        bool isAudioSettings = JsonStorage.instance.data.boolean.isFirstTimeAudio;

        if (!isAudioSettings)
        {
            JsonStorage.instance.data.audioSettings.musicGame = 0.5f;
            JsonStorage.instance.data.audioSettings.musicMainMenu = 0.5f;
            JsonStorage.instance.data.audioSettings.sfxGame = 1;
            JsonStorage.instance.data.audioSettings.sfxMainMenu = 1;

            JsonStorage.instance.data.boolean.isFirstTimeAudio = true;
        }

        LoadAudioValue();
    }

    private void LoadAudioValue()
    {
        float musicMainMenuValue = JsonStorage.instance.data.audioSettings.musicMainMenu;
        float sfxMainMenuValue = JsonStorage.instance.data.audioSettings.sfxMainMenu;
        float musicGameValue = JsonStorage.instance.data.audioSettings.musicGame;
        float sfxGameValue = JsonStorage.instance.data.audioSettings.sfxGame;

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
        JsonStorage.instance.data.audioSettings.musicMainMenu = slider[0].value;
        JsonStorage.instance.data.audioSettings.sfxMainMenu = slider[1].value;
        JsonStorage.instance.data.audioSettings.musicGame = slider[2].value;
        JsonStorage.instance.data.audioSettings.sfxGame = slider[3].value;

        CryptoHelper.Encrypt(JsonStorage.instance.data, JsonStorage.instance.password);

        SetAudioValue();
    }
}
