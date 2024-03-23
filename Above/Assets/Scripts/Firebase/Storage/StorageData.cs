using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Storage;
using System.IO;
using System.Threading.Tasks;
using System;
using Firebase.Extensions;
using UnityEngine.Networking;

public class StorageData : MonoBehaviour
{
    public static StorageData instance;
    StorageReference reference;

    FirebaseStorage storage;

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
        reference = storage.RootReference.Child(UserData.instance.User.UserId).Child("gameData");
    }

    public void SaveJsonData()
    {
        string local_file = Path.Combine(Application.persistentDataPath, "gameDataTemp.json");
        string local_file_uri = string.Format("{0}://{1}", Uri.UriSchemeFile, local_file);

        string jsonDataTemp = JsonUtility.ToJson(JsonStorage.instance.jsonData, true);
        System.IO.File.WriteAllText(local_file, jsonDataTemp);
        
        if (System.IO.File.Exists(local_file) && UserData.instance.User != null)
        {
            try
            {
                reference.PutFileAsync(local_file_uri).ContinueWith(task => 
                {
                    if (task.IsCompleted)
                    {
                        Debug.Log("SAVE");
                    }
                    else if (task.IsFaulted)
                    {
                        Debug.Log("FAIL");
                    }

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
        else
        {
            Debug.Log("NULL");
        }
    }

    public void DeleteJson()
    {
        if (JsonStorage.instance != null)
        {
            JsonStorage.instance.CancelTimer();
        }

        string filePath = Path.Combine(Application.persistentDataPath, "gameData.json");
        string filePath2 = Path.Combine(Application.persistentDataPath, "gameDataTemp.json");

        if (System.IO.File.Exists(filePath))
        {
            try
            {
                System.IO.File.Delete(filePath);
                System.IO.File.Delete(filePath2);

                Debug.Log("File is deleted: " + filePath);
            }
            catch (Exception e)
            {
                Debug.Log("Delete file exeption: " + e.Message);
            }
        }
        else
        {
            Debug.Log("File is null: " + filePath);
        }

        if (System.IO.File.Exists(filePath2))
        {
            try
            {
                System.IO.File.Delete(filePath2);

                Debug.Log("File is deleted: " + filePath2);
            }
            catch (Exception e)
            {
                Debug.Log("Delete file exeption: " + e.Message);
            }
        }
        else
        {
            Debug.Log("File is null: " + filePath2);
        }
    }

    public async Task<T> LoadJsonData<T>()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "gameData.json");

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
                StartCoroutine(DownloadFile(downloadUrl));
            }
            else
            {
                Debug.LogError("Error getting download URL: " + task.Exception);
            }
        });

        await downloadTaskCompletionSource.Task;

        string jsonData = System.IO.File.ReadAllText(filePath);

        T loadedData = JsonUtility.FromJson<T>(jsonData);

        return loadedData;
    }

    private IEnumerator DownloadFile(string downloadUrl)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(downloadUrl))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                string filePath = Path.Combine(Application.persistentDataPath, "gameData.json");
                System.IO.File.WriteAllBytes(filePath, www.downloadHandler.data);

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
            StorageMetadata metadata = await reference.GetMetadataAsync();
            return true;
        }
        catch (StorageException ex)
        {
            if (ex.HttpResultCode == 404)
            {
                return false;
            }
            else
            {
                Debug.LogError("Error checking file existence: " + ex.Message);
                return false;
            }
        }
    }
}