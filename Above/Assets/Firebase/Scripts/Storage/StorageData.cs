using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Storage;
using System.IO;
using System.Threading.Tasks;
using System;
using Firebase.Extensions;
using UnityEngine.Networking;
using UnityEditor;

public class StorageData : MonoBehaviour
{
    public static StorageData instance;
    StorageReference reference;

    FirebaseStorage storage;

    void Awake()
    {
        if (instance != null)
        {
            return;
        }
        
        instance = this;
        storage = FirebaseStorage.DefaultInstance;
    }

    public void SaveJsonData()
    {
        Debug.Log("Try to save");
        string localFile = Path.Combine(Application.persistentDataPath, "gameData.json");

        if (!File.Exists(localFile) || UserData.instance.User == null) return;

        reference = storage.RootReference.Child(UserData.instance.User.UserId).Child("gameData");
        Debug.Log(UserData.instance.User.UserId);
        reference.PutFileAsync(localFile).ContinueWith(task => 
        {
            if (task.IsCompleted)
            {
                Debug.Log("File is saved");
            }
        });
    }

    public void DeleteJson()
    {
        if (JsonStorage.instance != null)
        {
            JsonStorage.instance.CancelTimer();
        }

        SaveJsonData();

        string filePath = Path.Combine(Application.persistentDataPath, "gameData.json");

        if (File.Exists(filePath))
        {
            try
            {
                FileUtil.DeleteFileOrDirectory(filePath);
                AssetDatabase.Refresh();

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

    public async Task LoadJsonData()
    {
        reference = storage.RootReference.Child(UserData.instance.User.UserId).Child("gameData.json");

        if (!await CheckIfJsonDataExists())
        {
            Debug.Log("File does not exist in storage.");
            return;
        }

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

                Debug.Log("File downloaded successfully!");
            }
            else
            {
                Debug.LogError("Failed to download file: " + www.error);
            }
        }
    }

    public async Task<bool> CheckIfJsonDataExists()
    {
        reference = storage.RootReference.Child(UserData.instance.User.UserId).Child("gameData");
        Debug.Log(UserData.instance.User.UserId);

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