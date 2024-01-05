using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Storage;
using System.IO;
using Firebase.Extensions;

public class StorageData : MonoBehaviour
{
    public static StorageData instance;

    FirebaseStorage storage;

    void Start()
    {
        instance = this;
        storage = FirebaseStorage.DefaultInstance;
    }

    public void SaveData(string fileName)
    {
        string storagePath = UserData.instance.User.DisplayName;
        
        var storageReference = storage.GetReference(storagePath);
        string filePath = Path.Combine(Application.persistentDataPath, fileName);

        storageReference.PutFileAsync(filePath)
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted || task.IsCanceled)
                {
                    // Обробка помилок під час завантаження файлу
                    Debug.LogError("Failed to upload file: " + task.Exception);
                }
                else
                {
                    // Файл успішно завантажено
                    Debug.Log("File uploaded successfully!");
                }
            });
    }
}
