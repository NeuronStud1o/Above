using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OfflineManager : MonoBehaviour
{
    void Start()
    {
        DataBase.instance.SetActiveLoadingScreen(false);

        if (WIFIChecking.instance != null)
        {
            WIFIChecking.instance.ChangeSceneName();
        }
    }

    public void EquipHero(int index)
    {
        LevelManager.instance.equipedHero = index;
    }

    public void ToLobby()
    {
        DataBase.instance.gameObject.GetComponent<AudioSource>().volume = 0.2f;
        SceneManager.LoadSceneAsync("Authentication");
    }

    public void StartGame(int index)
    {
        StartCoroutine(GameIsStarted(index));
    }

    private IEnumerator GameIsStarted(int index)
    {
        DataBase.instance.SetActiveLoadingScreen(true);

        yield return new WaitForSeconds(0.5f);

        LevelManager.instance.eqipedLevel = index;
        DataBase.instance.GetComponent<AudioSource>().enabled = false;

        SceneManager.LoadSceneAsync("OfflineGame");
    }
}
