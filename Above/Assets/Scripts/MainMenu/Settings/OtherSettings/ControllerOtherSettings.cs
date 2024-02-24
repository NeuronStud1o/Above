using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerOtherSettings : MonoBehaviour
{
    [Header ("## Menu settings : ")]
    [SerializeField] private GameObject levelRanks;
    [SerializeField] private GameObject boost;
    [SerializeField] private List<MonoBehaviour> autoSaveScripts;
    [SerializeField] private List<GameObject> particles;

    void Start()
    {
        //ShowLevelRanks(JsonStorage.instance.jsonData.otherSettings.showLevelRanks);
        //AutoSaveSettings(JsonStorage.instance.jsonData.otherSettings.autoSave);
        //ActivateParticles(JsonStorage.instance.jsonData.otherSettings.particles);
        //ShowTheSelectedBoost(JsonStorage.instance.jsonData.otherSettings.showSelectedBoostInGame);
    }

    public void ShowLevelRanks(bool tog)
    {
        if (tog == true)
        {
            levelRanks.SetActive(true);
        }
        else
        {
            levelRanks.SetActive(false);
        }
    }

    public void AutoSaveSettings(bool tog)
    {
        if (tog == true)
        {
            foreach (MonoBehaviour script in autoSaveScripts)
            {
                script.enabled = true;
            }
        }
        else
        {
            foreach (MonoBehaviour script in autoSaveScripts)
            {
                script.enabled = false;
            }
        }
    }

    public void ActivateParticles(bool tog)
    {
        if (tog == true)
        {
            foreach (GameObject go in particles)
            {
                go.SetActive(true);
            }
        }
        else
        {
            foreach (GameObject go in particles)
            {
                go.SetActive(false);
            }
        }
    }

    public void ShowTheSelectedBoost(bool tog)
    {
        if (tog == true)
        {
            boost.SetActive(true);
        }
        else
        {
            boost.SetActive(false);
        }
    }
}
