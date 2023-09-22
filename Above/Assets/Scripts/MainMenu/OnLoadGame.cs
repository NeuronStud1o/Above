using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static IsActiveButton;

public class OnLoadGame : MonoBehaviour
{
    public GameObject[] ActivationWindowsOnLoadGame;
    public GameObject FadeTransition;

    private WaitForSeconds shortDelay = new WaitForSeconds(0.02f);

    private IEnumerator Start()
    {
        foreach (var window in ActivationWindowsOnLoadGame)
        {
            window.SetActive(true);
            yield return shortDelay;
        }

        foreach (var window in ActivationWindowsOnLoadGame)
        {
            window.SetActive(false);
        }

        yield return new WaitForSeconds(1.9f);

        FadeTransition.SetActive(false);
    }
}
