using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsManagerInGame : MonoBehaviour
{
    [SerializeField] private GameObject playTransition;
    [SerializeField] private GameObject fadeTransition;

    IEnumerator RetryGameAsync()
    {
        yield return new WaitForSeconds(3);

        SceneManager.LoadSceneAsync("Game");
    }

    IEnumerator ReturnToMainMenuAsync()
    {
        fadeTransition.SetActive(true);

        yield return new WaitForSeconds(3);

        SceneManager.LoadSceneAsync("MainMenu");
    }
    
    public void RetryGame()
    {
        playTransition.SetActive(true);

        StartCoroutine(RetryGameAsync());
    }

    public void ToMainMenu()
    {
        StartCoroutine(ReturnToMainMenuAsync());
    }
}
