using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class WIFIChecking : MonoBehaviour
{
    public static WIFIChecking instance;

    [SerializeField] private GameObject errorPanel;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        instance = this;
    }

    void Start()
    {
        RepeatCheck();
    }

    IEnumerator CheckInternetConection()
    {
        UnityWebRequest request = new UnityWebRequest("https://www.google.com/");

        yield return request.SendWebRequest();

        Scene currentScene = SceneManager.GetActiveScene();

        if (request.error != null)
        {
            errorPanel.SetActive(true);
            JsonStorage.instance.ActivateTimer(false);

            if (currentScene.name == "Game")
            {
                Debug.Log("Stop player");

                PauseController.instance.Hero.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                PauseController.instance.Hero.GetComponent<Player>().isCanMove = false;
            }
        }
        else
        {
            errorPanel.SetActive(false);

            if (JsonStorage.instance.isFrozenTimer)
            {
                JsonStorage.instance.ActivateTimer(true);
            }
            
            if (currentScene.name == "Game")
            {
                if (PauseController.instance.isPause == false && PauseController.instance.Hero != null)
                {
                    PauseController.instance.Hero.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

                    if (StartOnClick.instance == null)
                    {
                        PauseController.instance.Hero.GetComponent<Player>().isCanMove = true;
                    }
                }
            }
        }
    }

    void CheckInternet()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        if (currentScene.name == "Authentication")
        {
            Destroy(gameObject);

            return;
        }

        StartCoroutine(CheckInternetConection());
    }

    void RepeatCheck()
    {
        InvokeRepeating("CheckInternet", 3, 3);
    }
}
