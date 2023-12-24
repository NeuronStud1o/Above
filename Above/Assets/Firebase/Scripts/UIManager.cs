using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private GameObject authPanel;
    [SerializeField] private GameObject buttonsPanel;
    [SerializeField] private GameObject loginPanel;
    [SerializeField] private GameObject registrationPanel;
    [SerializeField] private TextMeshProUGUI errorText;

    [Space]
    [SerializeField] private GameObject emailVerificationPanel;
    [SerializeField] private TextMeshProUGUI emailVerificationText;
    
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

    public void SetErrorMessage(string error)
    {
        errorText.text = error;
    }

    private void ClearUI()
    {
        loginPanel.SetActive(false);
        registrationPanel.SetActive(false);
        emailVerificationPanel.SetActive(false);
        buttonsPanel.SetActive(false);
        errorText.text = "";
    }

    public void OpenLoginPanel()
    {
        ClearUI();
        authPanel.SetActive(true);
        loginPanel.SetActive(true);
    }

    public void OpenRegistrationPanel()
    {
        ClearUI();
        authPanel.SetActive(true);
        registrationPanel.SetActive(true);
    }

    public void OpenButtonsPanel()
    {
        ClearUI();
        buttonsPanel.SetActive(true);
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