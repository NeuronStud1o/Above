using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OfflineManager : MonoBehaviour
{
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
        LevelManager.instance.eqipedLevel = index;
        DataBase.instance.GetComponent<AudioSource>().enabled = false;

        SceneManager.LoadSceneAsync("OfflineGame");
    }
}
