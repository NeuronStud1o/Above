using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class WIFIChecking : MonoBehaviour
{
    public static WIFIChecking instance;
    [SerializeField] private GameObject errorPanel;
    [SerializeField] private GameObject playOfflineButton;

    private string sceneName;
    private bool isAuthInitialized = false;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        RepeatCheck();
    }

    IEnumerator CheckInternetConection()
    {
        UnityWebRequest request = new UnityWebRequest("https://www.google.com/");

        yield return request.SendWebRequest();

        if (request.error != null)
        {
            errorPanel.SetActive(true);
            JsonStorage.instance.ActivateTimer(false);

            if (sceneName == "Game")
            {
                Debug.Log("Stop player");

                PauseController.instance.Hero.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                PauseController.instance.Hero.GetComponent<Player>().isCanMove = false;
            }
        }
        else
        {
            errorPanel.SetActive(false);

            if (JsonStorage.instance != null)
            {
                if (JsonStorage.instance.isFrozenTimer)
                {
                    JsonStorage.instance.ActivateTimer(true);
                }
            }
            
            if (sceneName == "Game")
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

    IEnumerator AuthCheckWIFI()
    {
        UnityWebRequest request = new UnityWebRequest("https://www.google.com/");

        var asyncOperation = request.SendWebRequest();

        yield return asyncOperation;

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            errorPanel.SetActive(true);
        }
        else
        {
            FirebaseAuthManager.instance.StartAction();
            errorPanel.SetActive(false);

            isAuthInitialized = true;

        }
    }

    void CheckInternet()
    {
        if (sceneName == "Authentication" && !isAuthInitialized)
        {
            StartCoroutine(AuthCheckWIFI());
            return;
        }

        StartCoroutine(CheckInternetConection());
    }

    void RepeatCheck()
    {
        ChangeSceneName();

        InvokeRepeating("CheckInternet", 0, 3);
    }

    public void ChangeSceneName()
    {
        isAuthInitialized = false;

        Scene sc = SceneManager.GetActiveScene();
        sceneName = sc.name;

        if (sceneName == "Authentication")
        {
            playOfflineButton.SetActive(true);
        }
        else
        {
            playOfflineButton.SetActive(false);
        }

        if (sceneName == "OfflineMenu")
        {
            CancelInvoke("CheckInternet");
            instance = null;
            Destroy(gameObject);
        }
    }
}
