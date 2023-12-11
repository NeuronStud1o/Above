using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControlInGame : MonoBehaviour
{
    [SerializeField] private AudioSource music;
    [SerializeField] private AudioSource[] SFX;

    async void Start()
    {
        StartCoroutine(AudioVolume());

        music.volume = await DataBase.instance.LoadDataFloat("menu", "settings", "audio", "musicGameSA");
        float sfxVolume = await DataBase.instance.LoadDataFloat("menu", "settings", "audio", "sfxGameSA");

        for (int i = 0; i < SFX.Length; i++)
        {
            SFX[i].volume = sfxVolume;
        }
    }

    IEnumerator AudioVolume()
    {
        yield return new WaitForSeconds(0.1f);

        GetComponent<AudioSource>().enabled = true;
    }
}
