using System.Collections;
using UnityEngine;

public class AudioControlInGame : MonoBehaviour
{
    [SerializeField] private AudioSource[] SFX;

    private AudioSource audioSource;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
        audioSource.volume = JsonStorage.instance.data.audioSettings.musicGame;

        float sfxVolume = JsonStorage.instance.data.audioSettings.sfxGame;

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
