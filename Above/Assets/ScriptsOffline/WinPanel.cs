using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinPanel : MonoBehaviour
{
    [SerializeField] private Button NextLevelButton;

    void Start()
    {
        if (PlayerPrefs.GetInt("Levels") <= LevelManager.instance.eqipedLevel)
        {
            PlayerPrefs.SetInt("Levels", PlayerPrefs.GetInt("Levels") + 1);
        }

        if (LevelManager.instance.eqipedLevel == 32)
        {
            NextLevelButton.interactable = false;
        }
    }

    public void GoToMenu()
    {
        GameManager.instance.SetActiveLoadingScreen(true);

        GameManager.instance.GetComponent<AudioSource>().enabled = true;
        SceneManager.LoadSceneAsync("OfflineMenu");
    }

    public void NextLevel()
    {
        GameManager.instance.SetActiveLoadingScreen(true);

        LevelManager.instance.eqipedLevel++;
        SceneManager.LoadSceneAsync("OfflineGame");
    }

    public void RetryLevel()
    {
        GameManager.instance.SetActiveLoadingScreen(true);
        
        SceneManager.LoadSceneAsync("OfflineGame");
    }
}
