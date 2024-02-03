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
        SceneManager.LoadSceneAsync("Authentication");
    }
}
