using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Storage;
using System.IO;
using Firebase.Extensions;
using System.Threading.Tasks;

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
