using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class OnLoadGame : MonoBehaviour
{
    public static OnLoadGame instance;

    public List<Task> scriptsList = new List<Task>();
    [SerializeField] private SelectCharacterInGame selectCharacter;

    private async void Start()
    {
        instance = this;

        await Task.Delay(1000);

        await StartFunc();
    }

    private async Task StartFunc()
    {
        foreach (var sc in scriptsList)
        {
            await sc;
        }

        await selectCharacter.StartActivity();

        DataBase.instance.SetActiveLoadingScreen(false);
        print("Done");
    }
}
