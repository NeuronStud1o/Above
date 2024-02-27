using System.Collections;
using System.Collections.Generic;
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
    }
}
