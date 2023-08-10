using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static IsActiveButton;

public class OnLoadGame : MonoBehaviour
{
    public GameObject[] ActivationWindowsOnLoadGame;

    public GameObject FadeTransition;

    private void Start()
    {
        if (PlayerPrefs.HasKey("Slider") == false)
        {
            PlayerPrefs.SetFloat("Slider", 1);
        }
        if (PlayerPrefs.HasKey("Slider2") == false)
        {
            PlayerPrefs.SetFloat("Slider2", 1);
        }
        if (PlayerPrefs.HasKey("Slider3") == false)
        {
            PlayerPrefs.SetFloat("Slider", 1);
        }
        if (PlayerPrefs.HasKey("Slider4") == false)
        {
            PlayerPrefs.SetFloat("Slider", 1);
        }

        StartCoroutine("Wait");
    }

    IEnumerator Wait()
    {
        for (int i = 0; i < ActivationWindowsOnLoadGame.Length; i++)
        {
            ActivationWindowsOnLoadGame[i].SetActive(true);
        }

        yield return new WaitForSeconds(0.05f);

        for (int i = 0; i < ActivationWindowsOnLoadGame.Length; i++)
        {
            ActivationWindowsOnLoadGame[i].SetActive(false);
        }

        yield return new WaitForSeconds(1.42f);

        FadeTransition.SetActive(false);
    }
}
