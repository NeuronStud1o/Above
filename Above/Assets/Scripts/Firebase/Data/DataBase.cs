using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using System.Threading.Tasks;
using TMPro;
using Firebase;

public class DataBase : MonoBehaviour
{
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(loadingScreen);
    }

    public static DataBase instance;
    private DatabaseReference dbRef;

    public DatabaseReference Ref { get => dbRef; private set => dbRef = value; }

    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private TextMeshProUGUI info;

    public void SetMessage(string text)
    {
        info.text = text;
    }
    
    void Start()
    {
        instance = this;
        dbRef = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void SetActiveLoadingScreen(bool isActive)
    {
        loadingScreen.SetActive(isActive);
    }

    public void SaveData(int i, params string[] keys)
    {
        string path = string.Join("/", keys);
        dbRef.Child("user").Child(UserData.instance.User.UserId).Child(path).SetValueAsync(i);
    }

    public void SaveData(float i, params string[] keys)
    {
        string path = string.Join("/", keys);
        dbRef.Child("user").Child(UserData.instance.User.UserId).Child(path).SetValueAsync(i);
    }

    public void SaveData(string i, params string[] keys)
    {
        string path = string.Join("/", keys);
        dbRef.Child("user").Child(UserData.instance.User.UserId).Child(path).SetValueAsync(i);
    }

    public void SaveData(bool i, params string[] keys)
    {
        string path = string.Join("/", keys);
        dbRef.Child("user").Child(UserData.instance.User.UserId).Child(path).SetValueAsync(i);
    }

    public async Task SaveDataAsync(int i, params string[] keys)
    {
        string path = string.Join("/", keys);
        await dbRef.Child("user").Child(UserData.instance.User.UserId).Child(path).SetValueAsync(i);
    }

    public async Task SaveDataAsync(float i, params string[] keys)
    {
        string path = string.Join("/", keys);
        await dbRef.Child("user").Child(UserData.instance.User.UserId).Child(path).SetValueAsync(i);
    }


    public async Task SaveDataAsync(string i, params string[] keys)
    {
        string path = string.Join("/", keys);
        await dbRef.Child("user").Child(UserData.instance.User.UserId).Child(path).SetValueAsync(i);
    }

    public async Task SaveDataAsync(bool i, params string[] keys)
    {
        string path = string.Join("/", keys);
        await dbRef.Child("user").Child(UserData.instance.User.UserId).Child(path).SetValueAsync(i);
    }

    public async Task<int> LoadDataInt(params string[] keys)
    {
        int intValue = 0;

        string path = string.Join("/", keys);
        var data = await dbRef.Child("user").Child(UserData.instance.User.UserId).Child(path).GetValueAsync();

        if (data != null)
        {
            DataSnapshot snapshot = data;

            intValue = int.Parse(snapshot.Value.ToString());
        }

        return intValue;
    }

    public async Task<float> LoadDataFloat(params string[] keys)
    {
        float floatValue = 0;

        string path = string.Join("/", keys);
        var data = await dbRef.Child("user").Child(UserData.instance.User.UserId).Child(path).GetValueAsync();

        if (data != null)
        {
            DataSnapshot snapshot = data;

            floatValue = float.Parse(snapshot.Value.ToString());
        }

        return floatValue;
    }

    public async Task<string> LoadDataString(params string[] keys)
    {
        string stringValue = "";

        string path = string.Join("/", keys);
        var data = await dbRef.Child("user").Child(UserData.instance.User.UserId).Child(path).GetValueAsync();

        if (data != null)
        {
            DataSnapshot snapshot = data;

            stringValue = snapshot.Value.ToString();
        }

        return stringValue;
    }

    public async Task<bool> LoadDataBool(params string[] keys)
    {
        bool boolValue = false;

        string path = string.Join("/", keys);
        var data = await dbRef.Child("user").Child(UserData.instance.User.UserId).Child(path).GetValueAsync();

        if (data != null)
        {
            DataSnapshot snapshot = data;

            boolValue = bool.Parse(snapshot.Value.ToString());
        }

        return boolValue;
    }

    public async Task<bool> LoadDataCheck(params string[] keys)
    {
        string path = string.Join("/", keys);
        var data = await dbRef.Child("user").Child(UserData.instance.User.UserId).Child(path).GetValueAsync();

        if (data != null)
        {
            DataSnapshot snapshot = data;

            if (snapshot.Value == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        return false;
    }
}