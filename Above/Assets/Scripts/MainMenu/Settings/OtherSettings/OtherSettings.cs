using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OtherSettings : MonoBehaviour
{
    [SerializeField] private ControllerOtherSettings controller;

    public void ShowLevelRanks(bool tog)
    {
        if (tog == true)
        {
            JsonStorage.instance.jsonData.otherSettings.showLevelRanks = true;
        }
        else
        {
            JsonStorage.instance.jsonData.otherSettings.showLevelRanks = false;
        }

        controller.ShowLevelRanks(tog);

        string filePath = Path.Combine(Application.persistentDataPath, "gameData.json");
        CryptoHelper.Encrypt(filePath, JsonStorage.instance.jsonData, JsonStorage.instance.password);
    }

    public void AutoSaveSettings(bool tog)
    {
        if (tog == true)
        {
            JsonStorage.instance.jsonData.otherSettings.autoSave = true;
        }
        else
        {
            JsonStorage.instance.jsonData.otherSettings.autoSave = false;
        }

        controller.AutoSaveSettings(tog);

        string filePath = Path.Combine(Application.persistentDataPath, "gameData.json");
        CryptoHelper.Encrypt(filePath, JsonStorage.instance.jsonData, JsonStorage.instance.password);
    }

    public void OpenDocumentation()
    {
        Application.OpenURL("https://docs.google.com/document/d/1ANJd7XmfpLubOtsssmON4Fuc_4nhoc4LQUoeockF8RY/edit?usp=sharing");
    }

    public void PlayTutorial()
    {
        SceneManager.LoadSceneAsync("Tutorial");
    }

    public void Logout()
    {
        SceneManager.LoadSceneAsync("Authentication");
    }

    public async void DeleteAccount()
    {
        await AccountDeleting();
    }

    async Task AccountDeleting()
    {
        await StorageData.instance.DeleteUser();

        Debug.Log("Data is deleted");

        DataBase.instance.SetActiveLoadingScreen(false);
        SceneManager.LoadSceneAsync("Authentication");
    }

    public void RenameNick()
    {

    }

    public void ActivateParticles(bool tog)
    {
        if (tog == true)
        {
            JsonStorage.instance.jsonData.otherSettings.particles = true;
        }
        else
        {
            JsonStorage.instance.jsonData.otherSettings.particles = false;
        }

        controller.ActivateParticles(tog);

        string filePath = Path.Combine(Application.persistentDataPath, "gameData.json");
        CryptoHelper.Encrypt(filePath, JsonStorage.instance.jsonData, JsonStorage.instance.password);
    }

    public void ShowTheSelectedBoost(bool tog)
    {
        if (tog == true)
        {
            JsonStorage.instance.jsonData.otherSettings.showSelectedBoostInGame = true;
        }
        else
        {
            JsonStorage.instance.jsonData.otherSettings.showSelectedBoostInGame = false;
        }

        controller.ShowTheSelectedBoost(tog);

        string filePath = Path.Combine(Application.persistentDataPath, "gameData.json");
        CryptoHelper.Encrypt(filePath, JsonStorage.instance.jsonData, JsonStorage.instance.password);
    }

    public void CameraShake(bool tog)
    {
        if (tog == true)
        {
            JsonStorage.instance.jsonData.otherSettings.cameraShake = true;
        }
        else
        {
            JsonStorage.instance.jsonData.otherSettings.cameraShake = false;
        }

        string filePath = Path.Combine(Application.persistentDataPath, "gameData.json");
        CryptoHelper.Encrypt(filePath, JsonStorage.instance.jsonData, JsonStorage.instance.password);
    }

    public void Vibration(bool tog)
    {
        if (tog == true)
        {
            JsonStorage.instance.jsonData.otherSettings.vibration = true;
        }
        else
        {
            JsonStorage.instance.jsonData.otherSettings.vibration = false;
        }

        string filePath = Path.Combine(Application.persistentDataPath, "gameData.json");
        CryptoHelper.Encrypt(filePath, JsonStorage.instance.jsonData, JsonStorage.instance.password);
    }
}
