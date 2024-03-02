using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Storage;
using System.IO;
using System.Threading.Tasks;
using System;
using Firebase.Extensions;
using UnityEngine.Networking;
using TMPro;

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
        string path = Path.Combine(Application.temporaryCachePath, "gameDataTemp.json");

        string jsonDataTemp = JsonUtility.ToJson(JsonStorage.instance.jsonData, true);
        File.WriteAllText(path, jsonDataTemp);

        if (File.Exists(path) && UserData.instance.User != null)
        {
            reference.PutFileAsync(path).ContinueWith(task => 
            {
                if (task.IsCompleted)
                {
                    Debug.Log("SAVE");
                }
                else if (task.IsFaulted)
                {
                    Debug.Log("FAIL");
                }
            });
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

        if (File.Exists(filePath))
        {
            try
            {
                File.Delete(filePath);

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

        string jsonData = File.ReadAllText(filePath);

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