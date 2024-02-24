using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControlInGame : MonoBehaviour
{
    /*[SerializeField] private AudioSource[] SFX;

    private AudioSource audioSource;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
        audioSource.volume = JsonStorage.instance.jsonData.audioSettings.musicGame;

        float sfxVolume = JsonStorage.instance.jsonData.audioSettings.sfxGame;

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
    }*/
}
