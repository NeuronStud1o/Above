using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using System.Threading.Tasks;

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

    public void SaveData(int i, params string[] keys)
    {
        string path = string.Join("/", keys);
        dbRef.Child("user").Child(UserData.instance.User.DisplayName).Child(path).SetValueAsync(i);
    }

    public void SaveData(float i, params string[] keys)
    {
        string path = string.Join("/", keys);
        dbRef.Child("user").Child(UserData.instance.User.DisplayName).Child(path).SetValueAsync(i);
    }

    public void SaveData(string i, params string[] keys)
    {
        string path = string.Join("/", keys);
        dbRef.Child("user").Child(UserData.instance.User.DisplayName).Child(path).SetValueAsync(i);
    }

    public void SaveData(bool i, params string[] keys)
    {
        string path = string.Join("/", keys);
        dbRef.Child("user").Child(UserData.instance.User.DisplayName).Child(path).SetValueAsync(i);
    }

    public async Task<int> LoadDataInt(params string[] keys)
    {
        int intValue = 0;

        string path = string.Join("/", keys);
        var data = await dbRef.Child("user").Child(UserData.instance.User.DisplayName).Child(path).GetValueAsync();

        if (data != null)
        {
            DataSnapshot snapshot = data;

            intValue = int.Parse(snapshot.GetValue(true).ToString());
        }

        return intValue;
    }

    public async Task<float> LoadDataFloat(params string[] keys)
    {
        float floatValue = 0;

        string path = string.Join("/", keys);
        var data = await dbRef.Child("user").Child(UserData.instance.User.DisplayName).Child(path).GetValueAsync();

        if (data != null)
        {
            DataSnapshot snapshot = data;

            floatValue = float.Parse(snapshot.ToString());
        }

        return floatValue;
    }

    public async Task<string> LoadDataString(params string[] keys)
    {
        string stringValue = "";

        string path = string.Join("/", keys);
        var data = await dbRef.Child("user").Child(UserData.instance.User.DisplayName).Child(path).GetValueAsync();

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
        var data = await dbRef.Child("user").Child(UserData.instance.User.DisplayName).Child(path).GetValueAsync();

        if (data != null)
        {
            DataSnapshot snapshot = data;

            boolValue = bool.Parse(snapshot.ToString());
        }

        return boolValue;
    }

    public async Task<bool> LoadDataCheck(params string[] keys)
    {
        string path = string.Join("/", keys);
        var data = await dbRef.Child("user").Child(UserData.instance.User.DisplayName).Child(path).GetValueAsync();

        if (data != null)
        {
            DataSnapshot snapshot = data;

            if (snapshot.Value == null)
            {
                print ("Value is null");
                return false;
            }
            else
            {
                print ("Value is not null");
                return true;
            }
        }

        return false;
    }
}