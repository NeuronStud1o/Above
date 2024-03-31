using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsManagerInGame : MonoBehaviour
{
    IEnumerator RetryGameAsync()
    {
        DataBase.instance.SetActiveLoadingScreen(true);

        yield return new WaitForSeconds(3);

        SceneManager.LoadSceneAsync("Game");
    }

    IEnumerator ReturnToMainMenuAsync()
    {
        DataBase.instance.SetActiveLoadingScreen(true);

        yield return new WaitForSeconds(3);

        SceneManager.LoadSceneAsync("MainMenu");
    }
    
    public void RetryGame()
    {
        StartCoroutine(RetryGameAsync());
    }

    public void ToMainMenu()
    {
        StartCoroutine(ReturnToMainMenuAsync());
    }
}
