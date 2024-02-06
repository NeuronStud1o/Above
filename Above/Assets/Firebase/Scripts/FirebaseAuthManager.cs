using System.Collections;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using TMPro;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using Firebase.Extensions;

public class FirebaseAuthManager : MonoBehaviour
{
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser user;
    public GameObject storage;

    [Space]
    [Header("Login")]
    public TMP_InputField emailLoginField;
    public TMP_InputField passwordLoginField;

    [Space]
    [Header("Registration")]
    public TMP_InputField nameRegisterField;
    public TMP_InputField emailRegisterField;
    public TMP_InputField passwordRegisterField;
    public TMP_InputField confirmPasswordRegisterField;

    [Space]
    [Header("Foget password")]
    [SerializeField] private TMP_InputField fogetPassField;
    [SerializeField] private GameObject sendResetPassText;

    private void Start()
    {
        StartCoroutine(CheckAndFixDependenciesAsync());
    }

    private IEnumerator CheckAndFixDependenciesAsync()
    {
        var dependencyTask = FirebaseApp.CheckAndFixDependenciesAsync();

        yield return new WaitUntil(() => dependencyTask.IsCompleted);

        dependencyStatus = dependencyTask.Result;

        if (dependencyStatus == DependencyStatus.Available)
        {
            InitializeFirebase();

            yield return new WaitForEndOfFrame();

            StartCoroutine(CheckForAutoLogin());
        }
        else
        {
            Debug.LogError("Could not resolve all firebase dependencies: " + dependencyStatus);
        }
    }

    void InitializeFirebase()
    {
        auth = FirebaseAuth.DefaultInstance;

        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }

    private IEnumerator CheckForAutoLogin()
    {
        if (user != null)
        {
            var reloadUserTask = user.ReloadAsync();

            yield return new WaitUntil(() => reloadUserTask.IsCompleted);

            AutoLogin();
        }
        else
        {
            UIManager.Instance.OpenLoginPanel();
        }
    }

    private async void AutoLogin()
    {
        if (user != null)
        {
            References.userName = user.DisplayName;
            UserData.instance.SetUser(user);

            await StorageData.instance.LoadJsonData();
            UIManager.Instance.OpenButtonsPanel();
        }
        else
        {
            UIManager.Instance.OpenLoginPanel();
        }
    }

    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;

            if (!signedIn && user != null)
            {
                Debug.Log("Signed out " + user.UserId);
                UIManager.Instance.OpenLoginPanel();
                ClearLoginInputFieldText();
            }

            user = auth.CurrentUser;

