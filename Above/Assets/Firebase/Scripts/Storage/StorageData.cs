using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Storage;
using System.IO;
using System.Threading.Tasks;
using System;

public class StorageData : MonoBehaviour
{
    public static StorageData instance;
    StorageReference reference;

    FirebaseStorage storage;

    void Awake()
    {
        instance = this;
        storage = FirebaseStorage.DefaultInstance;
    }

    public void SaveJsonData()
    {
        reference = storage.RootReference.Child(UserData.instance.User.DisplayName).Child("gameData");

        string localFile = Path.Combine(Application.persistentDataPath, "gameData.json");

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
        string filePath = Path.Combine(Application.persistentDataPath, "gameData.json");

        if (File.Exists(filePath))
        {
            try
            {
                File.Delete(filePath);
                Console.WriteLine("File is deleted: " + filePath);
            }
            catch (Exception e)
            {
                Console.WriteLine("Delete file exeption: " + e.Message);
            }
        }
        else
        {
            Console.WriteLine("File is null: " + filePath);
        }
    }

    public async Task LoadJsonData()
    {
        reference = storage.RootReference.Child(UserData.instance.User.DisplayName).Child("gameData");

        string localFile = Path.Combine(Application.persistentDataPath, "gameData.json");

        await reference.GetFileAsync(localFile).ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Sucefull download");
            }
        });
    }

    public async Task<bool> CheckIfJsonDataExists()
    {
        reference = storage.RootReference.Child(UserData.instance.User.DisplayName).Child("gameData");

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
