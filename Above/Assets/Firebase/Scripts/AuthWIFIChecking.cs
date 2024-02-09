using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AuthWIFIChecking : MonoBehaviour
{
    [SerializeField] private GameObject errorPanel;
    [SerializeField] private FirebaseAuthManager firebaseAuthManager;

    void Start()
    {
        StartCoroutine(CheckInternetConection());
    }

    IEnumerator CheckInternetConection()
    {
        errorPanel.SetActive(false);

        UnityWebRequest request = new UnityWebRequest("https://www.google.com/");

        yield return request.SendWebRequest();

        if (request.error != null)
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
        StartCoroutine(CheckInternetConection());
        Debug.Log("Try again");
    }
}
