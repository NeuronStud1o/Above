using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControlInGame : MonoBehaviour
{
    public AudioSource Music;
    public AudioSource[] SFX;

    IEnumerator AudioVolume()
    {
        yield return new WaitForSeconds(0.1f);
        GetComponent<AudioSource>().enabled = true;
    }

    void Start()
    {
        StartCoroutine(AudioVolume());
        Music.volume = PlayerPrefs.GetFloat("Slider3");

        for (int i = 0; i < SFX.Length; i++)
        {
            SFX[i].volume = PlayerPrefs.GetFloat("Slider4");
        }
    }
}
