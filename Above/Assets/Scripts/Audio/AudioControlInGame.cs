using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class AudioControlInGame : MonoBehaviour
{
    [SerializeField] private AudioSource[] SFX;

    private AudioSource audioSource;

    void Start()
    {
        OnLoadGame.instance.scriptsList.Add(StartActivity());
    }

    public async Task StartActivity()
    {
        audioSource = GetComponent<AudioSource>();
        
        audioSource.volume = await DataBase.instance.LoadDataFloat("menu", "settings", "audio", "musicGameSA");

        float sfxVolume = await DataBase.instance.LoadDataFloat("menu", "settings", "audio", "sfxGameSA");

        for (int i = 0; i < SFX.Length; i++)
        {
            SFX[i].volume = sfxVolume;
        }
        
        StartCoroutine(GameMusicOn());
    }

    private IEnumerator GameMusicOn()
    {
        yield return new WaitForSeconds(0.5f);
        audioSource.enabled = true;
    }
}
