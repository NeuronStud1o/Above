using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("## Authentication :")]
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

    public void OpenGmail()
    {
        Application.OpenURL("https://mail.google.com");
    }

    public void SetErrorMessage(string error)
    {
        errorText.text = error;
    }

    public void ClearUI()
    {
        loginPanel.SetActive(false);
        registrationPanel.SetActive(false);
        emailVerificationPanel.SetActive(false);
        buttonsPanel.SetActive(false);
        errorText.text = "";
    }

    public void OpenLoginPanel()
    {
        StorageData.instance.DeleteJson();

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

    public void OpenDocumentation()
    {
        Application.OpenURL("https://docs.google.com/document/d/1ANJd7XmfpLubOtsssmON4Fuc_4nhoc4LQUoeockF8RY/edit?usp=sharing");
    } 
}