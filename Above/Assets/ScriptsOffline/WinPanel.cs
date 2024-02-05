using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinPanel : MonoBehaviour
{
    [SerializeField] private Button NextLevelButton;
    [SerializeField] private TextMeshProUGUI Description;

    void Start()
    {
        Description.text = "You have completed level " + LevelManager.instance.eqipedLevel;

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
        SceneManager.LoadSceneAsync("OfflineMenu");
    }

    public void NextLevel()
    {
        LevelManager.instance.eqipedLevel++;
        SceneManager.LoadSceneAsync("OfflineGame");
    }

    public void RetryLevel()
    {
        SceneManager.LoadSceneAsync("OfflineGame");
    }
}
