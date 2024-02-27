using System.Collections;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using TMPro;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using Firebase.Extensions;
using System;
using Firebase.Storage;

public class FirebaseAuthManager : MonoBehaviour
{
    public static FirebaseAuthManager instance;
    public static FirebaseStorage storageInstance;

    [Header("Firebase")]
    public FirebaseAuth auth;
    public FirebaseUser user;
    public FirebaseApp app;
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

    [Space]
    [Header("Other")]
    [SerializeField] private GameObject loading;
    [SerializeField] private TextMeshProUGUI initText;

    private static bool isReady = false;
    public static string exeption = "";

    async Task Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        await CheckIfReady();
    }

    public async void StartAction()
    {
        initText.text = "Check and fix dependencies";

        if (exeption != "")
        {
            initText.text = exeption;
        }

        int attemps = 1;

        while (isReady == false)
        {
            initText.text = "";
            initText.text += exeption + attemps;

            await Task.Delay(1000);

            await CheckIfReady();

            attemps++;
        }

        InitializeFirebase();
        StartCoroutine(CheckForAutoLogin());
    }

    async Task CheckIfReady()
    {
        try
        {
            await FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
            {
                initText.text = "Checking the readiness of dependencies";

                var dependencyStatus = task.Result;

                if (dependencyStatus == DependencyStatus.Available)
                {
                    if (FirebaseApp.DefaultInstance == null)
                    {
                        Firebase.AppOptions options = new Firebase.AppOptions();

                        options.ApiKey = "AIzaSyBBKdqnwmjChKTZrcJf9yluEj_vIuIOJLo";
                        options.AppId = "1:222329630877:android:91e403f4a340caea3f2474";
                        options.ProjectId = "above-acb46";
                        options.StorageBucket = "above-acb46.appspot.com";
                        options.DatabaseUrl = new System.Uri("https://above-acb46-default-rtdb.europe-west1.firebasedatabase.app/");

                        app = FirebaseApp.Create(options);
                    }

                    storageInstance = FirebaseStorage.GetInstance(app, "gs://above-acb46.appspot.com");

                    isReady = true;
                }
                else
                {
                    Debug.LogError(System.String.Format(
                        "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                }
            });
        }
        catch (System.Exception ex)
        {
            exeption = ex.Message.ToString();
        }
    }

    void InitializeFirebase()
    {
        initText.text = "Initialize data base";

        auth = FirebaseAuth.DefaultInstance;

        DataBase.instance.SetUserMessage("Authefication: " + auth);

        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }

    private IEnumerator CheckForAutoLogin()
    {
        initText.text = "Checking the automatic login";

        if (user != null)
        {
            var reloadUserTask = user.ReloadAsync();

            yield return new WaitUntil(() => reloadUserTask.IsCompleted);

            AutoLogin();
        }
        else
        {
            initText.gameObject.SetActive(false);
            loading.SetActive(false);

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

        initText.gameObject.SetActive(false);
        loading.SetActive(false);
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
            Application.Quit();

            Debug.Log("Exit");
        }
    }

    public void Login()
    {
        StartCoroutine(LoginAsync(emailLoginField.text, passwordLoginField.text));
    }

    private IEnumerator LoginAsync(string email, string password)
    {
        DataBase.instance.SetMessage("Asynchronous login via email and password");

        var loginTask = auth.SignInWithEmailAndPasswordAsync(email, password);

        yield return new WaitUntil(() => loginTask.IsCompleted);

        DataBase.instance.SetMessage("Error handling");

        if (loginTask.Exception != null)
        {
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

            DataBase.instance.SetUserMessage("User: " + user);

            if (user.IsEmailVerified)
            {
                DataBase.instance.SetMessage("User login");

                yield return new WaitForSeconds(2);

                References.userName = user.DisplayName;
                UserData.instance.SetUser(user);

                DataBase.instance.SetUserMessage("Data user: " + UserData.instance.User);
                DataBase.instance.SetUserMessage("User name: " + UserData.instance.User.DisplayName);

                DataBase.instance.SetMessage("Opening game scene");

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
        else if (!string.Equals(password, confirmPassword, StringComparison.Ordinal))
        {
            UIManager.Instance.SetErrorMessage("Password does not match");
            yield break;
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
        DataBase.instance.SetMessage("Start game");
        
        DataBase.instance.SetActiveLoadingScreen(true);

        storage.SetActive(true);

        await JsonStorage.instance.StartAction();

        DataBase.instance.SetMessage("Start action");

        await Task.Delay(1000);
        
        DataBase.instance.GetComponent<AudioSource>().volume = JsonStorage.instance.jsonData.audioSettings.musicMainMenu;

        bool tutorial = JsonStorage.instance.jsonData.boolean.isTutorial;

        DataBase.instance.SetMessage("");

        if (tutorial == false)
        {
            DataBase.instance.GetComponent<AudioSource>().enabled = false;
            JsonStorage.instance.jsonData.boolean.isTutorial = true;

            DataBase.instance.SetActiveLoadingScreen(false);

            await Task.Delay(500);

            SceneManager.LoadSceneAsync("Tutorial");
        }
        else
        {
            SceneManager.LoadSceneAsync("MainMenu");
        }
    }

    public void PlayOffline()
    {
        DataBase.instance.GetComponent<AudioSource>().volume = 0.7f;
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