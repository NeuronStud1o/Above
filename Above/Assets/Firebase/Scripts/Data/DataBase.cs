using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;

public class DataBase : MonoBehaviour
{
    public static DataBase instance;
    private DatabaseReference dbRef;

    public DatabaseReference Ref { get => dbRef; private set => dbRef = value; }

    void Start()
    {
        instance = this;
        dbRef = FirebaseDatabase.DefaultInstance.RootReference;
    }

    IEnumerator FetchData(params string[] keys)
    {
        // Формуємо шлях до даних в базі даних використовуючи передані ключі
        string path = string.Join("/", keys);

        // Отримуємо дані з бази даних за вказаним шляхом
        var fetchDataTask = dbRef.Child("user").Child(path).GetValueAsync();
        yield return new WaitUntil(() => fetchDataTask.IsCompleted);

        if (fetchDataTask.Exception != null)
        {
            Debug.LogError(fetchDataTask.Exception);
        }
        else if (fetchDataTask.Result.Exists)
        {
            // Отримали значення та записали його у змінну
            string fetchedValue = fetchDataTask.Result.Value.ToString();
            Debug.Log("Fetched Value: " + fetchedValue);

            // Тут ви можете зробити, що завгодно з отриманим значенням, наприклад, записати його у змінну
            // Наприклад, змінна вашого методу, яку ви передали
            string yourVariable = fetchedValue;
        }
        else
        {
            Debug.LogWarning("Data not found at path: " + path);
        }
    }
}
