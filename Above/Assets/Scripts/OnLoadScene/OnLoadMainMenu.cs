using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class OnLoadMainMenu : MonoBehaviour
{
    public static OnLoadMainMenu instance;

    public List<Task> scriptsList = new List<Task>();

    void Awake()
    {
        instance = this;
    }

    private async void Start()
    {
        await Task.Delay(1000);

        await StartFunc();
    }

    private async Task StartFunc()
    {
        foreach (var sc in scriptsList)
        {
            await sc;
        }

        DataBase.instance.SetActiveLoadingScreen(false);
        print("Done");
    }
}