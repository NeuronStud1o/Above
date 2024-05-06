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
using System.IO;

public class FirebaseAuthManager : MonoBehaviour
{
    public static FirebaseAuthManager instance;
    private FirebaseStorage storageInstance;

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
            initText.text = "Exception: " + exeption;
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

        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);

        StorageData.instance.auth = auth;
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

    private void AutoLogin()
    {
        if (user != null && user.IsEmailVerified)
        {
            if (storage != null)
            {
                storage.SetActive(true);
            }

            References.userName = user.DisplayName;
            UserData.instance.User = user;

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

                if (UIManager.Instance != null)
                {
                    UIManager.Instance.OpenLoginPanel();
                }
                
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

    public async void Logout()
    {
        await ExitGame();
    }

    async Task ExitGame()
    {
        if (auth != null && user != null)
        {
            string filePath = Path.Combine(Application.persistentDataPath, "data.json");
            string password = CryptoHelper.GenerateKeyFromUid(UserData.instance.User.UserId);

            StorageData.instance.SetStorage(storageInstance);
            StorageData.instance.SetReference();

            if (JsonStorage.instance.data.userData.level == 0)
            {
                if (!File.Exists(filePath))
                {
                    try
                    {
                        if (UserData.instance.User != null)
                        {
                            if (await StorageData.instance.CheckIfJsonDataExists() == true)
                            {
                                UnityEngine.Debug.Log("File is received from storage");

                                JsonStorage.instance.data = await StorageData.instance.LoadJsonData<Data>();

                                while (!File.Exists(filePath))
                                {
                                    await Task.Delay(100);
                                }

                                await Task.Delay(1000);

                                CryptoHelper.Encrypt(JsonStorage.instance.data, password);

                                return;
                            }
                            else
                            {
                                auth.SignOut();
                                Application.Quit();
                            }
                        }   
                    }
                    catch (System.Exception e)
                    {
                        UnityEngine.Debug.LogError(e);
                    }
                    
                }
                else
                {
                    JsonStorage.instance.data = CryptoHelper.LoadAndDecrypt<Data>(filePath, password);
                }
            }
            
            StorageData.instance.SaveJsonData();
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
        var loginTask = auth.SignInWithEmailAndPasswordAsync(email, password);

        yield return new WaitUntil(() => loginTask.IsCompleted);

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

            if (user.IsEmailVerified)
            {
                DataBase.instance.SetActiveLoadingScreen(true);

                DataBase.instance.SetMessage("User login");

                yield return new WaitForSeconds(2);

                References.userName = user.DisplayName;
                UserData.instance.User = user;

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
        DataBase.instance.SetActiveLoadingScreen(true);
        StartCoroutine(RegisterAsync(nameRegisterField.text, emailRegisterField.text, passwordRegisterField.text, confirmPasswordRegisterField.text));
    }

    private IEnumerator RegisterAsync(string name, string email, string password, string confirmPassword)
    {
        yield return new WaitForSeconds(0.5f);

        if (name == "")
        {
            UIManager.Instance.SetErrorMessage("User Name is empty");
            DataBase.instance.SetActiveLoadingScreen(false);
        }
        else if (email == "")
        {
            UIManager.Instance.SetErrorMessage("email field is empty");
            DataBase.instance.SetActiveLoadingScreen(false);
        }
        else if (!string.Equals(password, confirmPassword, StringComparison.Ordinal))
        {
            UIManager.Instance.SetErrorMessage("Password does not match");
            DataBase.instance.SetActiveLoadingScreen(false);
            yield break;
        }
        else
        {
            var registerTask = auth.CreateUserWithEmailAndPasswordAsync(email, password);

            yield return new WaitUntil(() => registerTask.IsCompleted);

            if (registerTask.Exception != null)
            {
                DataBase.instance.SetActiveLoadingScreen(false);
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

                    DataBase.instance.SetActiveLoadingScreen(false);
                }
                else
                {
                    Debug.Log("Registration Sucessful Welcome " + user.DisplayName);

                    if (user.IsEmailVerified)
                    {
                        UIManager.Instance.OpenLoginPanel();

                        DataBase.instance.SetActiveLoadingScreen(false);
                    }
                    else
                    {
                        SendEmailForVerification();

                        DataBase.instance.SetActiveLoadingScreen(false);
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
        
        DataBase.instance.SetMessage("Start game");

        if (storage != null)
        {
            storage.SetActive(true);
        }

        StorageData.instance.SetStorage(storageInstance);
        StorageData.instance.SetReference();
        
        await JsonStorage.instance.StartAction();

        DataBase.instance.SetMessage("Start action");

        await Task.Delay(1000);
        
        DataBase.instance.GetComponent<AudioSource>().volume = JsonStorage.instance.data.audioSettings.musicMainMenu;

        bool tutorial = JsonStorage.instance.data.boolean.isTutorial;

        DataBase.instance.SetMessage("");

        instance = null;
        UIManager.Instance = null;

        if (tutorial == false)
        {
            DataBase.instance.GetComponent<AudioSource>().enabled = false;
            JsonStorage.instance.data.boolean.isTutorial = true;

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