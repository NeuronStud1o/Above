using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField]
    private GameObject loginPanel;

    [SerializeField]
    private GameObject registrationPanel;

    [Space]
    [SerializeField]
    private GameObject emailVerificationPanel;

    [SerializeField]
    private TextMeshProUGUI emailVerificationText;

    private void Awake()
    {
        CreateInstance();
    }

    private void CreateInstance()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    private void ClearUI()
    {
        loginPanel.SetActive(false);
        registrationPanel.SetActive(false);
        emailVerificationPanel.SetActive(false);
    }

    public void OpenLoginPanel()
    {
        ClearUI();
        loginPanel.SetActive(true);
    }

    public void OpenRegistrationPanel()
    {
        ClearUI();
        registrationPanel.SetActive(true);
    }

public void ShowVerificationResponse(bool isEmailSent, string emailId, string errorMessage)
    {
        ClearUI();
        emailVerificationPanel.SetActive(true);

        if(isEmailSent)
        {
            emailVerificationText.text = $"Please verify your email address \n Verification email has been sent to {emailId}";
        }
        else
        {
            emailVerificationText.text = $"Couldn't sent email : {errorMessage}";
        }
    }
}