using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("## Authentication :")]
    [SerializeField] private GameObject authPanel;
    [SerializeField] private GameObject loginPanel;
    [SerializeField] private GameObject registrationPanel;
    [SerializeField] private TextMeshProUGUI errorText;

    [Space]
    [SerializeField] private GameObject emailVerificationPanel;
    [SerializeField] private TextMeshProUGUI emailVerificationText;
    [SerializeField] private TextManagerTMP verificationTextManager;

    [SerializeField] private Button playOnlineButton;
    [SerializeField] private Button logoutButton;

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
        errorText.text = "";
        verificationTextManager.ChooseLanguage();
    }

    public void OpenLoginPanel()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "data.json");
        
        StorageData.instance.DeletingFile(filePath);

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

    public void CloseAuthPanel()
    {
        ClearUI();
        authPanel.SetActive(false);
    }

    public void ActivateButtons()
    {
        ClearUI();

        if (FirebaseAuthManager.instance != null)
        {
            if (FirebaseAuthManager.instance.user != null && FirebaseAuthManager.instance.user.IsEmailVerified)
            {
                logoutButton.interactable = true;
            }
        }

        playOnlineButton.interactable = true;
    }

    public void ShowVerificationResponse(bool isEmailSent, string emailId, string errorMessage)
    {
        ClearUI();
        emailVerificationPanel.SetActive(true);

        if(isEmailSent)
        {
            emailVerificationText.text += emailId;
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