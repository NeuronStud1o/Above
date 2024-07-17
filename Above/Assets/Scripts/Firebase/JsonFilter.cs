using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public static class JsonFilter
{
    static string filePath;

    public async static Task StartFilter()
    {
        Debug.Log("Filter");
        await CheckingJsons();
    }

    private static async Task CheckingJsons()
    {
        filePath = Path.Combine(Application.persistentDataPath, "gameData.json");
        bool isAdditionallyFileOnServer = await StorageData.instance.CheckAdditionalJson();
        bool isFileOnServer = await StorageData.instance.CheckIfJsonDataExists();

        if (!isFileOnServer)
        {
            if (isAdditionallyFileOnServer)
            {
                ReplacingInfo();
            }
            else
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
        }

        await JsonStorage.instance.CheckJsons();
    }
    

    private static async void ReplacingInfo()
    {
        Debug.Log("Replacing");

        JsonData jsonData = await StorageData.instance.LoadAdditionalJsonData<JsonData>();

        JsonStorage.instance.data.CopyFromJsonData(jsonData);

        JsonStorage.instance.data.purchasedItems.skins = FindObjects(jsonData.shop.skins);
        JsonStorage.instance.data.purchasedItems.bgs = FindObjects(jsonData.shop.bgs);
        JsonStorage.instance.data.purchasedItems.boosts = FindObjects(jsonData.shop.boosts);
        JsonStorage.instance.data.icons.icons = FindObjects(jsonData.accountIcons.icons);

        StorageData.instance.DeleteAdditionalJson();
        StorageData.instance.SaveJsonData();
    }

    private static List<string> FindObjects(List<KeyForm> jsonList)
    {
        List<string> itemsToChange = new List<string>();

        foreach (KeyForm kf in jsonList)
        {
            if (kf.isPurchased == true)
            {
                itemsToChange.Add(kf.name);
            }
        }

        return itemsToChange;
    }
}