            if (signedIn)
            {
                Debug.Log("Signed in " + user.UserId);
            }
        }
    }

    public void ClearLoginInputFieldText()
    {
        emailLoginField.text = "";
        passwordLoginField.text = "";
    }

    public void ClearRegisterInputFieldText()
    {
        nameRegisterField.text = "";
        emailRegisterField.text = "";
        passwordRegisterField.text = "";
        confirmPasswordRegisterField.text = "";
    }

    public void Logout()
    {
        if (auth != null && user != null)
        {
            auth.SignOut();
            UIManager.Instance.OpenLoginPanel();
            StorageData.instance.DeleteJson();
        }
    }

    public void Login()
    {
        StartCoroutine(LoginAsync(emailLoginField.text, passwordLoginField.text));
    }

    private IEnumerator LoginAsync(string email, string password)
    {
        var loginTask = auth.SignInWithEmailAndPasswordAsync(email, password);

        yield return new WaitUntil(() => loginTask.IsCompleted);

        if (loginTask.Exception != null)
        {
            Debug.LogError(loginTask.Exception);

            FirebaseException firebaseException = loginTask.Exception.GetBaseException() as FirebaseException;
            AuthError authError = (AuthError)firebaseException.ErrorCode;

            string failedMessage = "Login Failed! Because ";

            switch (authError)
            {
                case AuthError.InvalidEmail:
                    failedMessage += "Email is invalid";
                    break;
                case AuthError.WrongPassword:
                    failedMessage += "Wrong Password";
                    break;
                case AuthError.MissingEmail:
                    failedMessage += "Email is missing";
                    break;
                case AuthError.MissingPassword:
                    failedMessage += "Password is missing";
                    break;
                default:
                    failedMessage = "Login Failed";
                    break;
            }

            UIManager.Instance.SetErrorMessage(failedMessage);
        }
        else
        {
            user = loginTask.Result.User;

            Debug.LogFormat("{0} You Are Successfully Logged In", user.DisplayName);

            if (user.IsEmailVerified)
            {
                References.userName = user.DisplayName;
                UserData.instance.SetUser(user);
                OpenGameScene();
            }
            else
            {
                SendEmailForVerification();
            }
        }
    }

    public void Register()
    {
        StartCoroutine(RegisterAsync(nameRegisterField.text, emailRegisterField.text, passwordRegisterField.text, confirmPasswordRegisterField.text));
    }

    private IEnumerator RegisterAsync(string name, string email, string password, string confirmPassword)
    {
        if (name == "")
        {
            UIManager.Instance.SetErrorMessage("User Name is empty");
        }
        else if (email == "")
        {
            UIManager.Instance.SetErrorMessage("email field is empty");
        }
        else if (passwordRegisterField.text != confirmPasswordRegisterField.text)
        {
            UIManager.Instance.SetErrorMessage("Password does not match");
        }
        else
        {
            var registerTask = auth.CreateUserWithEmailAndPasswordAsync(email, password);

            yield return new WaitUntil(() => registerTask.IsCompleted);

            if (registerTask.Exception != null)
            {
                Debug.LogError(registerTask.Exception);

                FirebaseException firebaseException = registerTask.Exception.GetBaseException() as FirebaseException;
                AuthError authError = (AuthError)firebaseException.ErrorCode;

                string failedMessage = "Registration Failed! Becuase ";
                switch (authError)
                {
                    case AuthError.InvalidEmail:
                        failedMessage += "Email is invalid";
                        break;
                    case AuthError.WrongPassword:
                        failedMessage += "Wrong Password";
                        break;
                    case AuthError.MissingEmail:
                        failedMessage += "Email is missing";
                        break;
                    case AuthError.MissingPassword:
                        failedMessage += "Password is missing";
                        break;
                    default:
                        failedMessage = "Registration Failed";
                        break;
                }

                UIManager.Instance.SetErrorMessage(failedMessage);
            }
            else
            {
                user = registerTask.Result.User;

                UserProfile userProfile = new UserProfile { DisplayName = name };

                var updateProfileTask = user.UpdateUserProfileAsync(userProfile);

                yield return new WaitUntil(() => updateProfileTask.IsCompleted);

                if (updateProfileTask.Exception != null)
                {
                    user.DeleteAsync();

                    Debug.LogError(updateProfileTask.Exception);

                    FirebaseException firebaseException = updateProfileTask.Exception.GetBaseException() as FirebaseException;
                    AuthError authError = (AuthError)firebaseException.ErrorCode;


                    string failedMessage = "Profile update Failed! Becuase ";
                    switch (authError)
                    {
                        case AuthError.InvalidEmail:
                            failedMessage += "Email is invalid";
                            break;
                        case AuthError.WrongPassword:
                            failedMessage += "Wrong Password";
                            break;
                        case AuthError.MissingEmail:
                            failedMessage += "Email is missing";
                            break;
                        case AuthError.MissingPassword:
                            failedMessage += "Password is missing";
                            break;
                        default:
                            failedMessage = "Profile update Failed";
                            break;
                    }

                    UIManager.Instance.SetErrorMessage(failedMessage);
                }
                else
                {
                    Debug.Log("Registration Sucessful Welcome " + user.DisplayName);

                    if (user.IsEmailVerified)
                    {
                        UIManager.Instance.OpenLoginPanel();
                        DataBase.instance.SaveData(UserData.instance.User.DisplayName, "userSettings", "name");
                        DataBase.instance.SaveData(UserData.instance.User.Email, "userSettings", "email");
                        DataBase.instance.SaveData("blackThrush", "userSettings", "icon");
                    }
                    else
                    {
                        SendEmailForVerification();
                    }
                }
            }
        }
    }

    public void SendEmailForVerification()
    {
        StartCoroutine(SendEmailForVerificationAsync());
    }

    private IEnumerator SendEmailForVerificationAsync()
    {
        if (user != null)
        {
            var sendEmailTask = user.SendEmailVerificationAsync();

            yield return new WaitUntil(() => sendEmailTask.IsCompleted);

            if (sendEmailTask.Exception != null)
            {
                FirebaseException firebaseException = sendEmailTask.Exception.GetBaseException() as FirebaseException;
                AuthError error = (AuthError) firebaseException.ErrorCode;

                string errorMessage = "Unknown Error : Please try again later";

                switch (error)
                {
                    case AuthError.Cancelled:
                        errorMessage = "Email verification was cancelled";
                        break;
                    case AuthError.TooManyRequests:
                        errorMessage = "Too many request";
                        break;
                    case AuthError.InvalidRecipientEmail:
                        errorMessage = "The email you entered is invalid";
                        break;
                }

                UIManager.Instance.ShowVerificationResponse(false, user.Email, errorMessage);
            }
            else
            {
                Debug.Log("Email has successfully sent");
                UIManager.Instance.ShowVerificationResponse(true, user.Email, null);
            }
        }
    }

    public async void OpenGameScene()
    {
        await StartGame();
    }

    async Task StartGame()
    {
        DataBase.instance.SetActiveLoadingScreen(true);

        storage.SetActive(true);

        await JsonStorage.instance.StartAction();

        await Task.Delay(1000);

        bool tutorial = JsonStorage.instance.jsonData.boolean.isTutorial;

        if (tutorial == false)
        {
            JsonStorage.instance.jsonData.boolean.isTutorial = true;

            DataBase.instance.SetActiveLoadingScreen(false);

            await Task.Delay(500);

            DataBase.instance.gameObject.GetComponent<AudioSource>().enabled = false;

            SceneManager.LoadSceneAsync("Tutorial");
        }
        else
        {
            SceneManager.LoadSceneAsync("MainMenu");
        }
    }

    public void PlayOffline()
    {
        SceneManager.LoadSceneAsync("OfflineMenu");
    }

    public void FogetPassword()
    {
        UIManager.Instance.ClearUI();
        
        if (string.IsNullOrEmpty(fogetPassField.text))
        {
            UIManager.Instance.SetErrorMessage("The email field cannot be empty");
            return;
        }

        FogetPassSubmit(fogetPassField.text);
    }

    void FogetPassSubmit(string email)
    {
        auth.SendPasswordResetEmailAsync(email).ContinueWithOnMainThread(task => {
            if (task.Exception != null)
            {
                FirebaseException firebaseException = task.Exception.GetBaseException() as FirebaseException;
                AuthError error = (AuthError)firebaseException.ErrorCode;

                string failedMessage = "";

                switch (error)
                {
                    case AuthError.InvalidEmail:
                        failedMessage += "Email is invalid";
                        break;
                    case AuthError.MissingEmail:
                        failedMessage += "Email is missing";
                        break;
                    default:
                        failedMessage = "Login Failed";
                        break;
                }

                UIManager.Instance.SetErrorMessage("Error: " + failedMessage);
            }
            else
            {
                sendResetPassText.SetActive(true);
            }
        });
    }
}