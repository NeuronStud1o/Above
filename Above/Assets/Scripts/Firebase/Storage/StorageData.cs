using System.Collections;
using UnityEngine;
using Firebase.Storage;
using System.IO;
using System.Threading.Tasks;
using System;
using Firebase.Extensions;
using UnityEngine.Networking;
using Firebase.Auth;

public class StorageData : MonoBehaviour
{
    public static StorageData instance;

    StorageReference reference;
    FirebaseStorage storage;
    StorageMetadata metadata;

    public FirebaseAuth auth;

    private TaskCompletionSource<bool> downloadTaskCompletionSource;

    void Start()
    {
        if (instance != null)
        {
            return;
        }
        
        instance = this;
    }

    public void SetStorage(FirebaseStorage stor)
    {
        storage = stor;
    }

    public void SetReference()
    {
        reference = storage.RootReference.Child(UserData.instance.User.UserId).Child("data");
    }

    public async Task DeleteUser()
    {
        await DeleteFolderAsync();
    }

    public async Task DeleteFolderAsync()
    {
        DataBase.instance.SetActiveLoadingScreen(true);

        StorageReference storageRef = storage.GetReferenceFromUrl("gs://above-acb46.appspot.com");
        StorageReference folderRef = storageRef.Child(UserData.instance.User.UserId);

        await folderRef.DeleteAsync().ContinueWith(async task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Folder deleted successfully.");
                await DeleteUserAsync();
            }
            else if (task.IsFaulted)
            {
                Debug.LogError("Failed to delete folder: " + task.Exception);
                DataBase.instance.SetActiveLoadingScreen(false);
            }
        });
    }

    public void DeleteAdditionalJson()
    {
        DataBase.instance.SetActiveLoadingScreen(true);

        StorageReference storageRef = storage.GetReferenceFromUrl("gs://above-acb46.appspot.com");
        StorageReference folderRef = storageRef.Child(UserData.instance.User.UserId).Child("gameData");

        folderRef.DeleteAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Json is deleted");
            }
            else if (task.IsFaulted)
            {
                Debug.LogError("Failed to delete folder: " + task.Exception);
                DataBase.instance.SetActiveLoadingScreen(false);
            }
        });
    }

    async Task DeleteUserAsync()
    {
        FirebaseUser user = auth.CurrentUser;
        if (user != null)
        {
            await user.DeleteAsync().ContinueWith(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.LogError("DeleteAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("DeleteAsync encountered an error: " + task.Exception);
                    return;
                }

                Debug.Log("User deleted successfully.");
            });
        }
    }

    public void SaveJsonData()
    {
        string local_file = Path.Combine(Application.persistentDataPath, "gameDataTemp.json");
        string local_file_uri = string.Format("{0}://{1}", Uri.UriSchemeFile, local_file);

        string jsonDataTemp = JsonUtility.ToJson(JsonStorage.instance.data, true);
        System.IO.File.WriteAllText(local_file, jsonDataTemp);
        
        if (System.IO.File.Exists(local_file) && UserData.instance.User != null)
        {
            try
            {
                reference.PutFileAsync(local_file_uri).ContinueWith(task => 
                {
                    StorageMetadata metadata = task.Result;
                    string md5Hash = metadata.Md5Hash;
                });
            }
            catch (StorageException e)
            {
                Debug.Log(e.HttpResultCode);

                Debug.Log(e);

                DataBase.instance.SetActiveLoadingScreen(true);
                DataBase.instance.SetMessage(e.HttpResultCode.ToString());

                //ErrorUnknown	Сталася невідома помилка.
                //ErrorObjectNotFound	Жодного об'єкта не існує в потрібному посиланні.
                //ErrorBucketNotFound	Жодне відро не налаштовано для Cloud Storage.
                //ErrorProjectNotFound	Жоден проект не налаштовано для Cloud Storage.
                //ErrorQuotaExceeded	Перевищено квоту вашого сегмента Cloud Storage. Якщо ви використовуєте безкоштовний план, перейдіть на платний план. Якщо у вас платний план, зверніться до служби підтримки Firebase.
                //ErrorNotAuthenticated	Користувач не автентифікований. Пройдіть автентифікацію та повторіть спробу.
                //ErrorNotAuthorized	Користувач не авторизований для виконання бажаної дії. Перевірте свої правила, щоб переконатися, що вони правильні.
                //ErrorRetryLimitExceeded	Перевищено максимальний ліміт часу для операції (завантаження, завантаження, видалення тощо). Спробуйте завантажити знову.
                //ErrorInvalidChecksum	Файл на клієнті не відповідає контрольній сумі файлу, отриманого сервером. Спробуйте завантажити знову.
                //ErrorCanceled	Користувач скасував операцію.
            }
        }
    }

    public void DeleteJson()
    {
        if (JsonStorage.instance != null)
        {
            JsonStorage.instance.CancelTimer();
        }

        string filePath = Path.Combine(Application.persistentDataPath, "data.json");
        string filePath2 = Path.Combine(Application.persistentDataPath, "gameData.json");
        string filePath3 = Path.Combine(Application.persistentDataPath, "gameDataTemp.json");

        DeletingFile(filePath);
        DeletingFile(filePath2);
        DeletingFile(filePath3);
    }

    void DeletingFile(string path)
    {
        if (File.Exists(path))
        {
            try
            {
                File.Delete(path);

                Debug.Log("File is deleted: " + path);
            }
            catch (Exception e)
            {
                Debug.Log("Delete file exeption: " + e.Message);
            }
        }
    }

    public async Task<T> LoadJsonData<T>()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "data.json");

        if (!await CheckIfJsonDataExists())
        {
            Debug.Log("File does not exist in storage.");
            return default;
        }

        downloadTaskCompletionSource = new TaskCompletionSource<bool>();

        var downloadUrlTask = reference.GetDownloadUrlAsync();
        await downloadUrlTask.ContinueWithOnMainThread(task =>
        {
            if (!task.IsFaulted && !task.IsCanceled)
            {
                string downloadUrl = task.Result.ToString();
                StartCoroutine(DownloadFile(downloadUrl, filePath));
            }
            else
            {
                Debug.LogError("Error getting download URL: " + task.Exception);
            }
        });

        await downloadTaskCompletionSource.Task;

        string jsonData = File.ReadAllText(filePath);

        T loadedData = JsonUtility.FromJson<T>(jsonData);

        return loadedData;
    }

    public async Task<T> LoadAdditionalJsonData<T>()
    {
        StorageReference reference2 = storage.RootReference.Child(UserData.instance.User.UserId).Child("gameData");
        string filePath = Path.Combine(Application.persistentDataPath, "gameData.json");

        if (!await CheckAdditionalJson())
        {
            Debug.Log("File does not exist in storage.");
            return default;
        }

        downloadTaskCompletionSource = new TaskCompletionSource<bool>();

        var downloadUrlTask = reference2.GetDownloadUrlAsync();
        await downloadUrlTask.ContinueWithOnMainThread(task =>
        {
            if (!task.IsFaulted && !task.IsCanceled)
            {
                string downloadUrl = task.Result.ToString();
                StartCoroutine(DownloadFile(downloadUrl, filePath));
            }
            else
            {
                Debug.LogError("Error getting download URL: " + task.Exception);
            }
        });

        await downloadTaskCompletionSource.Task;

        string jsonData = File.ReadAllText(filePath);

        T loadedData = JsonUtility.FromJson<T>(jsonData);

        return loadedData;
    }

    private IEnumerator DownloadFile(string downloadUrl, string filePath)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(downloadUrl))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                File.WriteAllBytes(filePath, www.downloadHandler.data);

                downloadTaskCompletionSource.TrySetResult(true);
            }
            else
            {
                Debug.LogError("Failed to download file: " + www.error);
            }
        }
    }

    public async Task<bool> CheckIfJsonDataExists()
    {
        try
        {
            metadata = await reference.GetMetadataAsync();
            return true;
        }
        catch (StorageException)
        {
            return false;
        }
    }

    public async Task<bool> CheckAdditionalJson()
    {
        Debug.Log("Checking game data");
        StorageReference additionalJsonRef = storage.RootReference.Child(UserData.instance.User.UserId).Child("gameData");

        try
        {
            metadata = await additionalJsonRef.GetMetadataAsync();

            return true;
        }
        catch (StorageException)
        {
            return false;
        }
    }
}