using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OfflineManager : MonoBehaviour
{
    public void ToLobby()
    {
        SceneManager.LoadSceneAsync("Authentication");
    }
}
