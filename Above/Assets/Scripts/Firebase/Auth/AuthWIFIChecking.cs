using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class AuthWIFIChecking : MonoBehaviour
{
    [SerializeField] private GameObject errorPanel;
    [SerializeField] private FirebaseAuthManager firebaseAuthManager;

    void Start()
    {
        StartCoroutine(CheckInternetConnection());
    }

    private IEnumerator CheckInternetConnection()
    {
        errorPanel.SetActive(false);

        UnityWebRequest request = new UnityWebRequest("https://www.google.com/");

        var asyncOperation = request.SendWebRequest();

        yield return asyncOperation;

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            errorPanel.SetActive(true);
        }
        else
        {
            firebaseAuthManager.StartAction();
        }
    }
    
    public void TryAgain()
    {
        StartCoroutine(CheckInternetConnection());
        Debug.Log("Try again");
    }
}
