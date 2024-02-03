using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinPanel : MonoBehaviour
{
    void Start()
    {
        PlayerPrefs.SetInt("Levels", PlayerPrefs.GetInt("Levels") + 1);
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
}
